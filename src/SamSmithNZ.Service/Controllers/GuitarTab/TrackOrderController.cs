using Microsoft.AspNetCore.Mvc;
using SamSmithNZ.Service.DataAccess.GuitarTab.Interfaces;
using SamSmithNZ.Service.Models.GuitarTab;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.Controllers.GuitarTab
{
    [Route("api/guitartab/[controller]")]
    [ApiController]
    public class TrackOrderController : ControllerBase
    {
        private readonly ITrackOrderDataAccess _repo;

        public TrackOrderController(ITrackOrderDataAccess repo)
        {
            _repo = repo;
        }

        [HttpGet("GetTrackOrders")]
        public async Task<List<TrackOrder>> GetTrackOrders()
        {
            return await _repo.GetList();
        }
    }
}
