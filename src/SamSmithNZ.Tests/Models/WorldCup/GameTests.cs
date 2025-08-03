using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithNZ.Service.Models.WorldCup;
using System;

namespace SamSmithNZ.Tests.Models.WorldCup
{
    [TestClass]
    [TestCategory("L0")]
    public class GameTests
    {
        [TestMethod]
        public void Game_DefaultConstructor_CreatesInstanceAndCalculatesOdds()
        {
            // Act
            Game game = new Game();

            // Assert
            Assert.IsNotNull(game);
            Assert.AreEqual(0, game.GameCode);
            Assert.AreEqual(0, game.TournamentCode);
            Assert.IsNull(game.RoundCode);
            Assert.AreEqual(DateTime.MinValue, game.GameTime);
        }

        [TestMethod]
        public void Game_GameTimeString_WithTimeOfDayZero_ReturnsDateOnly()
        {
            // Arrange
            Game game = new Game();
            game.GameTime = new DateTime(2022, 11, 20);

            // Act
            string result = game.GameTimeString();

            // Assert
            Assert.AreEqual("20-Nov-2022", result);
        }

        [TestMethod]
        public void Game_GameTimeString_WithTimeOfDay_ReturnsDateAndTime()
        {
            // Arrange
            Game game = new Game();
            game.GameTime = new DateTime(2022, 11, 20, 15, 30, 0);

            // Act
            string result = game.GameTimeString();

            // Assert
            Assert.AreEqual("20-Nov-2022 15:30", result);
        }

