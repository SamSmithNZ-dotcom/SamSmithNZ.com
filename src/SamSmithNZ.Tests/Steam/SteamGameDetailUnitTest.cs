//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using SamSmithNZ.Service.DataAccess.Steam;
//using SamSmithNZ.Service.Models.Steam;
//using System.Threading.Tasks;

//namespace SamSmithNZ.Tests.Steam
//{
//    [TestClass]
//    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
//    public class SteamGameDetailUnitTest
//    {
//        [TestMethod]
//        public async Task SteamGameDetailExistUnitTest()
//        {
//            //Arrange
//            string appId = "200510"; //XCOM

//            //Act
//            SteamGameDetailDA da = new();
//            SteamGameDetail result = await da.GetDataAsync(appId);

//            //Asset
//            Assert.IsTrue(result != null);
//            Assert.IsTrue(result.game != null);
//            Assert.IsTrue(result.game.gameName == "ValveTestApp200510");
//            Assert.IsTrue(result.game.gameVersion >= 67);
//            Assert.IsTrue(result.game.availableGameStats != null);
//            Assert.IsTrue(result.game.availableGameStats.achievements != null);
//            Assert.IsTrue(result.game.availableGameStats.achievements.Count >= 1);
//            Assert.IsTrue(result.game.availableGameStats.achievements[0].name == "ACHIEVEMENT_0");
//            Assert.IsTrue(result.game.availableGameStats.achievements[0].defaultvalue == 0);
//            Assert.IsTrue(result.game.availableGameStats.achievements[0].displayName == "No Looking Back");
//            Assert.IsTrue(result.game.availableGameStats.achievements[0].hidden == 0);
//            Assert.IsTrue(result.game.availableGameStats.achievements[0].description == "Beat the game in Ironman mode on Classic or Impossible Difficulty.");
//            Assert.IsTrue(result.game.availableGameStats.achievements[0].icon != "");
//            Assert.IsTrue(result.game.availableGameStats.achievements[0].icongray != "");

//            //<game>
//            //  <gameName>ValveTestApp200510</gameName>
//            //  <gameVersion>67</gameVersion>
//            //  <availableGameStats>
//            //      <achievements>
//            //          <achievement>
//            //              <name>ACHIEVEMENT_0</name>
//            //              <defaultvalue>0</defaultvalue>
//            //              <displayName>No Looking Back</displayName>
//            //              <hidden>0</hidden>
//            //              <description>Beat the game in Ironman mode on Classic or Impossible Difficulty.</description>
//            //              <icon>
//            //              http://media.steampowered.com/steamcommunity/public/images/apps/200510/9ea8b01a66eb7af4d074430bbb3300414b7d457d.jpg
//            //              </icon>
//            //              <icongray>
//            //              http://media.steampowered.com/steamcommunity/public/images/apps/200510/7dd89a5070fa8f8a07d9859c12a888a2885929ca.jpg
//            //              </icongray>
//            //          </achievement>
//            //      </achievements>
//            //  </availableGameStats>
//            //</game>

//        }
//    }
//}
