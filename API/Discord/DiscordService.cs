using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Victoria;

namespace API.Discord {
    public class DiscordService : BackgroundService {
        private readonly IConfiguration _config;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly BotSettings _botSettings;
        private readonly DiscordSocketClient _client;
        private readonly CommandService _cmdService;
        private readonly IServiceProvider _services;
        private readonly LavaNode _lavaNode;

        public DiscordService(IConfiguration config, IServiceScopeFactory serviceScopeFactory,
                              DiscordSocketClient client, BotSettings botSettings, CommandService cmdService,
                              IServiceProvider services, LavaNode lavaNode) {
            _cmdService = cmdService;
            _services = services;
            _client = client;
            _botSettings = botSettings;
            _serviceScopeFactory = serviceScopeFactory;
            _config = config;
            _lavaNode = lavaNode;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
            await MainAsync();
        }

        public async Task MainAsync() {
            _client.Log += LogAsync;

            await _client.LoginAsync(TokenType.Bot, _botSettings.BotToken);
            await _client.StartAsync();

            await _cmdService.AddModulesAsync(Assembly.GetEntryAssembly(), _services);

            _client.Ready += ReadyAsync;
            _cmdService.Log += LogAsync;
            _client.MessageReceived += MessageReceivedAsync;
        }

        private async Task MessageReceivedAsync(SocketMessage message) {
            var argPos = 0;

            if (message.Author.IsBot)
                return;

            var userMessage = message as SocketUserMessage;
            if (userMessage is null)
                return;

            if (!userMessage.HasCharPrefix(_botSettings.Prefix, ref argPos))
                return;

            var context = new SocketCommandContext(_client, userMessage);
            var result = await _cmdService.ExecuteAsync(context, argPos, _services);
        }

        private Task LogAsync(LogMessage msg) {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private async Task ReadyAsync() {
            if (!_lavaNode.IsConnected) {
                _lavaNode.OnLog += LogAsync;
                await _lavaNode.ConnectAsync();
            }

            Console.WriteLine($"Connected as -> {_client.CurrentUser.Username}");
        }
    }
}