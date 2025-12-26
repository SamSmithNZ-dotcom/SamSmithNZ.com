using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithNZ.Tests;
using SamSmithNZ.Service.DataAccess.WorldCup;
using System.Threading.Tasks;

namespace SamSmithNZ.Tests.DataAccess.WorldCup
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class EloRatingDataAccessTests : BaseIntegrationTest
    {
        [TestMethod]
        public async Task EloRatingDataAccess_UpdateGameELORating_ExecutesSuccessfully()
        {
            // Arrange
            EloRatingDataAccess da = new(base.Configuration);
            int tournamentCode = 19;
            int gameCode = 7328;

            // Act
            bool result = await da.UpdateGameELORating(tournamentCode, gameCode);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task EloRatingDataAccess_UpdateTournamentELORatings_ExecutesSuccessfully()
        {
            // Arrange
            EloRatingDataAccess da = new(base.Configuration);
            int tournamentCode = 19;

            // Act
            bool result = await da.UpdateTournamentELORatings(tournamentCode);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
