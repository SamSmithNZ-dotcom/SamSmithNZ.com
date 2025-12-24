using Microsoft.AspNetCore.Mvc;
using SamSmithNZ.Service.DataAccess.FooFighters.Interfaces;
using SamSmithNZ.Service.Models.FooFighters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.Controllers.FooFighters
{
    [Route("api/foofighters/[controller]")]
    [ApiController]
    public class YearController : ControllerBase
    {
        private readonly IYearDataAccess _repo;

        public YearController(IYearDataAccess repo)
        {
            _repo = repo;
        }

        [HttpGet("GetYears")]
        public async Task<List<Year>> GetYears()
        {
            return await _repo.GetList();
        }

    }
}
