using Microsoft.AspNetCore.Mvc;
using SamSmithNZ.Service.Models.Steam;
using SamSmithNZ.Web.Models.Steam;
using SamSmithNZ.Web.Services.Interfaces;
using System.Collections.Generic;
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
            // Parallelize independent API calls
            Task<Player> playerTask = _ServiceApiClient.GetPlayer(steamID);
            Task<List<Game>> gamesTask = _ServiceApiClient.GetPlayerGames(steamID);
            await Task.WhenAll(playerTask, gamesTask);

            Player player = await playerTask;
            List<Game> games = await gamesTask;

            return View(new IndexViewModel
            {
                SteamId = steamID,
                Player = player,
                Games = games
            });
        }

        public async Task<IActionResult> GameDetails(string steamID, string appID, bool showCompletedAchievements = false)
        {
            // Parallelize independent API calls
            Task<Player> playerTask = _ServiceApiClient.GetPlayer(steamID);
            Task<GameDetail> gameDetailTask = _ServiceApiClient.GetGameDetail(steamID, appID);
            await Task.WhenAll(playerTask, gameDetailTask);

            Player player = await playerTask;
            GameDetail gameDetail = await gameDetailTask;

            return View(new GameDetailViewModel(gameDetail, showCompletedAchievements)
            {
                SteamId = steamID,
                AppId = appID,
                Player = player
            });
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