//using Microsoft.Extensions.Configuration;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using SamSmithNZ.Service.Controllers.Steam;
//using SamSmithNZ.Service.Models.Steam;
//using StackExchange.Redis;
//using System.IO;
//using System.Threading.Tasks;

//namespace SamSmithNZ.Tests.Steam
//{
//    [TestClass]
//    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
//    public class PlayerControllerTest
//    {
//        //private static RedisService ;

//        [ClassInitialize]
//        public static void InitTestSuite(TestContext testContext)
//        {
//            _ = new ConfigurationBuilder()
//                .SetBasePath(Directory.GetCurrentDirectory())
//                .AddJsonFile("appsettings.json")
//                .Build();

//            //string connectionString = config["CacheConnection"];
//            //ConnectionMultiplexer cache = ConnectionMultiplexer.Connect(connectionString);
//            //IDatabase db = cache.GetDatabase();
//            // = new RedisService(db);
//        }

//        ////Currently runs in 688ms
//        //[TestMethod]
//        //public async Task PlayerControllerSamTest()
//        //{
//        //    //Arrange
//        //    PlayerController controller = new();
//        //    string steamId = "76561197971691578";

//        //    //Act
//        //    Player result = await controller.GetPlayer(steamId);

//        //    //Assert
//        //    Assert.IsTrue(result != null);
//        //    Assert.IsTrue(result.SteamID == "76561197971691578");
//        //    Assert.IsTrue(result.PlayerName == "Sam");
//        //    Assert.IsTrue(result.IsPublic == true);
//        //}

//        //[TestMethod]
//        //public async Task PlayerControllerSamWithoutCacheTest()
//        //{
//        //    //Arrange
//        //    PlayerController controller = new();
//        //    string steamId = "76561197971691578";

//        //    //Act
//        //    Player result = await controller.GetPlayer(steamId);

//        //    //Assert
//        //    Assert.IsTrue(result != null);
//        //    Assert.IsTrue(result.SteamID == "76561197971691578");
//        //    Assert.IsTrue(result.PlayerName == "Sam");
//        //    Assert.IsTrue(result.IsPublic == true);
//        //}


//    }
//}
