//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using SamSmithNZ.Service.DataAccess.Steam;
//using SamSmithNZ.Service.Models.Steam;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace SamSmithNZ.Tests.Steam
//{
//    [TestClass]
//    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
//    public class FriendsTest
//    {

//        //[TestMethod]
//        //public async Task FriendsSamFirstTest()
//        //{
//        //    //Arrange
//        //    FriendsDA da = new();
//        //    string steamId = "76561197971691578";

//        //    //Act
//        //    List<Friend> results = await da.GetDataAsync(steamId);

//        //    //Assert
//        //    Assert.IsTrue(results != null);
//        //    Assert.IsTrue(results.Count >= 0);
//        //    Friend result = null;
//        //    foreach (Friend item in results)
//        //    {
//        //        if (item.SteamId == "76561198034342716")
//        //        {
//        //            result = item;
//        //            break;
//        //        }
//        //    }
//        //    Assert.IsTrue(result != null);
//        //    Assert.IsTrue(result.SteamId == "76561198034342716");
//        //    Assert.IsTrue(result.Name == "Alex");
//        //    Assert.IsTrue(result.Avatar != "");
//        //    Assert.IsTrue(result.AvatarFull != "");
//        //    Assert.IsTrue(result.AvatarMedium != "");
//        //    Assert.IsTrue(result.FriendSince >= 0);
//        //    Assert.IsTrue(result.LastLogoff >= 0);
//        //    Assert.IsTrue(result.ProfileURL != "");
//        //    Assert.IsTrue(result.TimeCreated >= 0);
//        //}

//        [TestMethod]
//        public async Task FriendsAlexTest()
//        {
//            //Arrange
//            FriendsDA da = new();
//            string steamId = "76561198034342716";

//            //Act
//            List<Friend> results = await da.GetDataAsync(steamId);

//            //Assert
//            Assert.IsTrue(results != null);
//            Assert.IsTrue(results.Count >= 0);
//            //Friend result = null;
//            //foreach (Friend item in results)
//            //{
//            //    if (item.SteamId == "76561197971691578") //Sam
//            //    {
//            //        result = item;
//            //        break;
//            //    }
//            //}
//            //Assert.IsTrue(result != null);
//            //Assert.IsTrue(result.SteamId == "76561197971691578");
//            //Assert.IsTrue(result.Name == "Sam");
//        }

//        [TestMethod]
//        public async Task FriendsRandomTest()
//        {
//            //Arrange
//            FriendsDA da = new();
//            string steamId = "76561198154034472";

//            //Act
//            List<Friend> results = await da.GetDataAsync(steamId);

//            //Assert
//            Assert.IsTrue(results != null);
//            Assert.IsTrue(results.Count >= 0);
//        }

//        //[TestMethod]
//        //public async Task FriendsRandomWithMoreThan100FriendsTest()
//        //{
//        //    //Arrange
//        //    FriendsDA da = new();
//        //    string steamId = "76561198129345768";

//        //    //Act
//        //    List<Friend> results = await da.GetDataAsync(steamId);

//        //    //Assert
//        //    Assert.IsTrue(results != null);
//        //    Assert.IsTrue(results.Count > 100);
//        //}

//        //[TestMethod]
//        //public async Task FriendsRandomWithExactly99FriendsTest()
//        //{
//        //    //Arrange
//        //    FriendsDA da = new();
//        //    string steamId = "76561198140300853";

//        //    //Act
//        //    List<Friend> results = await da.GetDataAsync(steamId);

//        //    //Assert
//        //    Assert.IsTrue(results != null);
//        //    Assert.IsTrue(results.Count >= 0);
//        //}

//        //[TestMethod]
//        //public async Task FriendsSameGameSamXcomTest()
//        //{
//        //    //Arrange
//        //    FriendsDA da = new();
//        //    string steamId = "76561197971691578";
//        //    string appId = "200510"; //Xcom

//        //    //Act
//        //    List<Friend> results = await da.GetFriendsWithSameGame(null, steamId, appId, false);

//        //    //Assert
//        //    Assert.IsTrue(results != null);
//        //    Assert.IsTrue(results.Count >= 0);
//        //    Friend result = null;
//        //    foreach (Friend item in results)
//        //    {
//        //        if (item.SteamId == "76561197990013217") //Stew
//        //        {
//        //            result = item;
//        //            break;
//        //        }
//        //    }
//        //    Assert.IsTrue(result != null);
//        //    Assert.IsTrue(result.SteamId == "76561197990013217");
//        //    Assert.IsTrue(result.Name == "Captain Datsun");

//        //}

//        //[TestMethod]
//        //public async Task FriendsSameGameSamCiv6Test()
//        //{
//        //    //Arrange
//        //    FriendsDA da = new();
//        //    string steamId = "76561197971691578";
//        //    string appId = "289070"; //Civ 6

//        //    //Act
//        //    List<Friend> results = await da.GetFriendsWithSameGame(null, steamId, appId,false);

//        //    //Assert
//        //    Assert.IsTrue(results != null);
//        //    Assert.IsTrue(results.Count >= 1);
//        //    Friend result = null;
//        //    foreach (Friend item in results)
//        //    {
//        //        if (item.SteamId == "76561198030842184") //Coleman
//        //        {
//        //            result = item;
//        //        }
//        //        //else if (item.SteamId == "76561198034342716") //Alex
//        //        //{
//        //        //    Assert.IsTrue(false); //This should never happen, Alex has a private profile
//        //        //}
//        //    }
//        //    Assert.IsTrue(result != null);
//        //    Assert.IsTrue(result.SteamId == "76561198030842184");
//        //    Assert.IsTrue(result.Name == "coleman.yeaw");

//        //}

//        //[TestMethod]
//        //public async Task FriendsSameGameStewXcomTest()
//        //{
//        //    //Arrange
//        //    FriendsDA da = new();
//        //    string steamId = "76561197990013217";
//        //    string appId = "200510"; //Xcom

//        //    //Act
//        //    List<Friend> results = await da.GetFriendsWithSameGame(null, steamId, appId,false);

//        //    //Assert
//        //    Assert.IsTrue(results != null);
//        //    Assert.IsTrue(results.Count >= 0);
//        //    Friend result = null;
//        //    foreach (Friend item in results)
//        //    {
//        //        if (item.SteamId == "76561197971691578") //Sam
//        //        {
//        //            result = item;
//        //            break;
//        //        }
//        //    }
//        //    Assert.IsTrue(result != null);
//        //    Assert.IsTrue(result.SteamId == "76561197971691578");
//        //    Assert.IsTrue(result.Name == "Sam");
//        //}


//    }
//}
