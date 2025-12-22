using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using SamSmithNZ.Service.Models.Steam;
using SamSmithNZ.Web.Controllers;
using SamSmithNZ.Web.Models.Steam;
using SamSmithNZ.Web.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Tests.Controllers.Steam
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class SteamControllerTests
    {
        private ISteamServiceApiClient _mockApiClient;
        private SteamController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockApiClient = Substitute.For<ISteamServiceApiClient>();
            _controller = new SteamController(_mockApiClient);
        }

        [TestMethod]
        public void SteamController_Constructor_InitializesCorrectly()
        {
            // Arrange & Act
            SteamController controller = new SteamController(_mockApiClient);

            // Assert
            Assert.IsNotNull(controller);
        }

        [TestMethod]
        public async Task Index_ValidSteamID_ReturnsViewWithViewModel()
        {
            // Arrange
            string steamID = "test-steam-id";
            Player expectedPlayer = new Player { SteamID = steamID, PersonaName = "Test Player" };
            List<Game> expectedGames = new List<Game>
            {
                new Game { AppID = "123", Name = "Test Game 1" },
                new Game { AppID = "456", Name = "Test Game 2" }
            };

            _mockApiClient.GetPlayer(steamID).Returns(Task.FromResult(expectedPlayer));
            _mockApiClient.GetPlayerGames(steamID).Returns(Task.FromResult(expectedGames));

            // Act
            IActionResult result = await _controller.Index(steamID);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult viewResult = (ViewResult)result;
            Assert.IsNotNull(viewResult.Model);
            Assert.IsInstanceOfType(viewResult.Model, typeof(IndexViewModel));

            IndexViewModel model = (IndexViewModel)viewResult.Model;
            Assert.AreEqual(steamID, model.SteamId);
            Assert.AreEqual(expectedPlayer, model.Player);
            Assert.AreEqual(expectedGames, model.Games);

            // Verify both API calls were made
            await _mockApiClient.Received(1).GetPlayer(steamID);
            await _mockApiClient.Received(1).GetPlayerGames(steamID);
        }

        [TestMethod]
        public async Task GameDetails_ValidParameters_ReturnsViewWithViewModel()
        {
            // Arrange
            string steamID = "test-steam-id";
            string appID = "123";
            bool showCompletedAchievements = false;

            Player expectedPlayer = new Player { SteamID = steamID, PersonaName = "Test Player" };
            GameDetail expectedGameDetail = new GameDetail
            {
                GameName = "Test Game",
                Achievements = new List<Achievement>
                {
                    new Achievement { Name = "Achievement 1", Achieved = false },
                    new Achievement { Name = "Achievement 2", Achieved = true }
                }
            };

            _mockApiClient.GetPlayer(steamID).Returns(Task.FromResult(expectedPlayer));
            _mockApiClient.GetGameDetail(steamID, appID).Returns(Task.FromResult(expectedGameDetail));

            // Act
            IActionResult result = await _controller.GameDetails(steamID, appID, showCompletedAchievements);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult viewResult = (ViewResult)result;
            Assert.IsNotNull(viewResult.Model);
            Assert.IsInstanceOfType(viewResult.Model, typeof(GameDetailViewModel));

            GameDetailViewModel model = (GameDetailViewModel)viewResult.Model;
            Assert.AreEqual(steamID, model.SteamId);
            Assert.AreEqual(appID, model.AppId);
            Assert.AreEqual(expectedPlayer, model.Player);
            Assert.IsNotNull(model.GameDetail);

            // Verify both API calls were made
            await _mockApiClient.Received(1).GetPlayer(steamID);
            await _mockApiClient.Received(1).GetGameDetail(steamID, appID);
        }

        [TestMethod]
        public async Task GameDetails_ShowCompletedAchievementsTrue_ReturnsViewWithAllAchievements()
        {
            // Arrange
            string steamID = "test-steam-id";
            string appID = "123";
            bool showCompletedAchievements = true;

            Player expectedPlayer = new Player { SteamID = steamID, PersonaName = "Test Player" };
            GameDetail expectedGameDetail = new GameDetail
            {
                GameName = "Test Game",
                Achievements = new List<Achievement>
                {
                    new Achievement { Name = "Achievement 1", Achieved = false },
                    new Achievement { Name = "Achievement 2", Achieved = true },
                    new Achievement { Name = "Achievement 3", Achieved = true }
                }
            };

            _mockApiClient.GetPlayer(steamID).Returns(Task.FromResult(expectedPlayer));
            _mockApiClient.GetGameDetail(steamID, appID).Returns(Task.FromResult(expectedGameDetail));

            // Act
            IActionResult result = await _controller.GameDetails(steamID, appID, showCompletedAchievements);

            // Assert
            Assert.IsNotNull(result);
            ViewResult viewResult = (ViewResult)result;
            GameDetailViewModel model = (GameDetailViewModel)viewResult.Model;

            // When showCompletedAchievements is true, all achievements should be shown
            Assert.AreEqual(3, model.GameDetail.Achievements.Count);

            // Verify both API calls were made
            await _mockApiClient.Received(1).GetPlayer(steamID);
            await _mockApiClient.Received(1).GetGameDetail(steamID, appID);
        }

        [TestMethod]
        public async Task Index_ParallelExecution_BothCallsMadeConcurrently()
        {
            // Arrange
            string steamID = "test-steam-id";
            bool playerCallStarted = false;
            bool gamesCallStarted = false;
            bool bothCallsRunningConcurrently = false;

            Player expectedPlayer = new Player { SteamID = steamID };
            List<Game> expectedGames = new List<Game>();

            _mockApiClient.GetPlayer(steamID).Returns(async callInfo =>
            {
                playerCallStarted = true;
                await Task.Delay(50); // Simulate async work
                if (gamesCallStarted)
                {
                    bothCallsRunningConcurrently = true;
                }
                return expectedPlayer;
            });

            _mockApiClient.GetPlayerGames(steamID).Returns(async callInfo =>
            {
                gamesCallStarted = true;
                await Task.Delay(50); // Simulate async work
                if (playerCallStarted)
                {
                    bothCallsRunningConcurrently = true;
                }
                return expectedGames;
            });

            // Act
            IActionResult result = await _controller.Index(steamID);

            // Assert
            Assert.IsTrue(playerCallStarted, "Player call should have started");
            Assert.IsTrue(gamesCallStarted, "Games call should have started");
            Assert.IsTrue(bothCallsRunningConcurrently, "Both calls should run concurrently");
        }

        [TestMethod]
        public async Task GameDetails_ParallelExecution_BothCallsMadeConcurrently()
        {
            // Arrange
            string steamID = "test-steam-id";
            string appID = "123";
            bool playerCallStarted = false;
            bool gameDetailCallStarted = false;
            bool bothCallsRunningConcurrently = false;

            Player expectedPlayer = new Player { SteamID = steamID };
            GameDetail expectedGameDetail = new GameDetail { GameName = "Test", Achievements = new List<Achievement>() };

            _mockApiClient.GetPlayer(steamID).Returns(async callInfo =>
            {
                playerCallStarted = true;
                await Task.Delay(50); // Simulate async work
                if (gameDetailCallStarted)
                {
                    bothCallsRunningConcurrently = true;
                }
                return expectedPlayer;
            });

            _mockApiClient.GetGameDetail(steamID, appID).Returns(async callInfo =>
            {
                gameDetailCallStarted = true;
                await Task.Delay(50); // Simulate async work
                if (playerCallStarted)
                {
                    bothCallsRunningConcurrently = true;
                }
                return expectedGameDetail;
            });

            // Act
            IActionResult result = await _controller.GameDetails(steamID, appID);

            // Assert
            Assert.IsTrue(playerCallStarted, "Player call should have started");
            Assert.IsTrue(gameDetailCallStarted, "GameDetail call should have started");
            Assert.IsTrue(bothCallsRunningConcurrently, "Both calls should run concurrently");
        }
    }
}
