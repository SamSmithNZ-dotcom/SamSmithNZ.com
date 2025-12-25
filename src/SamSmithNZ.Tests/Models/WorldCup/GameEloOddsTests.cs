using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithNZ.Service.Models.WorldCup;

namespace SamSmithNZ.Tests.Models.WorldCup
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class GameEloOddsTests
    {
        [TestMethod]
        public void Game_WithEloRatings_CalculatesOddsCorrectly()
        {
            // Arrange & Act
            Game game = new Game
            {
                Team1PreGameEloRating = 2000,
                Team2PreGameEloRating = 1900,
                RoundCode = "G" // Group stage - can end in draw
            };

            // Assert
            Assert.IsTrue(game.Team1ChanceToWin > 0);
            Assert.IsTrue(game.Team2ChanceToWin > 0);
            Assert.IsTrue(game.TeamChanceToDraw > 0);
            Assert.IsTrue(game.Team1ChanceToWin > game.Team2ChanceToWin); // Higher ELO = higher win chance
        }

        [TestMethod]
        public void Game_WithEqualEloRatings_HasEqualWinChances()
        {
            // Arrange & Act
            Game game = new Game
            {
                Team1PreGameEloRating = 2000,
                Team2PreGameEloRating = 2000,
                RoundCode = "G"
            };

            // Assert
            Assert.AreEqual(game.Team1ChanceToWin, game.Team2ChanceToWin);
            Assert.IsTrue(game.TeamChanceToDraw > 0);
        }

        [TestMethod]
        public void Game_PlayoffRound_CannotEndInDraw()
        {
            // Arrange & Act
            Game game = new Game
            {
                Team1PreGameEloRating = 2000,
                Team2PreGameEloRating = 1900,
                RoundCode = "16" // Round of 16 - cannot end in draw
            };

            // Assert
            Assert.IsTrue(game.Team1ChanceToWin > 0);
            Assert.IsTrue(game.Team2ChanceToWin > 0);
            Assert.AreEqual(0, game.TeamChanceToDraw); // No draw chance in playoffs
        }

        [TestMethod]
        public void Game_QuarterFinalRound_CannotEndInDraw()
        {
            // Arrange & Act
            Game game = new Game
            {
                Team1PreGameEloRating = 2000,
                Team2PreGameEloRating = 1950,
                RoundCode = "QF"
            };

            // Assert
            Assert.AreEqual(0, game.TeamChanceToDraw);
        }

        [TestMethod]
        public void Game_SemiFinalRound_CannotEndInDraw()
        {
            // Arrange & Act
            Game game = new Game
            {
                Team1PreGameEloRating = 2000,
                Team2PreGameEloRating = 1950,
                RoundCode = "SF"
            };

            // Assert
            Assert.AreEqual(0, game.TeamChanceToDraw);
        }

        [TestMethod]
        public void Game_ThirdPlaceRound_CannotEndInDraw()
        {
            // Arrange & Act
            Game game = new Game
            {
                Team1PreGameEloRating = 2000,
                Team2PreGameEloRating = 1950,
                RoundCode = "3P"
            };

            // Assert
            Assert.AreEqual(0, game.TeamChanceToDraw);
        }

        [TestMethod]
        public void Game_FinalRound_CannotEndInDraw()
        {
            // Arrange & Act
            Game game = new Game
            {
                Team1PreGameEloRating = 2000,
                Team2PreGameEloRating = 1950,
                RoundCode = "FF"
            };

            // Assert
            Assert.AreEqual(0, game.TeamChanceToDraw);
        }

        [TestMethod]
        public void Game_WithoutEloRatings_HasNegativeOdds()
        {
            // Arrange & Act
            Game game = new Game
            {
                Team1PreGameEloRating = null,
                Team2PreGameEloRating = null
            };

            // Assert
            Assert.AreEqual(-1, game.Team1ChanceToWin);
            Assert.AreEqual(-1, game.Team2ChanceToWin);
            Assert.AreEqual(-1, game.TeamChanceToDraw);
        }

        [TestMethod]
        public void Team1BeatOdds_WhenFavoriteWins_ReturnsTrue()
        {
            // Arrange & Act
            Game game = new Game
            {
                Team1PreGameEloRating = 2100,
                Team2PreGameEloRating = 1900,
                Team1ResultWonGame = true,
                RoundCode = "G"
            };

            // Assert
            Assert.IsTrue(game.Team1BeatOdds);
        }

        [TestMethod]
        public void Team1BeatOdds_WhenUnderdogWins_ReturnsTrue()
        {
            // Arrange & Act
            Game game = new Game
            {
                Team1PreGameEloRating = 1800,
                Team2PreGameEloRating = 2100,
                Team1ResultWonGame = true,
                RoundCode = "G"
            };

            // Assert
            Assert.IsTrue(game.Team1BeatOdds);
        }

        [TestMethod]
        public void Team1BeatOdds_WhenFavoriteLoses_ReturnsFalse()
        {
            // Arrange & Act
            Game game = new Game
            {
                Team1PreGameEloRating = 2100,
                Team2PreGameEloRating = 1900,
                Team1ResultWonGame = false,
                RoundCode = "G"
            };

            // Assert
            Assert.IsFalse(game.Team1BeatOdds);
        }

        [TestMethod]
        public void Game_OddsSum_ApproximatelyEquals100Percent()
        {
            // Arrange & Act
            Game game = new Game
            {
                Team1PreGameEloRating = 2000,
                Team2PreGameEloRating = 1950,
                RoundCode = "G"
            };

            // Assert
            double total = game.Team1ChanceToWin + game.Team2ChanceToWin + game.TeamChanceToDraw;
            Assert.IsTrue(total >= 99.9 && total <= 100.1); // Allow for rounding
        }

        [TestMethod]
        public void Game_LargeEloGap_ReflectsInOdds()
        {
            // Arrange & Act
            Game game = new Game
            {
                Team1PreGameEloRating = 2200,
                Team2PreGameEloRating = 1700,
                RoundCode = "G"
            };

            // Assert
                            Assert.IsTrue(game.Team1ChanceToWin > 70); // Strong favorite
                            Assert.IsTrue(game.Team2ChanceToWin < 20); // Big underdog
                        }
                    }
                }
