using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithNZ.Service.Models.WorldCup;

namespace SamSmithNZ.Tests.Models.WorldCup
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class GameOddsResultsTests
    {
        [TestMethod]
        public void Game_Team1OddsResultUnknown_WhenNoScore_ReturnsTrue()
        {
            // Arrange & Act
            Game game = new Game
            {
                Team1PreGameEloRating = 2000,
                Team2PreGameEloRating = 1900,
                Team1NormalTimeScore = null,
                RoundCode = "G"
            };

            // Assert
            Assert.IsTrue(game.Team1OddsResultUnknown);
        }

        [TestMethod]
        public void Game_Team1OddsResultUnknown_WhenNoEloRating_ReturnsTrue()
        {
            // Arrange & Act
            Game game = new Game
            {
                Team1PreGameEloRating = null,
                Team2PreGameEloRating = null,
                Team1NormalTimeScore = 2,
                RoundCode = "G"
            };

            // Assert
            Assert.IsTrue(game.Team1OddsResultUnknown);
        }

        [TestMethod]
        public void Game_Team1OddsResultUnknown_WhenHasScoreAndElo_ReturnsFalse()
        {
            // Arrange & Act
            Game game = new Game
            {
                Team1PreGameEloRating = 2000,
                Team2PreGameEloRating = 1900,
                Team1NormalTimeScore = 2,
                Team2NormalTimeScore = 1,
                Team1ResultWonGame = true,
                RoundCode = "G"
            };

            // Assert
            Assert.IsFalse(game.Team1OddsResultUnknown);
        }

        [TestMethod]
        public void Game_Team1OddsResultExpectedWin_WhenFavoriteWins_ReturnsTrue()
        {
            // Arrange & Act
            Game game = new Game
            {
                Team1PreGameEloRating = 2100,
                Team2PreGameEloRating = 1900,
                Team1NormalTimeScore = 2,
                Team2NormalTimeScore = 1,
                Team1ResultWonGame = true,
                Team2ResultWonGame = false,
                RoundCode = "G"
            };

            // Assert
            Assert.IsTrue(game.Team1OddsResultExpectedWin);
            Assert.AreEqual(1, game.Team1OddsCountExpectedWin);
        }

        [TestMethod]
        public void Game_Team1OddsResultExpectedLoss_WhenUnderdogLoses_ReturnsTrue()
        {
            // Arrange & Act
            Game game = new Game
            {
                Team1PreGameEloRating = 1800,
                Team2PreGameEloRating = 2100,
                Team1NormalTimeScore = 0,
                Team2NormalTimeScore = 2,
                Team1ResultWonGame = false,
                Team2ResultWonGame = true,
                RoundCode = "G"
            };

            // Assert
            Assert.IsTrue(game.Team1OddsResultExpectedLoss);
            Assert.AreEqual(1, game.Team1OddsCountExpectedLoss);
        }

        [TestMethod]
        public void Game_Team1OddsResultUnexpectedWin_WhenUnderdogWins_ReturnsTrue()
        {
            // Arrange & Act
            Game game = new Game
            {
                Team1PreGameEloRating = 1800,
                Team2PreGameEloRating = 2100,
                Team1NormalTimeScore = 2,
                Team2NormalTimeScore = 1,
                Team1ResultWonGame = true,
                Team2ResultWonGame = false,
                RoundCode = "G"
            };

            // Assert
            Assert.IsTrue(game.Team1OddsResultUnexpectedWin);
            Assert.AreEqual(1, game.Team1OddsCountUnexpectedWin);
        }

        [TestMethod]
        public void Game_Team1OddsResultUnexpectedLoss_WhenFavoriteLoses_ReturnsTrue()
        {
            // Arrange & Act
            Game game = new Game
            {
                Team1PreGameEloRating = 2100,
                Team2PreGameEloRating = 1900,
                Team1NormalTimeScore = 1,
                Team2NormalTimeScore = 2,
                Team1ResultWonGame = false,
                Team2ResultWonGame = true,
                RoundCode = "G"
            };

            // Assert
            Assert.IsTrue(game.Team1OddsResultUnexpectedLoss);
            Assert.AreEqual(1, game.Team1OddsCountUnexpectedLoss);
        }

        [TestMethod]
        public void Game_Team1OddsResultUnexpectedDraw_WhenGameDraws_ReturnsTrue()
        {
            // Arrange & Act
            Game game = new Game
            {
                Team1PreGameEloRating = 2000,
                Team2PreGameEloRating = 1950,
                Team1NormalTimeScore = 1,
                Team2NormalTimeScore = 1,
                Team1ResultWonGame = false,
                Team2ResultWonGame = false,
                RoundCode = "G"
            };

            // Assert
            Assert.IsTrue(game.Team1OddsResultUnexpectedDraw);
            Assert.AreEqual(1, game.Team1OddsCountUnexpectedDraw);
        }

        [TestMethod]
        public void Game_Team1OddsStatusText_ExpectedWin_ReturnsCorrectText()
        {
            // Arrange & Act
            Game game = new Game
            {
                Team1Name = "Brazil",
                Team1PreGameEloRating = 2100,
                Team2PreGameEloRating = 1900,
                Team1NormalTimeScore = 2,
                Team2NormalTimeScore = 1,
                Team1ResultWonGame = true,
                Team2ResultWonGame = false,
                RoundCode = "G"
            };

            // Assert
            Assert.AreEqual("Brazil win: expected", game.Team1OddsStatusText);
        }

        [TestMethod]
        public void Game_Team1OddsStatusText_UnexpectedLoss_ReturnsCorrectText()
        {
            // Arrange & Act
            Game game = new Game
            {
                Team1Name = "Germany",
                Team1PreGameEloRating = 2100,
                Team2PreGameEloRating = 1900,
                Team1NormalTimeScore = 0,
                Team2NormalTimeScore = 1,
                Team1ResultWonGame = false,
                Team2ResultWonGame = true,
                RoundCode = "G"
            };

            // Assert
            Assert.AreEqual("Germany loss: upset (win expected)", game.Team1OddsStatusText);
        }

        [TestMethod]
        public void Game_Team1OddsStatusText_UnexpectedWin_ReturnsCorrectText()
        {
            // Arrange & Act
            Game game = new Game
            {
                Team1Name = "Iceland",
                Team1PreGameEloRating = 1800,
                Team2PreGameEloRating = 2100,
                Team1NormalTimeScore = 2,
                Team2NormalTimeScore = 1,
                Team1ResultWonGame = true,
                Team2ResultWonGame = false,
                RoundCode = "G"
            };

            // Assert
            Assert.AreEqual("Iceland win: upset (loss expected)", game.Team1OddsStatusText);
        }

        [TestMethod]
        public void Game_Team1OddsStatusText_ExpectedLoss_ReturnsCorrectText()
        {
            // Arrange & Act
            Game game = new Game
            {
                Team1Name = "Panama",
                Team1PreGameEloRating = 1700,
                Team2PreGameEloRating = 2100,
                Team1NormalTimeScore = 0,
                Team2NormalTimeScore = 3,
                Team1ResultWonGame = false,
                Team2ResultWonGame = true,
                RoundCode = "G"
            };

            // Assert
            Assert.AreEqual("Panama loss: expected", game.Team1OddsStatusText);
        }

        [TestMethod]
        public void Game_Team1OddsStatusText_UnexpectedDraw_FavoriteExpectedWin_ReturnsCorrectText()
        {
            // Arrange & Act
            Game game = new Game
            {
                Team1Name = "Spain",
                Team1PreGameEloRating = 2100,
                Team2PreGameEloRating = 1900,
                Team1NormalTimeScore = 1,
                Team2NormalTimeScore = 1,
                Team1ResultWonGame = false,
                Team2ResultWonGame = false,
                RoundCode = "G"
            };

            // Assert
            Assert.AreEqual("Spain draw: upset (win expected)", game.Team1OddsStatusText);
        }

        [TestMethod]
        public void Game_Team1OddsStatusText_UnexpectedDraw_UnderdogExpectedLoss_ReturnsCorrectText()
        {
            // Arrange & Act
            Game game = new Game
            {
                Team1Name = "Tunisia",
                Team1PreGameEloRating = 1700,
                Team2PreGameEloRating = 2100,
                Team1NormalTimeScore = 2,
                Team2NormalTimeScore = 2,
                Team1ResultWonGame = false,
                Team2ResultWonGame = false,
                RoundCode = "G"
            };

            // Assert
            Assert.AreEqual("Tunisia draw: upset (loss expected)", game.Team1OddsStatusText);
        }

        [TestMethod]
        public void Game_Team1OddsStatusText_NoEloRating_ReturnsEmptyString()
        {
            // Arrange & Act
            Game game = new Game
            {
                Team1Name = "NewTeam",
                Team1PreGameEloRating = null,
                Team2PreGameEloRating = null,
                Team1NormalTimeScore = 1,
                Team2NormalTimeScore = 1
            };

            // Assert
            Assert.AreEqual("", game.Team1OddsStatusText);
        }

        [TestMethod]
        public void Game_Team2OddsResultUnknown_WhenNoScore_ReturnsTrue()
        {
            // Arrange & Act
            Game game = new Game
            {
                Team1PreGameEloRating = 2000,
                Team2PreGameEloRating = 1900,
                Team2NormalTimeScore = null,
                RoundCode = "G"
            };

            // Assert
            Assert.IsTrue(game.Team2OddsResultUnknown);
        }

        [TestMethod]
        public void Game_Team2OddsResultExpectedWin_WhenFavoriteWins_ReturnsTrue()
        {
            // Arrange & Act
            Game game = new Game
            {
                Team1PreGameEloRating = 1900,
                Team2PreGameEloRating = 2100,
                Team1NormalTimeScore = 1,
                Team2NormalTimeScore = 2,
                Team1ResultWonGame = false,
                Team2ResultWonGame = true,
                RoundCode = "G"
            };

            // Assert
            Assert.IsTrue(game.Team2OddsResultExpectedWin);
            Assert.AreEqual(1, game.Team2OddsCountExpectedWin);
        }

        [TestMethod]
        public void Game_Team2OddsResultExpectedLoss_WhenUnderdogLoses_ReturnsTrue()
        {
            // Arrange & Act
            Game game = new Game
            {
                Team1PreGameEloRating = 2100,
                Team2PreGameEloRating = 1800,
                Team1NormalTimeScore = 2,
                Team2NormalTimeScore = 0,
                Team1ResultWonGame = true,
                Team2ResultWonGame = false,
                RoundCode = "G"
            };

            // Assert
            Assert.IsTrue(game.Team2OddsResultExpectedLoss);
            Assert.AreEqual(1, game.Team2OddsCountExpectedLoss);
        }

        [TestMethod]
        public void Game_Team2OddsResultUnexpectedWin_WhenUnderdogWins_ReturnsTrue()
        {
            // Arrange & Act
            Game game = new Game
            {
                Team1PreGameEloRating = 2100,
                Team2PreGameEloRating = 1800,
                Team1NormalTimeScore = 1,
                Team2NormalTimeScore = 2,
                Team1ResultWonGame = false,
                Team2ResultWonGame = true,
                RoundCode = "G"
            };

            // Assert
            Assert.IsTrue(game.Team2OddsResultUnexpectedWin);
            Assert.AreEqual(1, game.Team2OddsCountUnexpectedWin);
        }

        [TestMethod]
        public void Game_Team2OddsResultUnexpectedLoss_WhenFavoriteLoses_ReturnsTrue()
        {
            // Arrange & Act
            Game game = new Game
            {
                Team1PreGameEloRating = 1900,
                Team2PreGameEloRating = 2100,
                Team1NormalTimeScore = 2,
                Team2NormalTimeScore = 1,
                Team1ResultWonGame = true,
                Team2ResultWonGame = false,
                RoundCode = "G"
            };

            // Assert
            Assert.IsTrue(game.Team2OddsResultUnexpectedLoss);
            Assert.AreEqual(1, game.Team2OddsCountUnexpectedLoss);
        }

        [TestMethod]
        public void Game_Team2OddsResultUnexpectedDraw_WhenGameDraws_ReturnsTrue()
        {
            // Arrange & Act
            Game game = new Game
            {
                Team1PreGameEloRating = 1950,
                Team2PreGameEloRating = 2000,
                Team1NormalTimeScore = 2,
                Team2NormalTimeScore = 2,
                Team1ResultWonGame = false,
                Team2ResultWonGame = false,
                RoundCode = "G"
            };

            // Assert
            Assert.IsTrue(game.Team2OddsResultUnexpectedDraw);
            Assert.AreEqual(1, game.Team2OddsCountUnexpectedDraw);
        }

        [TestMethod]
        public void Game_AllOddsCounts_WhenResultsFalse_Return0()
        {
            // Arrange & Act - Team with no results yet
            Game game = new Game
            {
                Team1PreGameEloRating = 2000,
                Team2PreGameEloRating = 1900,
                RoundCode = "G"
            };

            // Assert - All counts should be 0 when no results
            Assert.AreEqual(0, game.Team1OddsCountExpectedWin);
            Assert.AreEqual(0, game.Team1OddsCountUnexpectedWin);
            Assert.AreEqual(0, game.Team1OddsCountExpectedLoss);
            Assert.AreEqual(0, game.Team1OddsCountUnexpectedLoss);
            Assert.AreEqual(0, game.Team1OddsCountUnexpectedDraw);
            Assert.AreEqual(0, game.Team2OddsCountExpectedWin);
            Assert.AreEqual(0, game.Team2OddsCountExpectedLoss);
            Assert.AreEqual(0, game.Team2OddsCountUnexpectedWin);
            Assert.AreEqual(0, game.Team2OddsCountUnexpectedLoss);
            Assert.AreEqual(0, game.Team2OddsCountUnexpectedDraw);
        }

        [TestMethod]
        public void Game_Team1OddsCountUnknown_WhenUnknownTrue_Returns1()
        {
            // Arrange & Act
            Game game = new Game
            {
                Team1PreGameEloRating = null,
                Team2PreGameEloRating = null,
                Team1NormalTimeScore = null
            };

            // Assert
            Assert.AreEqual(1, game.Team1OddsCountUnknown);
        }

        [TestMethod]
        public void Game_Team2OddsCountUnknown_WhenUnknownTrue_Returns1()
        {
            // Arrange & Act
            Game game = new Game
            {
                Team1PreGameEloRating = null,
                Team2PreGameEloRating = null,
                Team2NormalTimeScore = null
            };

                        // Assert
                        Assert.AreEqual(1, game.Team2OddsCountUnknown);
                    }

                    [TestMethod]
                    public void Game_Team2OddsStatusText_ExpectedWin_ReturnsCorrectText()
                    {
                        // Arrange & Act
                        Game game = new Game
                        {
                            Team2Name = "France",
                            Team1PreGameEloRating = 1900,
                            Team2PreGameEloRating = 2100,
                            Team1NormalTimeScore = 1,
                            Team2NormalTimeScore = 2,
                            Team1ResultWonGame = false,
                            Team2ResultWonGame = true,
                            RoundCode = "G"
                        };

                        // Assert
                        Assert.AreEqual("France win: expected", game.Team2OddsStatusText);
                    }

                    [TestMethod]
                    public void Game_Team2OddsStatusText_UnexpectedLoss_ReturnsCorrectText()
                    {
                        // Arrange & Act
                        Game game = new Game
                        {
                            Team2Name = "Argentina",
                            Team1PreGameEloRating = 1900,
                            Team2PreGameEloRating = 2100,
                            Team1NormalTimeScore = 2,
                            Team2NormalTimeScore = 1,
                            Team1ResultWonGame = true,
                            Team2ResultWonGame = false,
                            RoundCode = "G"
                        };

                        // Assert
                        Assert.AreEqual("Argentina loss: upset (win expected)", game.Team2OddsStatusText);
                    }

                    [TestMethod]
                    public void Game_Team2OddsStatusText_UnexpectedWin_ReturnsCorrectText()
                    {
                        // Arrange & Act
                        Game game = new Game
                        {
                            Team2Name = "SouthKorea",
                            Team1PreGameEloRating = 2100,
                            Team2PreGameEloRating = 1800,
                            Team1NormalTimeScore = 1,
                            Team2NormalTimeScore = 2,
                            Team1ResultWonGame = false,
                            Team2ResultWonGame = true,
                            RoundCode = "G"
                        };

                        // Assert
                        Assert.AreEqual("SouthKorea win: upset (loss expected)", game.Team2OddsStatusText);
                    }

                    [TestMethod]
                    public void Game_Team2OddsStatusText_ExpectedLoss_ReturnsCorrectText()
                    {
                        // Arrange & Act
                        Game game = new Game
                        {
                            Team2Name = "Australia",
                            Team1PreGameEloRating = 2100,
                            Team2PreGameEloRating = 1700,
                            Team1NormalTimeScore = 3,
                            Team2NormalTimeScore = 0,
                            Team1ResultWonGame = true,
                            Team2ResultWonGame = false,
                            RoundCode = "G"
                        };

                        // Assert
                        Assert.AreEqual("Australia loss: expected", game.Team2OddsStatusText);
                    }

                    [TestMethod]
                    public void Game_Team2OddsStatusText_UnexpectedDraw_FavoriteExpectedWin_ReturnsCorrectText()
                    {
                        // Arrange & Act
                        Game game = new Game
                        {
                            Team2Name = "Italy",
                            Team1PreGameEloRating = 1900,
                            Team2PreGameEloRating = 2100,
                            Team1NormalTimeScore = 1,
                            Team2NormalTimeScore = 1,
                            Team1ResultWonGame = false,
                            Team2ResultWonGame = false,
                            RoundCode = "G"
                        };

                        // Assert
                        Assert.AreEqual("Italy draw: upset (win expected)", game.Team2OddsStatusText);
                    }

                    [TestMethod]
                    public void Game_Team2OddsStatusText_UnexpectedDraw_UnderdogExpectedLoss_ReturnsCorrectText()
                    {
                        // Arrange & Act
                        Game game = new Game
                        {
                            Team2Name = "NewZealand",
                            Team1PreGameEloRating = 2100,
                            Team2PreGameEloRating = 1700,
                            Team1NormalTimeScore = 2,
                            Team2NormalTimeScore = 2,
                            Team1ResultWonGame = false,
                            Team2ResultWonGame = false,
                            RoundCode = "G"
                        };

                        // Assert
                        Assert.AreEqual("NewZealand draw: upset (loss expected)", game.Team2OddsStatusText);
                    }

                    [TestMethod]
                    public void Game_Team2OddsStatusText_NoEloRating_ReturnsEmptyString()
                    {
                        // Arrange & Act
                        Game game = new Game
                        {
                            Team2Name = "NewTeam",
                            Team1PreGameEloRating = null,
                            Team2PreGameEloRating = null,
                            Team1NormalTimeScore = 1,
                            Team2NormalTimeScore = 1
                        };

                        // Assert
                        Assert.AreEqual("", game.Team2OddsStatusText);
                    }
                }
            }
