using Microsoft.AspNetCore.Mvc;
using SamSmithNZ.Service.Models.ITunes;
using SamSmithNZ.Web.Models.ITunes;
using SamSmithNZ.Web.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Web.Controllers
{
    public class ITunesController : Controller
    {
        private readonly IITunesServiceApiClient _ServiceApiClient;

        public ITunesController(IITunesServiceApiClient serviceApiClient)
        {
            _ServiceApiClient = serviceApiClient;
        }

        public async Task<IActionResult> Index()
        {
            bool showJustSummary = true;
            Task<List<TopArtists>> topArtistsTask = _ServiceApiClient.GetTopArtistsSummary(showJustSummary);
            Task<List<Playlist>> playlistsTask = _ServiceApiClient.GetPlaylists(showJustSummary);
            await Task.WhenAll(topArtistsTask, playlistsTask);

            return View(new IndexViewModel
            {
                TopArtists = topArtistsTask.Result,
                Playlists = playlistsTask.Result
            });
        }

        public async Task<IActionResult> PlayList(int playlistCode, bool showJustSummary = true)
        {
            Task<Playlist> playlistTask = _ServiceApiClient.GetPlaylist(playlistCode);
            Task<List<TopArtists>> topArtistsTask = _ServiceApiClient.GetTopArtistsByPlaylist(playlistCode, showJustSummary);
            Task<List<Movement>> movementsTask = _ServiceApiClient.GetMovementsByPlaylist(playlistCode, showJustSummary);
            Task<List<Track>> tracksTask = _ServiceApiClient.GetTracks(playlistCode, showJustSummary);
            await Task.WhenAll(playlistTask, topArtistsTask, movementsTask, tracksTask);

            return View(new PlaylistViewModel
            {
                Playlist = playlistTask.Result,
                TopArtists = topArtistsTask.Result,
                Movements = movementsTask.Result,
                Tracks = tracksTask.Result
            });
        }

        public IActionResult About()
        {
            return RedirectToAction("About", "Home");
        }
    }
}
