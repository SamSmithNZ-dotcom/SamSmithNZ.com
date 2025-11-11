using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithNZ.Service.DataAccess.Steam;
using SamSmithNZ.Service.Models.Steam;
using System.Threading.Tasks;

namespace SamSmithNZ.Tests.Steam
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class SteamOwnedGamesTest
    {
        [TestMethod]
        public async Task SteamOwnedGamesExistTest()
        {
            //Arrange
            string steamId = "76561197971691578";

            //Act
            SteamOwnedGamesDA da = new();
            SteamOwnedGames result = await da.GetDataAsync(steamId);

            //Asset
            Assert.IsTrue(result != null);
            Assert.IsTrue(result.response.game_count >= 0);
            Assert.IsTrue(result.response.games != null);
            Assert.IsTrue(result.response.games.Count >= 1);
            Assert.IsTrue(result.response.games[0].appid == "50");
            Assert.IsTrue(result.response.games[0].name == "Half-Life: Opposing Force");
            Assert.IsTrue(result.response.games[0].img_icon_url == "04e81206c10e12416908c72c5f22aad411b3aeef");
            //Assert.IsTrue(result.response.games[0].img_logo_url == "e4ad9cf1b7dc8475c1118625daf9abd4bdcbcad0");
            Assert.IsTrue(result.response.games[0].playtime_forever >= 273);
            Assert.IsTrue(result.response.games[0].has_community_visible_stats == false);

        }
    }
}
