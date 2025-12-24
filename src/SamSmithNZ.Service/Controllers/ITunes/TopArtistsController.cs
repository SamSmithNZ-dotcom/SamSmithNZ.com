using Microsoft.AspNetCore.Mvc;
using SamSmithNZ.Service.DataAccess.ITunes.Interfaces;
using SamSmithNZ.Service.Models.ITunes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.Controllers.ITunes
{
    [Route("api/itunes/[controller]")]
    [ApiController]
    public class TopArtistsController : ControllerBase
    {
        private readonly ITopArtistsDataAccess _repo;

        public TopArtistsController(ITopArtistsDataAccess repo)
        {
            _repo = repo;
        }

        [HttpGet("GetTopArtistsByPlaylist")]
        public async Task<List<TopArtists>> GetTopArtistsByPlaylist(int playlistCode, bool showJustSummary)
        {
            return await _repo.GetList(playlistCode, showJustSummary);
        }

        [HttpGet("GetTopArtistsSummary")]
        public async Task<List<TopArtists>> GetTopArtistsSummary(bool showJustSummary)
        {
            return await _repo.GetList(showJustSummary);
        }
    }
}
