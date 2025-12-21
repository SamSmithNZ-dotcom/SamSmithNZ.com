//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using SamSmithNZ.Service.DataAccess.Steam;
//using SamSmithNZ.Service.Models.Steam;
//using System.Threading.Tasks;

//namespace SamSmithNZ.Tests.Steam
//{
//    [TestClass]
//    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
//    public class SteamPlayerAchievementsForAppUnitTest
//    {
//        [TestMethod]
//        public async Task SteamPlayerAchievementsForAppExistUnitTest()
//        {
//            //Arrange
//            string appId = "200510"; //XCOM
//            string steamId = "76561197971691578";

//            //Act
//            SteamPlayerAchievementsForAppDA da = new();
//            Tuple<SteamPlayerAchievementsForApp, SteamPlayerAchievementsForAppError> result = await da.GetDataAsync(steamId, appId);

//            //Asset
//            Assert.IsTrue(result != null);
//            Assert.IsTrue(result.Item1 != null);
//            Assert.IsTrue(result.Item2 == null);
//            Assert.IsTrue(result.Item1.playerstats != null);
//            Assert.IsTrue(result.Item1.playerstats.steamID == "76561197971691578");
//            Assert.IsTrue(result.Item1.playerstats.gameName == "XCOM: Enemy Unknown");
//            Assert.IsTrue(result.Item1.playerstats.achievements != null);
//            Assert.IsTrue(result.Item1.playerstats.achievements.Count >= 1);
//            Assert.IsTrue(result.Item1.playerstats.achievements[0].apiname == "ACHIEVEMENT_0");
//            Assert.IsTrue(result.Item1.playerstats.achievements[0].achieved == 1);
//            Assert.IsTrue(result.Item1.playerstats.achievements[0].name == "No Looking Back");
//            Assert.IsTrue(result.Item1.playerstats.achievements[0].description == "Beat the game in Ironman mode on Classic or Impossible Difficulty.");
//            Assert.IsTrue(result.Item1.playerstats.success == true);

//            //<playerstats>
//            //    <steamID>76561197971691578</steamID>
//            //    <gameName>XCOM: Enemy Unknown</gameName>
//            //    <achievements>
//            //      <achievement>
//            //          <apiname>ACHIEVEMENT_0</apiname>
//            //          <achieved>1</achieved>
//            //          <name>No Looking Back</name>
//            //          <description>Beat the game in Ironman mode on Classic or Impossible Difficulty.</description>
//            //      </achievement>
//            //    </achievements>
//            //    <success>true</success>
//            //</playerstats>
//        }

//        //[TestMethod]
//        //public async Task SteamPlayerAchievementsAlexPrivateProfileForAppExistUnitTest()
//        //{
//        //    //Arrange
//        //    string appId = "289070"; //Civ 6
//        //    string steamId = "76561198034342716";

//        //    //Act
//        //    SteamPlayerAchievementsForAppDA da = new();
//        //    Tuple<SteamPlayerAchievementsForApp, SteamPlayerAchievementsForAppError> result = await da.GetDataAsync(steamId, appId);

//        //    //Asset
//        //    Assert.IsTrue(result != null);
//        //    Assert.IsTrue(result.Item1 == null);
//        //    Assert.IsTrue(result.Item2 != null);
//        //    Assert.IsTrue(result.Item2.playerstats != null);
//        //    Assert.IsTrue(result.Item2.playerstats.error.Length > 0);
//        //    Assert.IsTrue(result.Item2.playerstats.success == false);
//        //    //Assert.IsTrue(result.Item1.playerstats != null);
//        //    //Assert.IsTrue(result.Item1.playerstats.steamID == "76561197971691578");
//        //    //Assert.IsTrue(result.Item1.playerstats.gameName == "XCOM: Enemy Unknown");
//        //    //Assert.IsTrue(result.Item1.playerstats.achievements != null);
//        //    //Assert.IsTrue(result.Item1.playerstats.achievements.Count >= 1);
//        //    //Assert.IsTrue(result.Item1.playerstats.achievements[0].apiname == "ACHIEVEMENT_0");
//        //    //Assert.IsTrue(result.Item1.playerstats.achievements[0].achieved == 1);
//        //    //Assert.IsTrue(result.Item1.playerstats.achievements[0].name == "No Looking Back");
//        //    //Assert.IsTrue(result.Item1.playerstats.achievements[0].description == "Beat the game in Ironman mode on Classic or Impossible Difficulty.");
//        //    //Assert.IsTrue(result.Item1.playerstats.success == true);

//        //    //<playerstats>
//        //    //    <steamID>76561197971691578</steamID>
//        //    //    <gameName>XCOM: Enemy Unknown</gameName>
//        //    //    <achievements>
//        //    //      <achievement>
//        //    //          <apiname>ACHIEVEMENT_0</apiname>
//        //    //          <achieved>1</achieved>
//        //    //          <name>No Looking Back</name>
//        //    //          <description>Beat the game in Ironman mode on Classic or Impossible Difficulty.</description>
//        //    //      </achievement>
//        //    //    </achievements>
//        //    //    <success>true</success>
//        //    //</playerstats>
//        //}
//    }
//}
