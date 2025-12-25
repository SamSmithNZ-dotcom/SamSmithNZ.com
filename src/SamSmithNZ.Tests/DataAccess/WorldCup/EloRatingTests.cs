using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithNZ.Service.DataAccess.WorldCup;
using SamSmithNZ.Service.Models.WorldCup;
using static SamSmithNZ.Service.DataAccess.WorldCup.EloRating;

namespace SamSmithNZ.Tests.DataAccess.WorldCup
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class EloRatingTests
    {
        [TestMethod]
        public void EloRating_GetEloRatingScoresForMatchUp_Team1Wins_UpdatesCorrectly()
        {
            // Arrange
            EloRating eloRating = new EloRating();
            int team1Score = 2000;
            int team2Score = 1900;
            bool team1Won = true;
            bool team2Won = false;

            // Act
            (int team1Result, int team2Result) = eloRating.GetEloRatingScoresForMatchUp(team1Score, team2Score, team1Won, team2Won);

            // Assert
            Assert.IsTrue(team1Result > team1Score); // Winner gains points
            Assert.IsTrue(team2Result < team2Score); // Loser loses points
        }

        [TestMethod]
        public void EloRating_GetEloRatingScoresForMatchUp_Team2Wins_UpdatesCorrectly()
        {
            // Arrange
            EloRating eloRating = new EloRating();
            int team1Score = 2000;
            int team2Score = 1900;
            bool team1Won = false;
            bool team2Won = true;

            // Act
            (int team1Result, int team2Result) = eloRating.GetEloRatingScoresForMatchUp(team1Score, team2Score, team1Won, team2Won);

            // Assert
            Assert.IsTrue(team1Result < team1Score); // Loser loses points
            Assert.IsTrue(team2Result > team2Score); // Underdog winner gains more points
        }

        [TestMethod]
        public void EloRating_GetEloRatingScoresForMatchUp_Draw_UpdatesCorrectly()
        {
            // Arrange
            EloRating eloRating = new EloRating();
            int team1Score = 2000;
            int team2Score = 1900;
            bool team1Won = false;
            bool team2Won = false;

            // Act
            (int team1Result, int team2Result) = eloRating.GetEloRatingScoresForMatchUp(team1Score, team2Score, team1Won, team2Won);

            // Assert - In a draw, favorite loses points, underdog gains points
            Assert.IsTrue(team1Result < team1Score); // Higher rated team loses points in draw
            Assert.IsTrue(team2Result > team2Score); // Lower rated team gains points in draw
        }

        [TestMethod]
        public void EloRating_CalculateKFactor_TwoGoalDifference_Multiplies1Point5()
        {
            // Arrange
            EloRating eloRating = new EloRating();
            Game game = new Game
            {
                Team1NormalTimeScore = 2,
                Team2NormalTimeScore = 0
            };

            // Act
            double kFactor = eloRating.CalculateKFactor(game);

            // Assert - 2 goal difference: 100 * 1.5 = 150
            Assert.AreEqual(150d, kFactor);
        }

        [TestMethod]
        public void EloRating_CalculateKFactor_ThreeGoalDifference_Multiplies2()
        {
            // Arrange
            EloRating eloRating = new EloRating();
            Game game = new Game
            {
                Team1NormalTimeScore = 4,
                Team2NormalTimeScore = 1
            };

            // Act
            double kFactor = eloRating.CalculateKFactor(game);

            // Assert - 3 goal difference: 100 * 2 = 200
            Assert.AreEqual(200d, kFactor);
        }

        [TestMethod]
        public void EloRating_CalculateKFactor_FourPlusGoalDifference_Multiplies3Point5()
        {
            // Arrange
            EloRating eloRating = new EloRating();
            Game game = new Game
            {
                Team1NormalTimeScore = 7,
                Team2NormalTimeScore = 1
            };

            // Act
            double kFactor = eloRating.CalculateKFactor(game);

            // Assert - 6 goal difference (>=4): 100 * 3.5 = 350
            Assert.AreEqual(350d, kFactor);
        }

        [TestMethod]
        public void EloRating_CalculateKFactor_OneGoalDifference_NoMultiplier()
        {
            // Arrange
            EloRating eloRating = new EloRating();
            Game game = new Game
            {
                Team1NormalTimeScore = 1,
                Team2NormalTimeScore = 0
            };

            // Act
            double kFactor = eloRating.CalculateKFactor(game);

            // Assert - 1 goal difference: 100 (no multiplier)
            Assert.AreEqual(100d, kFactor);
        }

        [TestMethod]
        public void EloRating_CalculateKFactor_NegativeGoalDifference_ConvertsToPositive()
        {
            // Arrange
            EloRating eloRating = new EloRating();
            Game game = new Game
            {
                Team1NormalTimeScore = 0,
                Team2NormalTimeScore = 2
            };

            // Act
            double kFactor = eloRating.CalculateKFactor(game);

            // Assert - -2 goal difference converted to 2: 100 * 1.5 = 150
            Assert.AreEqual(150d, kFactor);
        }

        [TestMethod]
        public void EloRating_WhoWon_Team1Wins_ReturnsTeam1()
        {
            // Arrange
            EloRating eloRating = new EloRating();
            Game game = new Game
            {
                Team1NormalTimeScore = 2,
                Team2NormalTimeScore = 1
            };

            // Act
            WhoWonEnum? result = eloRating.WhoWon(game);

            // Assert
            Assert.AreEqual(WhoWonEnum.Team1, result);
        }

        [TestMethod]
        public void EloRating_WhoWon_Team2Wins_ReturnsTeam2()
        {
            // Arrange
            EloRating eloRating = new EloRating();
            Game game = new Game
            {
                Team1NormalTimeScore = 1,
                Team2NormalTimeScore = 3
            };

            // Act
            WhoWonEnum? result = eloRating.WhoWon(game);

            // Assert
            Assert.AreEqual(WhoWonEnum.Team2, result);
        }

        [TestMethod]
        public void EloRating_WhoWon_Draw_ReturnsDraw()
        {
            // Arrange
            EloRating eloRating = new EloRating();
            Game game = new Game
            {
                Team1NormalTimeScore = 2,
                Team2NormalTimeScore = 2
            };

            // Act
            WhoWonEnum? result = eloRating.WhoWon(game);

            // Assert
            Assert.AreEqual(WhoWonEnum.Draw, result);
        }

        [TestMethod]
        public void EloRating_WhoWon_Team1Withdrew_ReturnsTeam2()
        {
            // Arrange
            EloRating eloRating = new EloRating();
            Game game = new Game
            {
                Team1NormalTimeScore = null,
                Team2NormalTimeScore = null,
                Team1Withdrew = true,
                Team2Withdrew = false
            };

            // Act
            WhoWonEnum? result = eloRating.WhoWon(game);

            // Assert
            Assert.AreEqual(WhoWonEnum.Team2, result);
        }

        [TestMethod]
        public void EloRating_WhoWon_Team2Withdrew_ReturnsTeam1()
        {
            // Arrange
            EloRating eloRating = new EloRating();
            Game game = new Game
            {
                Team1NormalTimeScore = null,
                Team2NormalTimeScore = null,
                Team1Withdrew = false,
                Team2Withdrew = true
            };

            // Act
            WhoWonEnum? result = eloRating.WhoWon(game);

            // Assert
            Assert.AreEqual(WhoWonEnum.Team1, result);
        }

        [TestMethod]
        public void EloRating_WhoWon_NoScoreYet_ReturnsNull()
        {
            // Arrange
            EloRating eloRating = new EloRating();
            Game game = new Game
            {
                Team1NormalTimeScore = null,
                Team2NormalTimeScore = null,
                Team1Withdrew = false,
                Team2Withdrew = false
            };

            // Act
            WhoWonEnum? result = eloRating.WhoWon(game);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void EloRating_CalculateGoalDifference_NoScores_ReturnsNull()
        {
            // Arrange
            EloRating eloRating = new EloRating();
            Game game = new Game
            {
                Team1NormalTimeScore = null,
                Team2NormalTimeScore = null
            };

            // Act
            int? result = eloRating.CalculateGoalDifference(game);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void EloRating_CalculateGoalDifference_Team1PenaltyWin_Returns1()
        {
            // Arrange
            EloRating eloRating = new EloRating();
            Game game = new Game
            {
                Team1NormalTimeScore = 1,
                Team2NormalTimeScore = 1,
                Team1ExtraTimeScore = 0,
                Team2ExtraTimeScore = 0,
                Team1PenaltiesScore = 5,
                Team2PenaltiesScore = 3
            };

            // Act
            int? result = eloRating.CalculateGoalDifference(game);

            // Assert - Penalties always count as 1 goal difference
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void EloRating_CalculateGoalDifference_Team2PenaltyWin_ReturnsNegative1()
        {
            // Arrange
            EloRating eloRating = new EloRating();
            Game game = new Game
            {
                Team1NormalTimeScore = 0,
                Team2NormalTimeScore = 0,
                Team1ExtraTimeScore = 0,
                Team2ExtraTimeScore = 0,
                Team1PenaltiesScore = 2,
                Team2PenaltiesScore = 4
            };

            // Act
            int? result = eloRating.CalculateGoalDifference(game);

            // Assert - Penalties always count as 1 goal difference (negative for team 2 win)
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void EloRating_CalculateGoalDifference_ExtraTime_CalculatesCorrectly()
        {
            // Arrange
            EloRating eloRating = new EloRating();
            Game game = new Game
            {
                Team1NormalTimeScore = 1,
                Team2NormalTimeScore = 1,
                Team1ExtraTimeScore = 1,
                Team2ExtraTimeScore = 0,
                Team1PenaltiesScore = -1,
                Team2PenaltiesScore = -1
            };

            // Act
            int? result = eloRating.CalculateGoalDifference(game);

            // Assert - Normal time (1-1) + Extra time (1-0) = 2-1 = 1 goal difference
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void EloRating_CalculateGoalDifference_NormalTimeOnly_CalculatesCorrectly()
        {
            // Arrange
            EloRating eloRating = new EloRating();
            Game game = new Game
            {
                Team1NormalTimeScore = 3,
                Team2NormalTimeScore = 1,
                Team1ExtraTimeScore = -1,
                Team2ExtraTimeScore = -1,
                Team1PenaltiesScore = -1,
                Team2PenaltiesScore = -1
            };

            // Act
            int? result = eloRating.CalculateGoalDifference(game);

            // Assert - 3-1 = 2 goal difference
            Assert.AreEqual(2, result);
        }
    }
}
