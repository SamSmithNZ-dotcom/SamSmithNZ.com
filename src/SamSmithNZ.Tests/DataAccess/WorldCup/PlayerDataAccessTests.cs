using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithnNZ.Tests;
using SamSmithNZ.Service.DataAccess.WorldCup;
using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Tests.DataAccess.WorldCup
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class PlayerDataAccessTests : BaseIntegrationTest
    {
                [TestMethod]
                public async Task PlayerDataAccess_GetPlayersByTournament_ReturnsPlayers()
                {
                    // Arrange
                    PlayerDataAccess da = new(base.Configuration);
                    int tournamentCode = 19;

                    // Act
                    List<Player> results = await da.GetPlayersByTournament(tournamentCode);

                    // Assert
                    Assert.IsNotNull(results);
                    Assert.IsTrue(results.Count > 0);
                }

                [TestMethod]
                public async Task PlayerDataAccess_GetList_ReturnsPlayers()
                {
                    // Arrange
                    PlayerDataAccess da = new(base.Configuration);
                    int gameCode = 1;

                    // Act
                    List<Player> results = await da.GetList(gameCode);

                    // Assert
                    Assert.IsNotNull(results);
                }

                [TestMethod]
                public async Task PlayerDataAccess_SaveItem_ExecutesSuccessfully()
                {
                    // Arrange
                    PlayerDataAccess da = new(base.Configuration);
                    Player player = new()
                    {
                        TeamCode = 1,
                        TournamentCode = 19,
                        Number = 10,
                        Position = "FW",
                        IsCaptain = false,
                        PlayerName = "Test Player",
                        DateOfBirth = new System.DateTime(1990, 1, 1),
                        ClubName = "Test Club"
                    };

                    // Act
                    bool result = await da.SaveItem(player);

                    // Assert - Just verify it executes without throwing
                    Assert.IsFalse(result == null);
                }
            }
        }
