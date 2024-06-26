﻿using ArabTube.Entities.DtoModels.PlaylistDTOs;
using ArabTube.Entities.GenericModels;
using ArabTube.Entities.PlaylistModels;

namespace ArabTube.Services.ControllersServices.PlaylistServices.Interfaces
{
    public interface IPlaylistService
    {
        Task<SearchPlaylistsTitlesResult> SearchPlaylistsTitlesAsync(string query);
        Task<GetPlaylistsResult> SearchPlaylistsAsync(string query);
        Task<GetPlaylistsResult> GetMyPlaylistsAsync(string userId);
        Task<GetPlaylistsResult> GetPlaylistsAsync(string userId);
        Task<ProcessResult> CreatePlaylistAsync(CreatePlaylistDto model, string userId);
        Task<ProcessResult> SavePlaylistAsync(string playlistId, string userId);
        Task<ProcessResult> UpdatePlaylistAsync(UpdatePlaylistDto model , string userId);
        Task<ProcessResult> DeletePlaylistAsync(string playlistId , string userId);
        Task<ProcessResult> CreateDefultPlaylistsAsync(string userId);
        Task<string> GetPlaylistIdAsync(string playlistTitle, bool isDefaultPlaylist, string userId);

    }
}
