using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithNZ.Service.Models.WorldCup;
using System;

namespace SamSmithNZ.Tests.Models.WorldCup
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class GameModelTests
    {
        [TestMethod]
        public void GameTimeString_WithTimeComponent_ReturnsDateAndTime()
        {
            // Arrange
            Game game = new Game
            {
                GameTime = new DateTime(2014, 6, 12, 17, 0, 0)
            };

            // Act
            string result = game.GameTimeString();

            // Assert
            Assert.AreEqual("12-Jun-2014 17:00", result);
        }

        [TestMethod]
        public void GameTimeString_WithoutTimeComponent_ReturnsDateOnly()
        {
            // Arrange
            Game game = new Game
            {
                GameTime = new DateTime(2014, 6, 12, 0, 0, 0)
            };

            // Act
            string result = game.GameTimeString();

            // Assert
            Assert.AreEqual("12-Jun-2014", result);
        }

        [TestMethod]
        public void Team1TotalGoals_WithNormalTimeOnly_ReturnsNormalTimeScore()
        {
            // Arrange
            Game game = new Game
            {
                Team1NormalTimeScore = 3,
                Team1ExtraTimeScore = null
            };

            // Act
            int? result = game.Team1TotalGoals;

            // Assert
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void Team1TotalGoals_WithNormalAndExtraTime_ReturnsCombinedScore()
        {
            // Arrange
            Game game = new Game
            {
                Team1NormalTimeScore = 2,
                Team1ExtraTimeScore = 1
            };

            // Act
            int? result = game.Team1TotalGoals;

            // Assert
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void Team1TotalGoals_WithNoScores_ReturnsNull()
        {
            // Arrange
            Game game = new Game
            {
                Team1NormalTimeScore = null,
                Team1ExtraTimeScore = null
            };

            // Act
            int? result = game.Team1TotalGoals;

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Team2TotalGoals_WithNormalTimeOnly_ReturnsNormalTimeScore()
        {
            // Arrange
            Game game = new Game
            {
                Team2NormalTimeScore = 2,
                Team2ExtraTimeScore = null
            };

            // Act
            int? result = game.Team2TotalGoals;

            // Assert
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void Team2TotalGoals_WithNormalAndExtraTime_ReturnsCombinedScore()
        {
            // Arrange
            Game game = new Game
            {
                Team2NormalTimeScore = 1,
                Team2ExtraTimeScore = 2
            };

            // Act
            int? result = game.Team2TotalGoals;

            // Assert
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void Team2TotalGoals_WithNoScores_ReturnsNull()
        {
            // Arrange
            Game game = new Game
            {
                Team2NormalTimeScore = null,
                Team2ExtraTimeScore = null
            };

            // Act
            int? result = game.Team2TotalGoals;

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Game_Constructor_InitializesWithCalculateOdds()
        {
            // Arrange & Act
            Game game = new Game();

            // Assert
            Assert.IsNotNull(game);
            Assert.AreEqual(0, game.GameCode);
        }

        [TestMethod]
        public void Game_AllProperties_CanBeSetAndRead()
        {
            // Arrange & Act
            Game game = new Game
            {
                GameCode = 7328,
                TournamentCode = 19,
                Team1Code = 10,
                Team2Code = 29,
                Team1Name = "Brazil",
                Team2Name = "Spain",
                Team1NormalTimeScore = 3,
                Team2NormalTimeScore = 0,
                RoundNumber = 1,
                RoundCode = "G",
                Location = "São Paulo",
                GameTime = new DateTime(2014, 6, 12, 17, 0, 0),
                Team1Withdrew = false,
                Team2Withdrew = false
            };

            // Assert
            Assert.AreEqual(7328, game.GameCode);
            Assert.AreEqual(19, game.TournamentCode);
            Assert.AreEqual(10, game.Team1Code);
            Assert.AreEqual(29, game.Team2Code);
            Assert.AreEqual("Brazil", game.Team1Name);
            Assert.AreEqual("Spain", game.Team2Name);
            Assert.AreEqual(3, game.Team1NormalTimeScore);
            Assert.AreEqual(0, game.Team2NormalTimeScore);
            Assert.IsFalse(game.Team1Withdrew);
            Assert.IsFalse(game.Team2Withdrew);
        }

        [TestMethod]
        public void Game_WithPenalties_PropertiesSetCorrectly()
        {
            // Arrange & Act
            Game game = new Game
            {
                Team1NormalTimeScore = 1,
                Team2NormalTimeScore = 1,
                Team1ExtraTimeScore = 0,
                Team2ExtraTimeScore = 0,
                Team1PenaltiesScore = 5,
                Team2PenaltiesScore = 4
            };

            // Assert
            Assert.AreEqual(1, game.Team1NormalTimeScore);
            Assert.AreEqual(1, game.Team2NormalTimeScore);
            Assert.AreEqual(5, game.Team1PenaltiesScore);
            Assert.AreEqual(4, game.Team2PenaltiesScore);
        }

        [TestMethod]
        public void Game_WithExtraTime_PropertiesSetCorrectly()
        {
            // Arrange & Act
            Game game = new Game
            {
                Team1NormalTimeScore = 2,
                Team2NormalTimeScore = 2,
                Team1ExtraTimeScore = 1,
                Team2ExtraTimeScore = 0
            };

            // Assert
            Assert.AreEqual(2, game.Team1NormalTimeScore);
            Assert.AreEqual(2, game.Team2NormalTimeScore);
            Assert.AreEqual(1, game.Team1ExtraTimeScore);
            Assert.AreEqual(0, game.Team2ExtraTimeScore);
            Assert.AreEqual(3, game.Team1TotalGoals);
            Assert.AreEqual(2, game.Team2TotalGoals);
        }

        [TestMethod]
        public void Game_WithEloRatings_PropertiesSetCorrectly()
        {
            // Arrange & Act
            Game game = new Game
            {
                Team1PreGameEloRating = 2000,
                Team2PreGameEloRating = 1950,
                Team1PostGameEloRating = 2010,
                Team2PostGameEloRating = 1940
            };

            // Assert
            Assert.AreEqual(2000, game.Team1PreGameEloRating);
            Assert.AreEqual(1950, game.Team2PreGameEloRating);
            Assert.AreEqual(2010, game.Team1PostGameEloRating);
            Assert.AreEqual(1940, game.Team2PostGameEloRating);
        }

        [TestMethod]
        public void Game_WithWithdrawal_PropertiesSetCorrectly()
        {
            // Arrange & Act
            Game game = new Game
            {
                Team1Withdrew = true,
                Team2Withdrew = false
            };

            // Assert
            Assert.IsTrue(game.Team1Withdrew);
            Assert.IsFalse(game.Team2Withdrew);
        }

        [TestMethod]
        public void Game_GoalProperties_CanBeSet()
        {
            // Arrange & Act
            Game game = new Game
            {
                IsPenalty = true,
                IsOwnGoal = false,
                IsGoldenGoal = false
            };

            // Assert
            Assert.IsTrue(game.IsPenalty);
            Assert.IsFalse(game.IsOwnGoal);
            Assert.IsFalse(game.IsGoldenGoal);
        }
    }
}
