using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithNZ.Tests;
using SamSmithNZ.Service.DataAccess.WorldCup;
using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Tests.DataAccess.WorldCup
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class TeamDataAccessTests : BaseIntegrationTest
    {
        [TestMethod]
        public async Task TeamDataAccess_GetList_ReturnsTeams()
        {
            // Arrange
            TeamDataAccess da = new(base.Configuration);

            // Act
            List<Team> results = await da.GetList();

            // Assert
            Assert.IsNotNull(results);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod]
        public async Task TeamDataAccess_GetItem_ReturnsTeam()
        {
            // Arrange
            TeamDataAccess da = new(base.Configuration);
            int teamCode = 1;

            // Act
            Team result = await da.GetItem(teamCode);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.TeamCode);
        }
    }
}
