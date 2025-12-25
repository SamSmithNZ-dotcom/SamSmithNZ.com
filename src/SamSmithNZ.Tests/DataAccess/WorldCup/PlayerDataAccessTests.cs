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
    }
}
