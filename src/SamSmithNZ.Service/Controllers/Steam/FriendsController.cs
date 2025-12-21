//using Microsoft.AspNetCore.Mvc;
//using SamSmithNZ.Service.DataAccess.Steam;
//using SamSmithNZ.Service.Models.Steam;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace SamSmithNZ.Service.Controllers.Steam
//{
//    [Route("api/Steam/[controller]")]
//    [ApiController]
//    public class FriendsController : ControllerBase
//    {
//        public FriendsController()
//        { }

//        // GET
//        [HttpGet("GetFriends")]
//        public async Task<List<Friend>> GetFriends(string steamId)
//        {
//            if (string.IsNullOrEmpty(steamId) == false)
//            {
//                FriendsDA da = new();
//                return await da.GetDataAsync(steamId);
//            }
//            else
//            {
//                return null;
//            }
//        }
//    }
//}
