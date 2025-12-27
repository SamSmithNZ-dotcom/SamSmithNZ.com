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
        private readonly IITunesServiceApiClient _serviceApiClient;

        public ITunesController(IITunesServiceApiClient serviceApiClient)
        {
            _serviceApiClient = serviceApiClient;
        }

        public async Task<IActionResult> Index()
        {
            bool showJustSummary = true;
            Task<List<TopArtists>> topArtistsTask = _serviceApiClient.GetTopArtistsSummary(showJustSummary);
            Task<List<Playlist>> playlistsTask = _serviceApiClient.GetPlaylists(showJustSummary);
            await Task.WhenAll(topArtistsTask, playlistsTask);

            List<TopArtists> topArtists = await topArtistsTask;
            List<Playlist> playlists = await playlistsTask;

            return View(new IndexViewModel
            {
                TopArtists = topArtists,
                Playlists = playlists
            });
        }

        public async Task<IActionResult> PlayList(int playlistCode, bool showJustSummary = true)
        {
            Task<Playlist> playlistTask = _serviceApiClient.GetPlaylist(playlistCode);
            Task<List<TopArtists>> topArtistsTask = _serviceApiClient.GetTopArtistsByPlaylist(playlistCode, showJustSummary);
            Task<List<Movement>> movementsTask = _serviceApiClient.GetMovementsByPlaylist(playlistCode, showJustSummary);
            Task<List<Track>> tracksTask = _serviceApiClient.GetTracks(playlistCode, showJustSummary);
            await Task.WhenAll(playlistTask, topArtistsTask, movementsTask, tracksTask);

            Playlist playlist = await playlistTask;
            List<TopArtists> topArtists = await topArtistsTask;
            List<Movement> movements = await movementsTask;
            List<Track> tracks = await tracksTask;

            return View(new PlaylistViewModel
            {
                Playlist = playlist,
                TopArtists = topArtists,
                Movements = movements,
                Tracks = tracks
            });
        }

        public IActionResult About()
        {
            return RedirectToAction("About", "Home");
        }
    }
}
