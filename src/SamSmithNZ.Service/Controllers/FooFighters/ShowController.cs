using Microsoft.AspNetCore.Mvc;
using SamSmithNZ.Service.DataAccess.FooFighters.Interfaces;
using SamSmithNZ.Service.Models.FooFighters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.Controllers.FooFighters
{
    [Route("api/foofighters/[controller]")]
    [ApiController]
    public class ShowController : ControllerBase
    {
        private readonly IShowDataAccess _repo;

        public ShowController(IShowDataAccess repo)
        {
            _repo = repo;
        }

        [HttpGet("GetShowsByYear")]
        public async Task<List<Show>> GetShowsByYear(int yearCode)
        {
            return await _repo.GetListByYearAsync(yearCode);
        }

        [HttpGet("GetShowsBySong")]
        public async Task<List<Show>> GetShowsBySong(int songCode)
        {
            return await _repo.GetListBySongAsync(songCode);
        }

        [HttpGet("GetListByFFLCode")]
        public async Task<List<Show>> GetListByFFLCode( )
        {
            return await _repo.GetListByFFLCode();
        }

        [HttpGet("GetShow")]
        public async Task<Show> GetShow(int showCode)
        {
            return await _repo.GetItem(showCode);
        }

    }
}
