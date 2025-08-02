using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithNZ.Service.Models.Steam;

namespace SamSmithNZ.Tests.Models.Steam
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class PlayerTests
    {
        [TestMethod]
        public void Player_SetAndGetSteamID_ReturnsCorrectValue()
        {
            // Arrange
            var player = new Player();
            var expectedSteamID = "76561198059077520";

            // Act
            player.SteamID = expectedSteamID;

            // Assert
            Assert.AreEqual(expectedSteamID, player.SteamID);
        }

        [TestMethod]
        public void Player_SetAndGetPlayerName_ReturnsCorrectValue()
        {
            // Arrange
            var player = new Player();
            var expectedPlayerName = "TestPlayer";

            // Act
            player.PlayerName = expectedPlayerName;

            // Assert
            Assert.AreEqual(expectedPlayerName, player.PlayerName);
        }

        [TestMethod]
        public void Player_SetAndGetIsPublic_ReturnsCorrectValue()
        {
            // Arrange
            var player = new Player();

            // Act & Assert - Test true
            player.IsPublic = true;
            Assert.IsTrue(player.IsPublic);

            // Act & Assert - Test false
            player.IsPublic = false;
            Assert.IsFalse(player.IsPublic);
        }

        [TestMethod]
        public void Player_DefaultConstructor_CreatesInstance()
        {
            // Act
            var player = new Player();

            // Assert
            Assert.IsNotNull(player);
            Assert.IsNull(player.SteamID);
            Assert.IsNull(player.PlayerName);
            Assert.IsFalse(player.IsPublic); // default bool value
        }

        [TestMethod]
        public void Player_InitializeWithAllProperties_ReturnsAllValues()
        {
            // Arrange
            var expectedSteamID = "76561198059077520";
            var expectedPlayerName = "TestPlayer";
            var expectedIsPublic = true;

            // Act
            var player = new Player
            {
                SteamID = expectedSteamID,
                PlayerName = expectedPlayerName,
                IsPublic = expectedIsPublic
            };

            // Assert
            Assert.AreEqual(expectedSteamID, player.SteamID);
            Assert.AreEqual(expectedPlayerName, player.PlayerName);
            Assert.AreEqual(expectedIsPublic, player.IsPublic);
        }
    }
}