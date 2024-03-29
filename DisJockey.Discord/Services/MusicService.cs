using System;
using System.Linq;
using System.Threading.Tasks;
using DisJockey.Shared.Exceptions;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Victoria;
using Victoria.Enums;
using Victoria.EventArgs;
using DisJockey.Services.Interfaces;
using Victoria.Responses.Search;

namespace DisJockey.Discord.Services
{
    public class MusicService
    {
        private readonly LavaNode _lavaNode;
        private readonly IServiceScopeFactory _serviceScope;
        private LavaTrack _pulledTrack;

        public MusicService(LavaNode lavaNode, IServiceScopeFactory serviceScope)
        {
            _serviceScope = serviceScope;
            _lavaNode = lavaNode;

            _lavaNode.OnTrackEnded += OnTrackEnded;
            _pulledTrack = null;
        }

        public async Task<string> PlayTrack(string query, SocketUser user, SocketGuild guild, bool skipQueue)
        {
            if (!_lavaNode.HasPlayer(guild))
            {
                await ConnectToChannelAsync(user, guild);
            }
            var player = _lavaNode.GetPlayer(guild);

            var results = await _lavaNode.SearchYouTubeAsync($"\"{query}\"");

            if (results.Status is SearchStatus.LoadFailed or SearchStatus.NoMatches)
            {
                return "No matches found";
            }

            using var scope = _serviceScope.CreateScope();

            var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();

            var track = results.Tracks.FirstOrDefault();

            if (await unitOfWork.TrackRepository.IsTrackBlacklisted(track.Id))
            {
                return "This track is blacklisted";
            }

            string returnMessage;
            if (player.PlayerState == PlayerState.Playing && !skipQueue)
            {
                player.Queue.Enqueue(track);
                returnMessage = $"Track {track.Title} is queued";
            } else
            {
                await player.PlayAsync(track);
                returnMessage = $"Track {track.Title} is now playing";
            }

            var discordTrackService = scope.ServiceProvider.GetService<IDiscordTrackService>();

            try
            {
                await discordTrackService.AddTrackAsync(user, track.Url);
            } catch (Exception e)
            {
                returnMessage = e.Message;
            }

            return returnMessage;
        }

        public async Task<string> PullUpTrackAsync(LavaPlayer player, LavaTrack track, SocketUser user)
        {
            LavaTrack wheelUpSound;
            try
            {
                wheelUpSound = await GetWheelUpSoundAsync();
            } catch
            {
                return "Something went wrong getting the Wheel Up Sound";
            }

            string returnMessage = "";

            var currentPosition = track.Position.TotalSeconds;

            await player.PlayAsync(wheelUpSound);
            _pulledTrack = track;

            using (var scope = _serviceScope.CreateScope())
            {
                try
                {
                    var discordTrackService = scope.ServiceProvider.GetService<IDiscordTrackService>();

                    try
                    {
                        await discordTrackService.PullUpTrackAsync(user, track.Url, currentPosition);
                    } catch (InvalidUrlException e)
                    {
                        returnMessage = e.ToString();
                    } catch (DataContextException e)
                    {
                        returnMessage = e.ToString();
                    }
                } catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            return returnMessage;
        }

        private async Task OnTrackEnded(TrackEndedEventArgs args)
        {
            if (args.Reason != TrackEndReason.Finished)
            {
                return;
            }

            var player = args.Player;

            if (_pulledTrack != null)
            {
                await player.PlayAsync(_pulledTrack);
                _pulledTrack = null;
                return;
            }

            if (!player.Queue.TryDequeue(out var queueable))
            {
                return;
            }

            if (queueable is not LavaTrack track)
            {
                await player.TextChannel.SendMessageAsync("Something wrong with the next track");
                return;
            }

            await player.PlayAsync(track);
        }

        private async Task ConnectToChannelAsync(SocketUser user, SocketGuild guild)
        {
            var voiceChannel = guild.VoiceChannels.FirstOrDefault(x => x.Users.FirstOrDefault(u => u.Id == user.Id) != null);

            if (voiceChannel == null) throw new Exception("You must be connected to a Voice Channel to play a track");

            await _lavaNode.JoinAsync(voiceChannel, guild.DefaultChannel);
        }

        private async Task<LavaTrack> GetWheelUpSoundAsync()
        {
            return await SearchForTrackAsync("https://youtu.be/LfbJs4uoHF0");
        }

        private async Task<LavaTrack> SearchForTrackAsync(string query)
        {
            var results = await _lavaNode.SearchYouTubeAsync(query);

            if (results.Status is SearchStatus.LoadFailed or SearchStatus.NoMatches)
            {
                throw new Exception("Something went wrong with the search");
            }

            return results.Tracks.First();
        }
    }
}