//using Microsoft.AspNetCore.Mvc;
//using SamSmithNZ.Service.DataAccess.Steam;
//using SamSmithNZ.Service.Models.Steam;
//using System.Threading.Tasks;

//namespace SamSmithNZ.Service.Controllers.Steam
//{
//    [Route("api/Steam/[controller]")]
//    [ApiController]
//    public class GameDetailsController : ControllerBase
//    {    
//        public GameDetailsController()
//        {
            
//        }

//        // GET
//        [HttpGet("GetGameDetails")]
//        public async Task<GameDetail> GetGameDetails(string steamID, string appID, bool getStats = true, string achievementToSearch = null)
//        {
//            if (string.IsNullOrEmpty(steamID) == false && string.IsNullOrEmpty(appID) == false)
//            {
//                GameDetailsDA da = new();
//                return await da.GetDataAsync(steamID, appID, getStats, achievementToSearch);
//            }
//            else
//            {
//                return null;
//            }
//        }

//        //// GET
//        //[HttpGet("GetGameWithFriendDetails")]
//        //public async Task<GameDetail> GetGameWithFriendDetails(string steamID, string appID, string friendSteamId, bool getStats = true, string achievementToSearch = null)
//        //{
//        //    GameDetailsDA da = new();
//        //    return await da.GetDataWithFriendAsync(steamID, appID, friendSteamId, getStats, achievementToSearch);
//        //}
//    }
//}
