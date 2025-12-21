//using Microsoft.AspNetCore.Mvc;
//using SamSmithNZ.Service.DataAccess.Steam;
//using SamSmithNZ.Service.Models.Steam;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace SamSmithNZ.Service.Controllers.Steam
//{
//    [Route("api/Steam/[controller]")]
//    [ApiController]
//    public class PlayerGamesController : ControllerBase
//    {
//        public PlayerGamesController()
//        {
            
//        }

//        // GET
//        [HttpGet("GetPlayerGames")]
//        public async Task<List<Game>> GetPlayerGames(string steamID)
//        {
//            if (string.IsNullOrEmpty(steamID) == false)
//            {
//                PlayerGamesDA da = new();
//                return await da.GetDataAsync(steamID);
//            }
//            else
//            {
//                return null;
//            }
//        }
//    }
//}
