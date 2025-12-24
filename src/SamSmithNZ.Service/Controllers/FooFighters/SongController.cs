using Microsoft.AspNetCore.Mvc;
using SamSmithNZ.Service.DataAccess.FooFighters.Interfaces;
using SamSmithNZ.Service.Models.FooFighters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.Controllers.FooFighters
{
    [Route("api/foofighters/[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {
        private readonly ISongDataAccess _repo;

        public SongController(ISongDataAccess repo)
        {
            _repo = repo;
        }

        [HttpGet("GetSongs")]
        public async Task<List<Song>> GetSongs()
        {
            return await _repo.GetList();
        }

        [HttpGet("GetSongsByAlbum")]
        public async Task<List<Song>> GetSongsByAlbum(int albumCode)
        {
            return await _repo.GetListForAlbumAsync(albumCode);
        }

        [HttpGet("GetSongsByShow")]
        public async Task<List<Song>> GetSongsByShow(int showCode)
        {
            return await _repo.GetListForShowAsync(showCode);
        }

        [HttpGet("GetSong")]
        public async Task<Song> GetSong(int songCode)
        {
            return await _repo.GetItem(songCode);
        }
    }
}
