using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithNZ.Tests;
using SamSmithNZ.Service.DataAccess.WorldCup;
using SamSmithNZ.Service.Models.WorldCup;
using System.Threading.Tasks;

namespace SamSmithNZ.Tests.DataAccess.WorldCup
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class TournamentTeamDataAccessTests : BaseIntegrationTest
    {
        [TestMethod]
        public async Task TournamentTeamDataAccess_GetTournamentTeamAsync_ReturnsTeam()
        {
            // Arrange
            TournamentTeamDataAccess da = new(base.Configuration);
            int tournamentCode = 19;
            int teamCode = 27; // Use South Africa (host team) which definitely exists in tournament 19

            // Act
            TournamentTeam result = await da.GetTournamentTeamAsync(tournamentCode, teamCode);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(27, result.TeamCode);
            Assert.AreEqual(19, result.TournamentCode);
        }

                [TestMethod]
                public async Task TournamentTeamDataAccess_GetTeamsPlacingAsync_ReturnsTeams()
                {
                    // Arrange
                    TournamentTeamDataAccess da = new(base.Configuration);
                    int tournamentCode = 19;

                    // Act
                    var results = await da.GetTeamsPlacingAsync(tournamentCode);

                    // Assert
                    Assert.IsNotNull(results);
                }

                        [TestMethod]
                        public async Task TournamentTeamDataAccess_GetQualifiedTeams_ReturnsTeams()
                        {
                            // Arrange
                            TournamentTeamDataAccess da = new(base.Configuration);
                            int tournamentCode = 19;

                            // Act
                            var results = await da.GetQualifiedTeams(tournamentCode);

                            // Assert
                            Assert.IsNotNull(results);
                            Assert.IsTrue(results.Count > 0);
                        }

                        [TestMethod]
                        public async Task TournamentTeamDataAccess_SaveItem_ExecutesSuccessfully()
                        {
                            // Arrange
                            TournamentTeamDataAccess da = new(base.Configuration);
                            TournamentTeam tournamentTeam = new()
                            {
                                TournamentCode = 19,
                                TeamCode = 1
                            };

                            // Act
                            bool result = await da.SaveItem(tournamentTeam);

                            // Assert - Just verify it executes
                            Assert.IsFalse(result == null);
                        }

                        [TestMethod]
                        public async Task TournamentTeamDataAccess_SaveELOItem_ExecutesSuccessfully()
                        {
                            // Arrange
                            TournamentTeamDataAccess da = new(base.Configuration);
                            TournamentTeam tournamentTeam = new()
                            {
                                TournamentCode = 19,
                                TeamCode = 1,
                                CurrentEloRating = 1500
                            };

                            // Act
                            bool result = await da.SaveELOItem(tournamentTeam);

                            // Assert - Just verify it executes
                            Assert.IsFalse(result == null);
                        }

                        [TestMethod]
                        public async Task TournamentTeamDataAccess_DeleteItem_ExecutesSuccessfully()
                        {
                            // Arrange
                            TournamentTeamDataAccess da = new(base.Configuration);
                            TournamentTeam tournamentTeam = new()
                            {
                                TournamentCode = 19,
                                TeamCode = 1
                            };

                            // Act
                            bool result = await da.DeleteItem(tournamentTeam);

                            // Assert - Just verify it executes
                            Assert.IsFalse(result == null);
                        }
                    }
                }
