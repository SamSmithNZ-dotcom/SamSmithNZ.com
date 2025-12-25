using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithNZ.Service.Models.WorldCup;
using System;

namespace SamSmithNZ.Tests.Models.WorldCup
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class PlayerModelTests
    {
        [TestMethod]
        public void Player_Constructor_CreatesInstance()
        {
            // Arrange & Act
            Player player = new Player();

            // Assert
            Assert.IsNotNull(player);
            Assert.AreEqual(0, player.PlayerCode);
        }

        [TestMethod]
        public void Player_AllProperties_CanBeSetAndRead()
        {
            // Arrange & Act
            Player player = new Player
            {
                PlayerCode = 12345,
                PlayerName = "Neymar",
                Number = 10,
                Position = "Forward",
                TournamentCode = 19,
                TeamCode = 10,
                TeamName = "Brazil",
                DateOfBirth = new DateTime(1992, 2, 5),
                IsCaptain = false,
                ClubName = "Barcelona"
            };

            // Assert
            Assert.AreEqual(12345, player.PlayerCode);
            Assert.AreEqual("Neymar", player.PlayerName);
            Assert.AreEqual(10, player.Number);
            Assert.AreEqual("Forward", player.Position);
            Assert.AreEqual(19, player.TournamentCode);
            Assert.AreEqual(10, player.TeamCode);
            Assert.AreEqual("Brazil", player.TeamName);
            Assert.AreEqual(new DateTime(1992, 2, 5), player.DateOfBirth);
            Assert.IsFalse(player.IsCaptain);
            Assert.AreEqual("Barcelona", player.ClubName);
        }

        [TestMethod]
        public void Player_Captain_CanBeSetToTrue()
        {
            // Arrange & Act
            Player player = new Player
            {
                PlayerName = "Thiago Silva",
                IsCaptain = true
            };

            // Assert
            Assert.IsTrue(player.IsCaptain);
        }

        [TestMethod]
        public void Player_DifferentPositions_CanBeSet()
        {
            // Arrange & Act
            Player goalkeeper = new Player { Position = "Goalkeeper" };
            Player defender = new Player { Position = "Defender" };
            Player midfielder = new Player { Position = "Midfielder" };
            Player forward = new Player { Position = "Forward" };

            // Assert
            Assert.AreEqual("Goalkeeper", goalkeeper.Position);
            Assert.AreEqual("Defender", defender.Position);
            Assert.AreEqual("Midfielder", midfielder.Position);
            Assert.AreEqual("Forward", forward.Position);
        }
    }
}
