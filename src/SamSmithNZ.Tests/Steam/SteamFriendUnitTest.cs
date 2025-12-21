//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using SamSmithNZ.Service.DataAccess.Steam;
//using SamSmithNZ.Service.Models.Steam;
//using System.Threading.Tasks;

//namespace SamSmithNZ.Tests.Steam
//{
//    [TestClass]
//    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
//    public class SteamFriendUnitTest
//    {
//        [TestMethod]
//        public async Task SteamFriendsExistTest()
//        {
//            //Arrange
//            string steamId = "76561197971691578";

//            //Act
//            SteamFriendDA da = new();
//            SteamFriendList result = await da.GetDataAsync(steamId);

//            //Asset
//            Assert.IsTrue(result != null);
//            Assert.IsTrue(result.friendslist != null);
//            Assert.IsTrue(result.friendslist.friends != null);
//            Assert.IsTrue(result.friendslist.friends.Count >= 0);
//            Assert.IsTrue(result.friendslist.friends.Count >= 1);
//            Assert.IsTrue(result.friendslist.friends[0].steamid != "");
//            Assert.IsTrue(result.friendslist.friends[0].relationship != "");
//            Assert.IsTrue(result.friendslist.friends[0].friend_since >= 0);
//        }
//    }
//}
