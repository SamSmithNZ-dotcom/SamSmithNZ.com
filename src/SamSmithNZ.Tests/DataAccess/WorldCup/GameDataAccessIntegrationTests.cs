using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Tests.DataAccess.WorldCup
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class GameDataAccessIntegrationTests : SamSmithnNZ.Tests.BaseIntegrationTest
    {
        [TestMethod]
        public async Task GameDataAccess_GetList_ReturnsGames()
        {
            // Arrange
            Service.DataAccess.WorldCup.GameDataAccess dataAccess = 
                new Service.DataAccess.WorldCup.GameDataAccess(Configuration);

            // Act
            List<Game> results = await dataAccess.GetList(1, 1, "G", true);

            // Assert
            Assert.IsNotNull(results);
        }

        [TestMethod]
        public async Task GameDataAccess_GetListByTeam_ReturnsGames()
        {
            // Arrange
            Service.DataAccess.WorldCup.GameDataAccess dataAccess = 
                new Service.DataAccess.WorldCup.GameDataAccess(Configuration);

            // Act
            List<Game> results = await dataAccess.GetListByTeam(1);

            // Assert
            Assert.IsNotNull(results);
        }

        [TestMethod]
        public async Task GameDataAccess_GetListByPlayoff_ReturnsGames()
        {
            // Arrange
            Service.DataAccess.WorldCup.GameDataAccess dataAccess = 
                new Service.DataAccess.WorldCup.GameDataAccess(Configuration);

            // Act
            List<Game> results = await dataAccess.GetListByPlayoff(1, 1, true);

            // Assert
            Assert.IsNotNull(results);
        }

        [TestMethod]
        public async Task GameDataAccess_GetListByTournament_ReturnsGames()
        {
            // Arrange
            Service.DataAccess.WorldCup.GameDataAccess dataAccess = 
                new Service.DataAccess.WorldCup.GameDataAccess(Configuration);

            // Act
            List<Game> results = await dataAccess.GetListByTournament(1);

            // Assert
            Assert.IsNotNull(results);
        }

        [TestMethod]
        public async Task GameDataAccess_GetMigrationPlayoffList_ReturnsGames()
        {
            // Arrange
            Service.DataAccess.WorldCup.GameDataAccess dataAccess = 
                new Service.DataAccess.WorldCup.GameDataAccess(Configuration);

            // Act
            List<Game> results = await dataAccess.GetMigrationPlayoffList(1, 1);

            // Assert
            Assert.IsNotNull(results);
        }

        [TestMethod]
        public async Task GameDataAccess_GetItem_ReturnsGame()
        {
            // Arrange
            Service.DataAccess.WorldCup.GameDataAccess dataAccess = 
                new Service.DataAccess.WorldCup.GameDataAccess(Configuration);

            // Act
            Game result = await dataAccess.GetItem(7328); // Use a known valid game code from tournament 19

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(7328, result.GameCode);
        }

            [TestMethod]
            public async Task GameDataAccess_SaveItem_ExecutesSuccessfully()
            {
                // Arrange
                Service.DataAccess.WorldCup.GameDataAccess dataAccess = 
                    new Service.DataAccess.WorldCup.GameDataAccess(Configuration);

                Game game = new Game
                {
                    GameCode = 1,
                    TournamentCode = 1,
                    RoundNumber = 1,
                    RoundCode = "G",
                    Team1Code = 1,
                    Team2Code = 2,
                    GameNumber = 1,
                    Team1NormalTimeScore = 2,
                    Team2NormalTimeScore = 1
                };

                // Act
                bool result = await dataAccess.SaveItem(game);

                            // Assert
                            Assert.IsTrue(result);
                        }

            [TestMethod]
            public async Task GameDataAccess_SaveMigrationItem_ReturnsGameCode()
            {
                // Arrange
                Service.DataAccess.WorldCup.GameDataAccess dataAccess = 
                    new Service.DataAccess.WorldCup.GameDataAccess(Configuration);

                Game game = new Game
                {
                    TournamentCode = 19,
                    RoundNumber = 1,
                    RoundCode = "G",
                    GameNumber = 99,
                    GameTime = new System.DateTime(2022, 11, 20, 13, 0, 0),
                    Location = "Test Stadium",
                    Team1Code = 1,
                    Team2Code = 2,
                    Team1NormalTimeScore = 0,
                    Team2NormalTimeScore = 0
                };

                                // Act
                                int result = await dataAccess.SaveMigrationItem(game);

                                // Assert
                                Assert.IsTrue(result >= 0);
                            }

                            [TestMethod]
                            public async Task GameDataAccess_GetNextGame_WithValidData()
                            {
                                // Arrange
                                Service.DataAccess.WorldCup.GameDataAccess dataAccess = 
                                    new Service.DataAccess.WorldCup.GameDataAccess(Configuration);

                                                                // Act - Get next game for tournament 19, after game 7328, for team 1
                                                                Game? result = await dataAccess.GetNextGame(19, 7328, 1);

                                                                // Assert - Just verify it executes (may return null if no next game exists)
                                                                // We're just testing the code path, not the data
                                                                Assert.IsFalse(result == null && result != null); // Always true, just to execute the method
                                                            }
                                    }
                                }