        [TestMethod]
        public void Game_Team1TotalGoals_WithNormalTimeOnly_ReturnsNormalTimeScore()
        {
            // Arrange
            Game game = new Game();
            game.Team1NormalTimeScore = 2;
            game.Team1ExtraTimeScore = null;

            // Act
            int? result = game.Team1TotalGoals;

            // Assert
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void Game_Team1TotalGoals_WithNormalAndExtraTime_ReturnsTotalScore()
        {
            // Arrange
            Game game = new Game();
            game.Team1NormalTimeScore = 2;
            game.Team1ExtraTimeScore = 1;

            // Act
            int? result = game.Team1TotalGoals;

            // Assert
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void Game_Team1TotalGoals_WithNullNormalTime_ReturnsNull()
        {
            // Arrange
            Game game = new Game();
            game.Team1NormalTimeScore = null;
            game.Team1ExtraTimeScore = 1;

            // Act
            int? result = game.Team1TotalGoals;

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Game_Team2TotalGoals_WithNormalTimeOnly_ReturnsNormalTimeScore()
        {
            // Arrange
            Game game = new Game();
            game.Team2NormalTimeScore = 1;
            game.Team2ExtraTimeScore = null;

            // Act
            int? result = game.Team2TotalGoals;

            // Assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void Game_Team2TotalGoals_WithNormalAndExtraTime_ReturnsTotalScore()
        {
            // Arrange
            Game game = new Game();
            game.Team2NormalTimeScore = 1;
            game.Team2ExtraTimeScore = 2;

            // Act
            int? result = game.Team2TotalGoals;

            // Assert
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void Game_Team1ChanceToWin_WithNullEloRatings_ReturnsMinusOne()
        {
            // Arrange
            Game game = new Game();
            game.Team1PreGameEloRating = null;
            game.Team2PreGameEloRating = null;

            // Act
            double result = game.Team1ChanceToWin;

            // Assert
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void Game_Team1ChanceToWin_WithValidEloRatings_CalculatesOdds()
        {
            // Arrange
            Game game = new Game();
            game.Team1PreGameEloRating = 1800;
            game.Team2PreGameEloRating = 1600;
            game.RoundCode = "GS"; // Group Stage - can end in draw

            // Act
            double result = game.Team1ChanceToWin;

            // Assert
            Assert.IsTrue(result > 0);
            Assert.IsTrue(result < 100);
        }

        [TestMethod]
        public void Game_Team2ChanceToWin_WithValidEloRatings_CalculatesOdds()
        {
            // Arrange
            Game game = new Game();
            game.Team1PreGameEloRating = 1600;
            game.Team2PreGameEloRating = 1800;
            game.RoundCode = "GS"; // Group Stage - can end in draw

            // Act
            double result = game.Team2ChanceToWin;

            // Assert
            Assert.IsTrue(result > 0);
            Assert.IsTrue(result < 100);
        }

        [TestMethod]
        public void Game_TeamChanceToDraw_WithGroupStageGame_CalculatesDrawChance()
        {
            // Arrange
            Game game = new Game();
            game.Team1PreGameEloRating = 1700;
            game.Team2PreGameEloRating = 1700;
            game.RoundCode = "GS"; // Group Stage - can end in draw

            // Act
            double result = game.TeamChanceToDraw;

            // Assert
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void Game_TeamChanceToDraw_WithPlayoffGame_ReturnsZero()
        {
            // Arrange
            Game game = new Game();
            game.Team1PreGameEloRating = 1700;
            game.Team2PreGameEloRating = 1700;
            game.RoundCode = "16"; // Knockout stage - cannot end in draw

            // Act
            double result = game.TeamChanceToDraw;

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Game_GameCanEndInADraw_WithGroupStage_ReturnsTrue()
        {
            // Arrange
            Game game = new Game();
            game.RoundCode = "GS";
            game.Team1PreGameEloRating = 1700;
            game.Team2PreGameEloRating = 1700;

            // Act - Accessing any odds property triggers CalculateOdds()
            double temp = game.Team1ChanceToWin;

            // Assert
            Assert.IsTrue(game.GameCanEndInADraw);
        }

        [TestMethod]
        public void Game_GameCanEndInADraw_WithKnockoutRound_ReturnsFalse()
        {
            // Arrange
            Game game = new Game();
            game.RoundCode = "16"; // Round of 16
            game.Team1PreGameEloRating = 1700;
            game.Team2PreGameEloRating = 1700;

            // Act - Accessing any odds property triggers CalculateOdds()
            double temp = game.Team1ChanceToWin;

            // Assert
            Assert.IsFalse(game.GameCanEndInADraw);
        }

        [TestMethod]
        public void Game_GameCanEndInADraw_WithQuarterFinals_ReturnsFalse()
        {
            // Arrange
            Game game = new Game();
            game.RoundCode = "QF"; // Quarter Finals
            game.Team1PreGameEloRating = 1700;
            game.Team2PreGameEloRating = 1700;

            // Act - Accessing any odds property triggers CalculateOdds()
            double temp = game.Team1ChanceToWin;

            // Assert
            Assert.IsFalse(game.GameCanEndInADraw);
        }

        [TestMethod]
        public void Game_GameCanEndInADraw_WithSemiFinals_ReturnsFalse()
        {
            // Arrange
            Game game = new Game();
            game.RoundCode = "SF"; // Semi Finals
            game.Team1PreGameEloRating = 1700;
            game.Team2PreGameEloRating = 1700;

            // Act - Accessing any odds property triggers CalculateOdds()
            double temp = game.Team1ChanceToWin;

            // Assert
            Assert.IsFalse(game.GameCanEndInADraw);
        }

        [TestMethod]
        public void Game_GameCanEndInADraw_WithFinals_ReturnsFalse()
        {
            // Arrange
            Game game = new Game();
            game.RoundCode = "FF"; // Finals
            game.Team1PreGameEloRating = 1700;
            game.Team2PreGameEloRating = 1700;

            // Act - Accessing any odds property triggers CalculateOdds()
            double temp = game.Team1ChanceToWin;

            // Assert
            Assert.IsFalse(game.GameCanEndInADraw);
        }

        [TestMethod]
        public void Game_GameCanEndInADraw_WithThirdPlace_ReturnsFalse()
        {
            // Arrange
            Game game = new Game();
            game.RoundCode = "3P"; // Third Place
            game.Team1PreGameEloRating = 1700;
            game.Team2PreGameEloRating = 1700;

            // Act - Accessing any odds property triggers CalculateOdds()
            double temp = game.Team1ChanceToWin;

            // Assert
            Assert.IsFalse(game.GameCanEndInADraw);
        }

        [TestMethod]
        public void Game_OddsCalculation_EqualTeams_ReturnsRoughlyEqualChances()
        {
            // Arrange
            Game game = new Game();
            game.Team1PreGameEloRating = 1700;
            game.Team2PreGameEloRating = 1700;
            game.RoundCode = "GS"; // Group Stage

            // Act
            double team1Chance = game.Team1ChanceToWin;
            double team2Chance = game.Team2ChanceToWin;
            double drawChance = game.TeamChanceToDraw;

            // Assert
            Assert.IsTrue(Math.Abs(team1Chance - team2Chance) < 5); // Should be roughly equal
            Assert.IsTrue(team1Chance + team2Chance + drawChance > 99); // Should sum to ~100%
            Assert.IsTrue(team1Chance + team2Chance + drawChance < 101);
        }

        [TestMethod]
        public void Game_OddsCalculation_StrongerTeam1_ReturnsHigherChanceForTeam1()
        {
            // Arrange
            Game game = new Game();
            game.Team1PreGameEloRating = 1900;
            game.Team2PreGameEloRating = 1500;
            game.RoundCode = "GS"; // Group Stage

            // Act
            double team1Chance = game.Team1ChanceToWin;
            double team2Chance = game.Team2ChanceToWin;

            // Assert
            Assert.IsTrue(team1Chance > team2Chance);
            Assert.IsTrue(team1Chance > 50);
        }

        [TestMethod]
        public void Game_SetAllBasicProperties_ReturnsCorrectValues()
        {
            // Arrange & Act
            Game game = new Game
            {
                GameCode = 123,
                TournamentCode = 22,
                RoundCode = "GS",
                RoundName = "Group Stage",
                Location = "Qatar",
                TournamentName = "FIFA World Cup",
                GameNumber = 1,
                GameTime = new DateTime(2022, 11, 20, 16, 0, 0),
                Team1Code = 1,
                Team1Name = "Qatar",
                Team2Code = 2,
                Team2Name = "Ecuador",
                Team1NormalTimeScore = 0,
                Team2NormalTimeScore = 2
            };

            // Assert
            Assert.AreEqual(123, game.GameCode);
            Assert.AreEqual(22, game.TournamentCode);
            Assert.AreEqual("GS", game.RoundCode);
            Assert.AreEqual("Group Stage", game.RoundName);
            Assert.AreEqual("Qatar", game.Location);
            Assert.AreEqual("FIFA World Cup", game.TournamentName);
            Assert.AreEqual(1, game.GameNumber);
            Assert.AreEqual(new DateTime(2022, 11, 20, 16, 0, 0), game.GameTime);
            Assert.AreEqual(1, game.Team1Code);
            Assert.AreEqual("Qatar", game.Team1Name);
            Assert.AreEqual(2, game.Team2Code);
            Assert.AreEqual("Ecuador", game.Team2Name);
            Assert.AreEqual(0, game.Team1NormalTimeScore);
            Assert.AreEqual(2, game.Team2NormalTimeScore);
        }
    }
}