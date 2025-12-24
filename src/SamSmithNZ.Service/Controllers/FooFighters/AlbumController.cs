using Microsoft.AspNetCore.Mvc;
using SamSmithNZ.Service.DataAccess.FooFighters.Interfaces;
using SamSmithNZ.Service.Models.FooFighters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.Controllers.FooFighters
{
    [Route("api/foofighters/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumDataAccess _repo;

        public AlbumController(IAlbumDataAccess repo)
        {
            _repo = repo;
        }

        [HttpGet("GetAlbums")]
        public async Task<List<Album>> GetAlbums()
        {
            return await _repo.GetList();
        }

        [HttpGet("GetAlbum")]
        public async Task<Album> GetAlbum(int albumCode)
        {
            return await _repo.GetItem(albumCode);
        }

    }
}
