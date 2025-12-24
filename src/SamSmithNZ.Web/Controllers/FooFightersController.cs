using Microsoft.AspNetCore.Mvc;
using SamSmithNZ.Service.Models.FooFighters;
using SamSmithNZ.Web.Models.FooFighters;
using SamSmithNZ.Web.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Web.Controllers
{
    public class FooFightersController : Controller
    {
        private readonly IFooFightersServiceApiClient _ServiceApiClient;

        public FooFightersController(IFooFightersServiceApiClient ServiceApiClient)
        {
            _ServiceApiClient = ServiceApiClient;
        }

        public async Task<IActionResult> Index()
        {
            List<Song> items = await _ServiceApiClient.GetSongs();
            return View(items);
        }

        public async Task<IActionResult> ShowHistory(int yearCode = 0)
        {
            Task<List<Year>> yearsTask = _ServiceApiClient.GetYears();
            Task<List<AverageSetlist>> averageSetlistsTask = _ServiceApiClient.GetAverageSetlist(yearCode);
            Task<List<Show>> showsTask = _ServiceApiClient.GetShowsByYear(yearCode);
            await Task.WhenAll(yearsTask, averageSetlistsTask, showsTask);

            return View(new ShowHistoryViewModel(await yearsTask)
            {
                YearCode = yearCode,
                AverageSetlists = await averageSetlistsTask,
                Shows = await showsTask
            });
        }

        [HttpPost]
        public IActionResult ShowHistoryYearPost(int yearCode)
        {
            if (yearCode > 0)
            {
                return RedirectToAction("ShowHistory", new { yearCode = yearCode });
            }

            return View(yearCode);
        }

        public async Task<IActionResult> Song(int songCode = 0, int? songkey = null)
        {
            //for backward compatibility
            if (songkey != null)
            {
                songCode = (int)songkey;
            }
            Task<Song> songTask = _ServiceApiClient.GetSong(songCode);
            Task<List<Show>> showsTask = _ServiceApiClient.GetShowsBySong(songCode);
            await Task.WhenAll(songTask, showsTask);

            return View(new SongViewModel
            {
                Song = await songTask,
                Shows = await showsTask
            });
        }

        public async Task<IActionResult> Show(int showCode = 0, int? showkey = null)
        {
            //for backward compatibility
            if (showkey != null)
            {
                showCode = (int)showkey;
            }
            Task<Show> showTask = _ServiceApiClient.GetShow(showCode);
            Task<List<Song>> songsTask = _ServiceApiClient.GetSongsByShow(showCode);
            await Task.WhenAll(showTask, songsTask);

            return View(new ShowViewModel
            {
                Show = await showTask,
                Songs = await songsTask
            });
        }

        public async Task<IActionResult> Album(int albumCode = 0, int? albumkey = null)
        {
            //for backward compatibility
            if (albumkey != null)
            {
                albumCode = (int)albumkey;
            }
            Task<Album> albumTask = _ServiceApiClient.GetAlbum(albumCode);
            Task<List<Song>> songsTask = _ServiceApiClient.GetSongsByAlbum(albumCode);
            await Task.WhenAll(albumTask, songsTask);

            return View(new AlbumViewModel
            {
                Album = await albumTask,
                Songs = await songsTask
            });
        }

        public async Task<IActionResult> AlbumList()
        {
            List<Album> albums = await _ServiceApiClient.GetAlbums();
            return View(albums);
        }

        public IActionResult ShowYearList()
        {
            return RedirectToAction("ShowList");
        }

        public IActionResult About()
        {
            return RedirectToAction("About", "Home");
        }
    }
}
