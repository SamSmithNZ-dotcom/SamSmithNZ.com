using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithnNZ.Tests;
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
            int teamCode = 1;

            // Act
            TournamentTeam result = await da.GetTournamentTeamAsync(tournamentCode, teamCode);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.TeamCode);
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
            Assert.IsTrue(results.Count > 0);
        }
    }
}
