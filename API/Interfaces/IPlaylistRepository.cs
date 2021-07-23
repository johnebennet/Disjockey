﻿using API.DTOs.Playlist;
using DisJockey.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Interfaces {
    public interface IPlaylistRepository {
        Task<Playlist> AddPlaylist(Playlist playlist, AppUser user);
        Task AddMissingTracks(IList<PlaylistTrack> playlistTracks);
        Task<PlaylistDetailDto> GetPlaylistById(int id);
        Task<PlaylistDetailDto> GetPlaylistByYoutubeId(string youtubeId);
        Task<bool> CheckPlaylistExists(string playlistId);
    }
}
