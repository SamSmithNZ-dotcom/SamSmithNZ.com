using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;

namespace SamSmithNZ.Tests.Models.WorldCup
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class TeamStatisticsTests
    {
        [TestMethod]
        public void TeamStatistics_WithGames_CalculatesStatisticsCorrectly()
        {
            // Arrange
            Team team = new Team { TeamCode = 1, TeamName = "Brazil" };
            
            List<Game> games = new List<Game>
            {
                new Game
                {
                    Team1Code = 1,
                    Team2Code = 2,
                    Team1PreGameEloRating = 2100,
                    Team2PreGameEloRating = 1900,
                    Team1NormalTimeScore = 2,
                    Team2NormalTimeScore = 1,
                    Team1ResultWonGame = true,
                    Team2ResultWonGame = false,
                    RoundCode = "G"
                },
                new Game
                {
                    Team1Code = 3,
                    Team2Code = 1,
                    Team1PreGameEloRating = 1800,
                    Team2PreGameEloRating = 2100,
                    Team1NormalTimeScore = 0,
                    Team2NormalTimeScore = 2,
                    Team1ResultWonGame = false,
                    Team2ResultWonGame = true,
                    RoundCode = "G"
                }
            };

            TeamStatistics stats = new TeamStatistics
            {
                Team = team
            };

            // Act
            stats.Games = games;

            // Assert
            Assert.AreEqual(2, stats.GamesTotal);
            Assert.AreEqual(2, stats.GamesExpectedWon);
            Assert.AreEqual(0, stats.GamesExpectedLoss);
            Assert.AreEqual("WW", stats.TeamRecord);
        }

        [TestMethod]
        public void TeamStatistics_WithUnexpectedResults_CalculatesCorrectly()
        {
            // Arrange
            Team team = new Team { TeamCode = 1, TeamName = "Iceland" };
            
            List<Game> games = new List<Game>
            {
                new Game
                {
                    Team1Code = 1,
                    Team2Code = 2,
                    Team1PreGameEloRating = 1800,
                    Team2PreGameEloRating = 2100,
                    Team1NormalTimeScore = 2,
                    Team2NormalTimeScore = 1,
                    Team1ResultWonGame = true,
                    Team2ResultWonGame = false,
                    RoundCode = "G"
                }
            };

            TeamStatistics stats = new TeamStatistics
            {
                Team = team
            };

            // Act
            stats.Games = games;

            // Assert
            Assert.AreEqual(1, stats.GamesUnexpectedWin);
            Assert.AreEqual(1, stats.UnexpectedResultTotal);
            Assert.AreEqual("W", stats.TeamRecord);
        }

        [TestMethod]
        public void TeamStatistics_WithDraws_CalculatesCorrectly()
        {
            // Arrange
            Team team = new Team { TeamCode = 1, TeamName = "Spain" };
            
            List<Game> games = new List<Game>
            {
                new Game
                {
                    Team1Code = 1,
                    Team2Code = 2,
                    Team1PreGameEloRating = 2100,
                    Team2PreGameEloRating = 1900,
                    Team1NormalTimeScore = 1,
                    Team2NormalTimeScore = 1,
                    Team1ResultWonGame = false,
                    Team2ResultWonGame = false,
                    RoundCode = "G"
                }
            };

            TeamStatistics stats = new TeamStatistics
            {
                Team = team
            };

            // Act
            stats.Games = games;

            // Assert
            Assert.AreEqual(1, stats.GamesUnexpectedDraw);
            Assert.AreEqual("D", stats.TeamRecord);
        }

        [TestMethod]
        public void TeamStatistics_WithLosses_CalculatesCorrectly()
        {
            // Arrange
            Team team = new Team { TeamCode = 1, TeamName = "Germany" };
            
            List<Game> games = new List<Game>
            {
                new Game
                {
                    Team1Code = 1,
                    Team2Code = 2,
                    Team1PreGameEloRating = 2100,
                    Team2PreGameEloRating = 1900,
                    Team1NormalTimeScore = 0,
                    Team2NormalTimeScore = 1,
                    Team1ResultWonGame = false,
                    Team2ResultWonGame = true,
                    RoundCode = "G"
                }
            };

            TeamStatistics stats = new TeamStatistics
            {
                Team = team
            };

            // Act
            stats.Games = games;

            // Assert
            Assert.AreEqual(1, stats.GamesUnexpectedLoss);
            Assert.AreEqual("L", stats.TeamRecord);
        }

        [TestMethod]
        public void TeamStatistics_AsTeam2_CalculatesCorrectly()
        {
            // Arrange
            Team team = new Team { TeamCode = 2, TeamName = "Argentina" };
            
            List<Game> games = new List<Game>
            {
                new Game
                {
                    Team1Code = 1,
                    Team2Code = 2,
                    Team1PreGameEloRating = 1900,
                    Team2PreGameEloRating = 2100,
                    Team1NormalTimeScore = 1,
                    Team2NormalTimeScore = 2,
                    Team1ResultWonGame = false,
                    Team2ResultWonGame = true,
                    RoundCode = "G"
                }
            };

            TeamStatistics stats = new TeamStatistics
            {
                Team = team
            };

            // Act
            stats.Games = games;

            // Assert
            Assert.AreEqual(1, stats.GamesExpectedWon);
            Assert.AreEqual("W", stats.TeamRecord);
        }

        [TestMethod]
        public void TeamStatistics_WithFutureGames_IgnoresThemInRecord()
        {
            // Arrange
            Team team = new Team { TeamCode = 1, TeamName = "France" };
            
            List<Game> games = new List<Game>
            {
                new Game
                {
                    Team1Code = 1,
                    Team2Code = 2,
                    Team1PreGameEloRating = 2100,
                    Team2PreGameEloRating = 1900,
                    Team1NormalTimeScore = null, // Future game
                    Team2NormalTimeScore = null,
                    RoundCode = "G"
                }
            };

            TeamStatistics stats = new TeamStatistics
            {
                Team = team
            };

            // Act
            stats.Games = games;

            // Assert
            Assert.AreEqual("", stats.TeamRecord); // No record for future games
            Assert.AreEqual(1, stats.GamesUnknown); // But counted as unknown
        }

        [TestMethod]
        public void TeamStatistics_ExpectedResultPercent_CalculatesCorrectly()
        {
            // Arrange
            Team team = new Team { TeamCode = 1, TeamName = "Brazil" };
            
            List<Game> games = new List<Game>
            {
                new Game
                {
                    Team1Code = 1,
                    Team2Code = 2,
                    Team1PreGameEloRating = 2100,
                    Team2PreGameEloRating = 1900,
                    Team1NormalTimeScore = 2,
                    Team2NormalTimeScore = 1,
                    Team1ResultWonGame = true,
                    Team2ResultWonGame = false,
                    RoundCode = "G"
                },
                new Game
                {
                    Team1Code = 1,
                    Team2Code = 3,
                    Team1PreGameEloRating = 1800,
                    Team2PreGameEloRating = 2100,
                    Team1NormalTimeScore = 2,
                    Team2NormalTimeScore = 1,
                    Team1ResultWonGame = true,
                    Team2ResultWonGame = false,
                    RoundCode = "G"
                }
            };

            TeamStatistics stats = new TeamStatistics
            {
                Team = team
            };

            // Act
            stats.Games = games;

            // Assert
            Assert.AreEqual(2, stats.GamesTotal);
            Assert.AreEqual(1, stats.ExpectedResultTotal);
            Assert.AreEqual(0.5, stats.ExpectedResultPercent);
        }
    }
}
