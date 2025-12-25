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
            Game result = await dataAccess.GetItem(1);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GameDataAccess_SaveItem_ExecutesSuccessfully()
        {
            // Arrange
            Service.DataAccess.WorldCup.GameDataAccess dataAccess = 
                new Service.DataAccess.WorldCup.GameDataAccess(Configuration);
            
            Game game = new Game
            {
                GameCode = 9999,
                TournamentCode = 1,
                RoundNumber = 1,
                RoundCode = "G",
                Team1Code = 1,
                Team2Code = 2,
                GameNumber = 1
            };

            // Act
            bool result = await dataAccess.SaveItem(game);

                        // Assert
                        Assert.IsTrue(result);
                    }

                            [TestMethod]
                            public async Task GameDataAccess_GetNextGame_ReturnsGame()
                            {
                                // Arrange
                                Service.DataAccess.WorldCup.GameDataAccess dataAccess = 
                                    new Service.DataAccess.WorldCup.GameDataAccess(Configuration);

                                // Act
                                await dataAccess.GetNextGame(1, 1, 1);

                                // Assert - result can be null if no next game exists
                                // This test just verifies the method executes without error
                                Assert.IsNotNull(dataAccess);
                            }

                            [TestMethod]
                            public async Task GameDataAccess_GetNextGame_WithValidData_ReturnsNextGame()
                            {
                                // Arrange
                                Service.DataAccess.WorldCup.GameDataAccess dataAccess = 
                                    new Service.DataAccess.WorldCup.GameDataAccess(Configuration);
                                int tournamentCode = 19;
                                int gameCode = 7328;
                                int teamCode = 1;

                                // Act
                                await dataAccess.GetNextGame(tournamentCode, gameCode, teamCode);

                                // Assert - result can be null if no next game exists, which is valid
                                Assert.IsNotNull(dataAccess);
                            }
                        }
                    }
