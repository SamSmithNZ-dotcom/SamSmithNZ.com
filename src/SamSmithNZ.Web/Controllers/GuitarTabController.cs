using Microsoft.AspNetCore.Mvc;
using SamSmithNZ.Service.Models.GuitarTab;
using SamSmithNZ.Web.Models.GuitarTab;
using SamSmithNZ.Web.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArtistAlbumsEntry = System.Collections.Generic.KeyValuePair<SamSmithNZ.Service.Models.GuitarTab.Artist, System.Collections.Generic.List<SamSmithNZ.Service.Models.GuitarTab.Album>>;

namespace SamSmithNZ.Web.Controllers
{
    public class GuitarTabController : Controller
    {
        private readonly IGuitarTabServiceApiClient _serviceApiClient;

        public GuitarTabController(IGuitarTabServiceApiClient serviceApiClient)
        {
            _serviceApiClient = serviceApiClient;
        }

        public async Task<IActionResult> Index(bool isAdmin = false)
        {
            // Execute both API calls in parallel
            Task<List<Artist>> artistsTask = _serviceApiClient.GetArtists(isAdmin);
            Task<List<Album>> albumsTask = _serviceApiClient.GetAlbums(isAdmin);
            await Task.WhenAll(artistsTask, albumsTask);

            List<Artist> artists = await artistsTask;
            List<Album> albums = await albumsTask;

            // Use ToLookup for O(1) lookups instead of O(n) per artist
            ILookup<string, Album> albumLookup = albums.ToLookup(a => a.ArtistName);

            // Combine artists and their albums
            List<ArtistAlbumsEntry> items = new();
            foreach (Artist artist in artists)
            {
                List<Album> artistAlbums = albumLookup[artist.ArtistName].ToList();
                items.Add(new ArtistAlbumsEntry(artist, artistAlbums));
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
            Task<Album> albumTask = _serviceApiClient.GetAlbum(albumCode, isAdmin);
            Task<List<Tab>> tabsTask = _serviceApiClient.GetTabs(albumCode);
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
            List<Search> searchResults = await _serviceApiClient.GetSearchResults(searchText);

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
            Task<Album> albumTask = _serviceApiClient.GetAlbum(albumCode, true);
            Task<List<Tab>> tabsTask = _serviceApiClient.GetTabs(albumCode);
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

            album = await _serviceApiClient.SaveAlbum(album);

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

            Task<Tab> tabTask = _serviceApiClient.GetTab(tabCode);
            Task<List<Rating>> ratingsTask = _serviceApiClient.GetRatings();
            Task<List<Tuning>> tuningsTask = _serviceApiClient.GetTunings();
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
            // Safely parse numeric values from the request, validating txtOrder explicitly
            int tabOrder = 0;
            bool isTabOrderValid = int.TryParse(txtOrder, out tabOrder);

            int rating = 0; // 0 = no rating, consistent with AddNewTrack
            if (!int.TryParse(cboRating, out rating))
            {
                // On invalid input, fall back to "no rating"
                rating = 0;
            }

            int tuningCode = 0; // 0 = no tuning, consistent with AddNewTrack
            if (!int.TryParse(cboTuning, out tuningCode))
            {
                // If parsing fails, fall back to "no tuning" (0), consistent with existing behavior.
                tuningCode = 0;
            }

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

            if (!isTabOrderValid)
            {
                ModelState.AddModelError(nameof(txtOrder), "Tab order must be a whole number.");

                // Rebuild the view model in the same way as EditTab to redisplay the form with errors
                Task<List<Rating>> ratingsTask = _serviceApiClient.GetRatings();
                Task<List<Tuning>> tuningsTask = _serviceApiClient.GetTunings();
                await Task.WhenAll(ratingsTask, tuningsTask);

                // Create a new tab object with the submitted values to redisplay the form
                Tab tabForDisplay = new()
                {
                    TabCode = tabCode,
                    AlbumCode = albumCode,
                    TabName = txtTabName,
                    TabText = txtTabText,
                    TabOrder = tabOrder, // remains 0 when invalid, but shown back to user
                    Rating = rating,
                    TuningCode = tuningCode
                };

                TabsViewModel viewModel = new TabsViewModel(await ratingsTask, await tuningsTask)
                {
                    Tab = tabForDisplay,
                    Rating = rating == 0 ? "" : rating.ToString(),
                    Tuning = tuningCode == 0 ? "" : tuningCode.ToString(),
                    IsAdmin = isAdmin
                };

                return View("EditTab", viewModel);
            }

            await _serviceApiClient.SaveTab(tab);

            return RedirectToAction("EditAlbum", new
            {
                albumCode = albumCode,
                isAdmin = isAdmin
            });
        }

        public async Task<IActionResult> AddNewTrack(int albumCode, bool isAdmin = false)
        {
            //Get the current list of tabs, to establish the last position of the new tab
            List<Tab> tabs = await _serviceApiClient.GetTabs(albumCode);

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
            await _serviceApiClient.SaveTab(tab);

            return RedirectToAction("EditAlbum", new
            {
                albumCode = albumCode,
                isAdmin = isAdmin
            });
        }

        public async Task<IActionResult> DeleteTab(int albumCode, int tabCode, bool isAdmin = false)
        {
            await _serviceApiClient.DeleteTab(tabCode);

            return RedirectToAction("EditAlbum", new
            {
                albumCode = albumCode,
                isAdmin = isAdmin
            });
        }

    }
}
