using Microsoft.AspNetCore.Mvc;
using SamSmithNZ.Service.DataAccess.GuitarTab.Interfaces;
using SamSmithNZ.Service.Models.GuitarTab;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.Controllers.GuitarTab
{
    [Route("api/guitartab/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistDataAccess _repo;

        public ArtistController(IArtistDataAccess repo)
        {
            _repo = repo;
        }

        [HttpGet("GetArtists")]
        public async Task<List<Artist>> GetArtists(bool includeAllItems = false, bool isAdmin = false)
        {
            return await _repo.GetList(includeAllItems, isAdmin);
        }
    }
}
