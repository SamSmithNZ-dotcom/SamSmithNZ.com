using Microsoft.AspNetCore.Mvc;
using SamSmithNZ.Web.Services.Interfaces;
using System.Threading.Tasks;

namespace SamSmithNZ.Web.Controllers
{
    public class SteamController : Controller
    {
        //https://steamcommunity.com/dev
        //https://developer.valvesoftware.com/wiki/Steam_Web_API#GetPlayerSummaries_.2v0001.29
        //https://portablesteamwebapi.codeplex.com/documentation

        private readonly ISteamServiceApiClient _ServiceApiClient;

        public SteamController(ISteamServiceApiClient serviceApiClient)
        {
            _ServiceApiClient = serviceApiClient;
        }

        public async Task<IActionResult> Index(string steamID)
        {

            //Player player = await _ServiceApiClient.GetPlayer(steamID);
            //List<Game> games = await _ServiceApiClient.GetPlayerGames(steamID);

            //return View(new IndexViewModel
            //{
            //    SteamId = steamID,
            //    Player = player,
            //    Games = games
            //});
            return View();
        }

        public async Task<IActionResult> GameDetails(string steamID, string appID, bool showCompletedAchievements = false)
        {


            return View("Index");

            //Player player = await _ServiceApiClient.GetPlayer(steamID);
            //GameDetail gameDetail = await _ServiceApiClient.GetGameDetail(steamID, appID);

            //return View(new GameDetailViewModel(gameDetail, showCompletedAchievements)
            //{
            //    SteamId = steamID,
            //    AppId = appID,
            //    Player = player
            //});
        }

        //[HttpGet]
        //[HttpPost]
        //public IActionResult GameDetailsPost(string steamID, string appID, bool showCompletedAchievements = false)
        //{
        //    return RedirectToAction("GameDetails",
        //        new
        //        {
        //            steamID = steamID,
        //            appID = appID,
        //            showCompletedAchievements = showCompletedAchievements
        //        });
        //}

        public IActionResult SteamIsDown()
        {
            return View();
        }

        public IActionResult About()
        {
            return RedirectToAction("About", "Home");
        }

    }
}