//using Microsoft.AspNetCore.Mvc;
//using SamSmithNZ.Service.DataAccess.Steam;
//using SamSmithNZ.Service.Models.Steam;
//using System.Threading.Tasks;

//namespace SamSmithNZ.Service.Controllers.Steam
//{
//    [Route("api/Steam/[controller]")]
//    [ApiController]
//    public class PlayerController : ControllerBase
//    {
//        public PlayerController()
//        {
            
//        }

//        // GET
//        [HttpGet("GetPlayer")]
//        public async Task<Player> GetPlayer(string steamID)
//        {
//            if (string.IsNullOrEmpty(steamID) == false)
//            {
//                PlayerDA da = new();
//                return await da.GetDataAsync(steamID);
//            }
//            else
//            {
//                return null;
//            }
//        }
//    }
//}
