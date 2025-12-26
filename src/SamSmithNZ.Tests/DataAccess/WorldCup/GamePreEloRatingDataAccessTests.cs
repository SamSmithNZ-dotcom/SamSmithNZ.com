using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithNZ.Tests;
using SamSmithNZ.Service.DataAccess.WorldCup;
using System.Threading.Tasks;

namespace SamSmithNZ.Tests.DataAccess.WorldCup
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class GamePreEloRatingDataAccessTests : BaseIntegrationTest
    {
        [TestMethod]
        public async Task GamePreEloRatingDataAccess_GetGamePreELORatings_ReturnsRatings()
        {
            // Arrange
            GamePreEloRatingDataAccess da = new(base.Configuration);
            int tournamentCode = 19;
            int gameCode = 7328;

            // Act
            var result = await da.GetGamePreELORatings(tournamentCode, gameCode);

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
