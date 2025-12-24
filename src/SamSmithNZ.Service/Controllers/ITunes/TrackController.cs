using Microsoft.AspNetCore.Mvc;
using SamSmithNZ.Service.DataAccess.ITunes.Interfaces;
using SamSmithNZ.Service.Models.ITunes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.Controllers.ITunes
{
    [Route("api/itunes/[controller]")]
    [ApiController]
    public class TrackController : ControllerBase
    {
        private readonly ITrackDataAccess _repo;

        public TrackController(ITrackDataAccess repo)
        {
            _repo = repo;
        }

        [HttpGet("GetTracks")]
        public async Task<List<Track>> GetTracks(int playlistCode, bool showJustSummary)
        {
            return await _repo.GetList(playlistCode, showJustSummary);
        }

        //public async Task<Track> GetTrack(int playlistCode, bool showJustSummary, String trackName)
        //{
        //    TrackDataAccess da = new TrackDataAccess();
        //    return await da.GetItemAsync(playlistCode, showJustSummary, trackName);
        //}
    }
}
