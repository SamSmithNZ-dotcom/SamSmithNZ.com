using Microsoft.AspNetCore.Mvc;
using SamSmithNZ.Service.Models.GuitarTab;
using SamSmithNZ.Web.Models.GuitarTab;
using SamSmithNZ.Web.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamSmithNZ.Web.Controllers
{
    public class GuitarTabController : Controller
    {
        private readonly IGuitarTabServiceApiClient _ServiceApiClient;

        public GuitarTabController(IGuitarTabServiceApiClient ServiceApiClient)
        {
            _ServiceApiClient = ServiceApiClient;
        }

        public async Task<IActionResult> Index(bool isAdmin = false)
        {
            // Execute both API calls in parallel
            Task<List<Artist>> artistsTask = _ServiceApiClient.GetArtists(isAdmin);
            Task<List<Album>> albumsTask = _ServiceApiClient.GetAlbums(isAdmin);
            await Task.WhenAll(artistsTask, albumsTask);

            List<Artist> artists = await artistsTask;
            List<Album> albums = await albumsTask;

            // Use ToLookup for O(1) lookups instead of O(n) per artist
            ILookup<string, Album> albumLookup = albums.ToLookup(a => a.ArtistName);

            // Combine artists and their albums
            List<KeyValuePair<Artist, List<Album>>> items = new();
            foreach (Artist artist in artists)
            {
                List<Album> artistAlbums = albumLookup[artist.ArtistName].ToList();
                items.Add(new KeyValuePair<Artist, List<Album>>(artist, artistAlbums));
            }

            return View(new IndexViewModel
            {
                ArtistAlbums = items,
                IsAdmin = isAdmin
            });
        }

        public IActionResult About()
        {
            return RedirectToAction("About", "Home");
        }

        public async Task<IActionResult> Album(int albumCode, bool isAdmin = false)
        {
            Task<Album> albumTask = _ServiceApiClient.GetAlbum(albumCode, isAdmin);
            Task<List<Tab>> tabsTask = _ServiceApiClient.GetTabs(albumCode);
            await Task.WhenAll(albumTask, tabsTask);

            return View(new AlbumViewModel()
            {
                Album = await albumTask,
                Tabs = await tabsTask,
                IsAdmin = isAdmin
            });
        }

        public async Task<IActionResult> SearchResults(string searchText, bool isAdmin = false)
        {
            List<Search> searchResults = await _ServiceApiClient.GetSearchResults(searchText);

            return View(new SearchViewModel
            {
                SearchResults = searchResults,
                IsAdmin = isAdmin
            });
        }

        [HttpPost]
        public IActionResult SearchPost(string txtSearch, bool isAdmin = false)
        {
            return RedirectToAction("SearchResults", new
            {
                searchText = txtSearch,
                isAdmin = isAdmin
            });
        }

        public async Task<IActionResult> EditAlbum(int albumCode, bool isAdmin = false)
        {
            Task<Album> albumTask = _ServiceApiClient.GetAlbum(albumCode, true);
            Task<List<Tab>> tabsTask = _ServiceApiClient.GetTabs(albumCode);
            await Task.WhenAll(albumTask, tabsTask);

            return View(new AlbumTabsViewModel
            {
                Album = await albumTask,
                Tabs = await tabsTask,
                IsAdmin = isAdmin
            });
        }

        [HttpPost]
        public async Task<IActionResult> SaveAlbum(int albumCode, string txtArtist, string txtAlbumName, string txtYear,
            bool chkIsBassTab, bool chkIncludeInIndex, bool chkIncludeOnWebsite, bool chkIsMiscCollectionAlbum, bool isAdmin = false)
        {

            if (!int.TryParse(txtYear, out int year))
            {
                // Return to form with error or use ModelState
                return RedirectToAction("EditAlbum", new { albumCode = albumCode, isAdmin = isAdmin });
            }

            Album album = new()
            {
                AlbumCode = albumCode,
                ArtistName = txtArtist,
                AlbumName = txtAlbumName,
                AlbumYear = year,
                IsBassTab = chkIsBassTab,
                IncludeInIndex = chkIncludeInIndex,
                IncludeOnWebsite = chkIncludeOnWebsite,
                IsMiscCollectionAlbum = chkIsMiscCollectionAlbum
            };

            album = await _ServiceApiClient.SaveAlbum(album);

            if (albumCode == 0)
            {
                return RedirectToAction("Index", new
                {
                    isAdmin = isAdmin
                });
            }
            else
            {
                return RedirectToAction("Album", new
                {
                    albumCode = album.AlbumCode,
                    isAdmin = isAdmin
                });
            }
        }

        public async Task<IActionResult> EditTab(int tabCode, bool isAdmin = false)
        {

            Task<Tab> tabTask = _ServiceApiClient.GetTab(tabCode);
            Task<List<Rating>> ratingsTask = _ServiceApiClient.GetRatings();
            Task<List<Tuning>> tuningsTask = _ServiceApiClient.GetTunings();
            await Task.WhenAll(tabTask, ratingsTask, tuningsTask);

            //because we need all of the properties of Tab, we await here, instead of in the view return
            Tab tab = await tabTask;

            return View(new TabsViewModel(await ratingsTask, await tuningsTask)
            {
                Tab = tab,
                Rating = tab.Rating == null ? "" : tab.Rating.ToString(),
                Tuning = tab.TuningCode.ToString(),
                IsAdmin = isAdmin
            });
        }

        [HttpPost]
        public async Task<IActionResult> SaveTab(int tabCode, int albumCode, string txtTabName, string txtTabText, string txtOrder, string cboRating, string cboTuning, bool isAdmin = false)
        {
            // Safely parse numeric values from the request, falling back to 0 on invalid input
            int tabOrder = 0;
            int.TryParse(txtOrder, out tabOrder);

            int rating = 0; // 0 = no rating, consistent with AddNewTrack
            int.TryParse(cboRating, out rating);

            int tuningCode = 0; // 0 = no tuning, consistent with AddNewTrack
            int.TryParse(cboTuning, out tuningCode);

            Tab tab = new()
            {
                TabCode = tabCode,
                AlbumCode = albumCode,
                TabName = txtTabName,
                TabText = txtTabText,
                TabOrder = tabOrder,
                Rating = rating,
                TuningCode = tuningCode
            };
            await _ServiceApiClient.SaveTab(tab);

            return RedirectToAction("EditAlbum", new
            {
                albumCode = albumCode,
                isAdmin = isAdmin
            });
        }

        public async Task<IActionResult> AddNewTrack(int albumCode, bool isAdmin = false)
        {
            //Get the current list of tabs, to establish the last position of the new tab
            List<Tab> tabs = await _ServiceApiClient.GetTabs(albumCode);

            Tab tab = new()
            {
                TabCode = 0,
                AlbumCode = albumCode,
                TabName = $"Track {tabs.Count + 1}",
                TabText = "",
                TabOrder = (tabs.Count + 1),
                Rating = 0, // no rating
                TuningCode = 0 // no tuning
            };
            await _ServiceApiClient.SaveTab(tab);

            return RedirectToAction("EditAlbum", new
            {
                albumCode = albumCode,
                isAdmin = isAdmin
            });
        }

        public async Task<IActionResult> DeleteTab(int albumCode, int tabCode, bool isAdmin = false)
        {
            await _ServiceApiClient.DeleteTab(tabCode);

            return RedirectToAction("EditAlbum", new
            {
                albumCode = albumCode,
                isAdmin = isAdmin
            });
        }

    }
}
