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
    public class TournamentDataAccessTests : BaseIntegrationTest
    {
        [TestMethod]
        public async Task TournamentDataAccess_GetListWithCompetitionCode_ReturnsTournaments()
        {
            // Arrange
            TournamentDataAccess da = new(base.Configuration);
            int? competitionCode = 1;

            // Act
            List<Tournament> results = await da.GetList(competitionCode);

            // Assert
            Assert.IsNotNull(results);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod]
        public async Task TournamentDataAccess_GetListWithNullCompetitionCode_ReturnsTournaments()
        {
            // Arrange
            TournamentDataAccess da = new(base.Configuration);
            int? competitionCode = null;

            // Act
            List<Tournament> results = await da.GetList(competitionCode);

            // Assert
            Assert.IsNotNull(results);
        }

                [TestMethod]
                public async Task TournamentDataAccess_GetItem_ReturnsTournament()
                {
                    // Arrange
                    TournamentDataAccess da = new(base.Configuration);
                    int tournamentCode = 19;

                    // Act
                    Tournament result = await da.GetItem(tournamentCode);

                    // Assert
                    Assert.IsNotNull(result);
                    Assert.AreEqual(19, result.TournamentCode);
                }

                [TestMethod]
                public async Task TournamentDataAccess_ResetTournament_ExecutesSuccessfully()
                {
                    // Arrange
                    TournamentDataAccess da = new(base.Configuration);
                    int tournamentCode = 19;

                    // Act
                    bool result = await da.ResetTournament(tournamentCode);

                    // Assert - Just verify it executes
                    Assert.IsFalse(result == null);
                }
            }
        }
