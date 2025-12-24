using Microsoft.AspNetCore.Mvc;
using SamSmithNZ.Service.DataAccess.ITunes.Interfaces;
using SamSmithNZ.Service.Models.ITunes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.Controllers.ITunes
{
    [Route("api/itunes/[controller]")]
    [ApiController]
    public class PlaylistController : ControllerBase
    {
        private readonly IPlaylistDataAccess _repo;

        public PlaylistController(IPlaylistDataAccess repo)
        {
            _repo = repo;
        }

        [HttpGet("GetPlaylists")]
        public async Task<List<Playlist>> GetPlaylists()
        {
            return await _repo.GetList();
        }

        [HttpGet("GetPlaylist")]
        public async Task<Playlist> GetPlaylist(int playlistCode)
        {
            return await _repo.GetItem(playlistCode);
        }
        
    }
}
