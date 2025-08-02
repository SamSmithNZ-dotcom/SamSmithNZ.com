using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithNZ.Service.Models.Steam;

namespace SamSmithNZ.Tests.Models.Steam
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class GameTests
    {
        [TestMethod]
        public void Game_SetAndGetAppID_ReturnsCorrectValue()
        {
            // Arrange
            var game = new Game();
            var expectedAppID = "200510";

            // Act
            game.AppID = expectedAppID;

            // Assert
            Assert.AreEqual(expectedAppID, game.AppID);
        }

        [TestMethod]
        public void Game_SetAndGetGameName_ReturnsCorrectValue()
        {
            // Arrange
            var game = new Game();
            var expectedGameName = "XCOM: Enemy Unknown";

            // Act
            game.GameName = expectedGameName;

            // Assert
            Assert.AreEqual(expectedGameName, game.GameName);
        }

        [TestMethod]
        public void Game_SetAndGetTotalMinutesPlayed_ReturnsCorrectValue()
        {
            // Arrange
            var game = new Game();
            long expectedMinutes = 1234;

            // Act
            game.TotalMinutesPlayed = expectedMinutes;

            // Assert
            Assert.AreEqual(expectedMinutes, game.TotalMinutesPlayed);
        }

        [TestMethod]
        public void Game_SetAndGetTotalTimeString_ReturnsCorrectValue()
        {
            // Arrange
            var game = new Game();
            var expectedTimeString = "20.6 hrs";

            // Act
            game.TotalTimeString = expectedTimeString;

            // Assert
            Assert.AreEqual(expectedTimeString, game.TotalTimeString);
        }

        [TestMethod]
        public void Game_SetAndGetIconURL_ReturnsCorrectValue()
        {
            // Arrange
            var game = new Game();
            var expectedIconURL = "cd8f7a795e34e16449f7ad8d8190dce521967917";

            // Act
            game.IconURL = expectedIconURL;

            // Assert
            Assert.AreEqual(expectedIconURL, game.IconURL);
        }

        [TestMethod]
        public void Game_SetAndGetLogoURL_ReturnsCorrectValue()
        {
            // Arrange
            var game = new Game();
            var expectedLogoURL = "5450218e6f8ea246272cddcb2ab9a453b0ca7ef5";

            // Act
            game.LogoURL = expectedLogoURL;

            // Assert
            Assert.AreEqual(expectedLogoURL, game.LogoURL);
        }

        [TestMethod]
        public void Game_SetAndGetCommunityIsVisible_ReturnsCorrectValue()
        {
            // Arrange
            var game = new Game();

            // Act & Assert - Test true
            game.CommunityIsVisible = true;
            Assert.IsTrue(game.CommunityIsVisible);

            // Act & Assert - Test false
            game.CommunityIsVisible = false;
            Assert.IsFalse(game.CommunityIsVisible);
        }

        [TestMethod]
        public void Game_DefaultConstructor_CreatesInstance()
        {
            // Act
            var game = new Game();

            // Assert
            Assert.IsNotNull(game);
            Assert.IsNull(game.AppID);
            Assert.IsNull(game.GameName);
            Assert.AreEqual(0, game.TotalMinutesPlayed);
            Assert.IsNull(game.TotalTimeString);
            Assert.IsNull(game.IconURL);
            Assert.IsNull(game.LogoURL);
            Assert.IsFalse(game.CommunityIsVisible);
        }

        [TestMethod]
        public void Game_InitializeWithAllProperties_ReturnsAllValues()
        {
            // Arrange
            var expectedAppID = "200510";
            var expectedGameName = "XCOM: Enemy Unknown";
            long expectedMinutes = 1234;
            var expectedTimeString = "20.6 hrs";
            var expectedIconURL = "cd8f7a795e34e16449f7ad8d8190dce521967917";
            var expectedLogoURL = "5450218e6f8ea246272cddcb2ab9a453b0ca7ef5";
            var expectedCommunityIsVisible = true;

            // Act
            var game = new Game
            {
                AppID = expectedAppID,
                GameName = expectedGameName,
                TotalMinutesPlayed = expectedMinutes,
                TotalTimeString = expectedTimeString,
                IconURL = expectedIconURL,
                LogoURL = expectedLogoURL,
                CommunityIsVisible = expectedCommunityIsVisible
            };

            // Assert
            Assert.AreEqual(expectedAppID, game.AppID);
            Assert.AreEqual(expectedGameName, game.GameName);
            Assert.AreEqual(expectedMinutes, game.TotalMinutesPlayed);
            Assert.AreEqual(expectedTimeString, game.TotalTimeString);
            Assert.AreEqual(expectedIconURL, game.IconURL);
            Assert.AreEqual(expectedLogoURL, game.LogoURL);
            Assert.AreEqual(expectedCommunityIsVisible, game.CommunityIsVisible);
        }
    }
}