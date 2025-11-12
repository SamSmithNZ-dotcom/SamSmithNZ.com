using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithNZ.Service.DataAccess.Steam;
using SamSmithNZ.Service.Models.Steam;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Tests.Steam
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class PlayerGamesTest
    {

        //[TestMethod]
        //public async Task PlayerGamesSamTest()
        //{
        //    //Arrange
        //    PlayerGamesDA da = new();
        //    string steamId = "76561197971691578";

        //    //Act
        //    List<Game> results = await da.GetDataAsync(steamId);

        //    //Assert
        //    Assert.IsTrue(results != null);
        //    Assert.IsTrue(results.Count >= 1);
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
            PlayerGamesDA da = new();
            string steamId = "76561197971691578";
            string appId = "223530";

            //Act
            List<Game> results = await da.GetDataAsync(steamId);

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
            PlayerGamesDA da = new();
            string steamId = "76561198059077520";

            //Act
            List<Game> results = await da.GetDataAsync(steamId);

            //Assert
            Assert.IsTrue(results != null);
            Assert.AreEqual(0, results.Count);

        }

        [TestMethod]
        public async Task PlayerGamesRandomAnotherWithNoGamesTest()
        {
            //Arrange
            PlayerGamesDA da = new();
            string steamId = "76561198121979762";

            //Act
            List<Game> results = await da.GetDataAsync(steamId);

            //Assert
            Assert.IsTrue(results != null);
            Assert.AreEqual(2, results.Count);

        }
        
    }
}