using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithNZ.Service.Controllers.Steam;
using SamSmithNZ.Service.Models.Steam;
using StackExchange.Redis;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SamSmithNZ.Tests.Steam
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class PlayerGamesControllerTest
    {
        //private static RedisService ;

        [ClassInitialize]
        public static void InitTestSuite(TestContext testContext)
        {
            _ = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            //string connectionString = config["CacheConnection"];
            //ConnectionMultiplexer cache = ConnectionMultiplexer.Connect(connectionString);
            //IDatabase db = cache.GetDatabase();
            // = new RedisService(db);
        }

        //[TestMethod]
        //public async Task PlayerGamesSamTest()
        //{
        //    //Arrange
        //    PlayerGamesController controller = new();            
        //    string steamId = "76561197971691578";

        //    //Act
        //    List<Game> results = await controller.GetPlayerGames(steamId);

        //    //Assert
        //    Assert.IsTrue(results != null);
        //    Assert.IsTrue(results.Count >= 1);
        //    Assert.IsTrue(results[2].AppID == "15100");
        //    Assert.IsTrue(results[2].GameName == "Assassin's Creed");
        //    Assert.IsTrue(results[2].CommunityIsVisible == false); ;
        //    Assert.IsTrue(results[2].IconURL == "cd8f7a795e34e16449f7ad8d8190dce521967917");
        //    //Assert.IsTrue(results[1].LogoURL == "5450218e6f8ea246272cddcb2ab9a453b0ca7ef5");
        //    Assert.IsTrue(results[2].TotalMinutesPlayed == 185);
        //    Assert.IsTrue(results[2].TotalTimeString == "3 hrs");
        //}

        //[TestMethod]
        //public async Task PlayerGamesSamWithoutCacheTest()
        //{
        //    //Arrange
        //    PlayerGamesController controller = new();
        //    string steamId = "76561197971691578";

        //    //Act
        //    List<Game> results = await controller.GetPlayerGames(steamId);

        //    //Assert
        //    Assert.IsTrue(results != null);
        //    Assert.IsTrue(results.Count >= 0);
        //    Assert.IsTrue(results[2].AppID == "15100");
        //    Assert.IsTrue(results[2].GameName == "Assassin's Creed");
        //    Assert.IsTrue(results[2].CommunityIsVisible == false);
        //    Assert.IsTrue(results[2].IconURL == "cd8f7a795e34e16449f7ad8d8190dce521967917");
        //    //Assert.IsTrue(results[2].LogoURL == "5450218e6f8ea246272cddcb2ab9a453b0ca7ef5");
        //    Assert.IsTrue(results[2].TotalMinutesPlayed == 185);
        //    Assert.IsTrue(results[2].TotalTimeString == "3 hrs");
        //}

        [TestMethod]
        public async Task PlayerGamesSamWithNoIconTest()
        {
            //Arrange
            PlayerGamesController controller = new();
            string steamId = "76561197971691578";
            string appId = "223530";

            //Act
            List<Game> results = await controller.GetPlayerGames(steamId);

            //Assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count >= 0);
            foreach (Game item in results)
            {
                if (item.AppID == appId)
                {
                    Assert.IsTrue(item.AppID == "223530");
                    Assert.IsTrue(item.GameName == "Left 4 Dead 2 Beta");
                    Assert.IsTrue(item.CommunityIsVisible == false);
                    Assert.IsTrue(item.IconURL == null);
                    Assert.IsTrue(item.LogoURL == "");
                    Assert.IsTrue(item.TotalMinutesPlayed == 0);
                    Assert.IsTrue(item.TotalTimeString == "0 hrs");
                    break;
                }
            }

        }

        [TestMethod]
        public async Task PlayerGamesRandomWithNoGamesTest()
        {
            //Arrange
            PlayerGamesController controller = new();
            string steamId = "76561198059077520";

            //Act
            List<Game> results = await controller.GetPlayerGames(steamId);

            //Assert
            Assert.IsTrue(results != null);
            Assert.AreEqual(0, results.Count);

        }

        [TestMethod]
        public async Task PlayerGamesRandomAnotherWithNoGamesTest()
        {
            //Arrange
            PlayerGamesController controller = new();
            string steamId = "76561198121979762";

            //Act
            List<Game> results = await controller.GetPlayerGames(steamId);

            //Assert
            Assert.IsTrue(results != null);
            Assert.AreEqual(2, results.Count);

        }
        
    }
}