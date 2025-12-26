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
    public class GameGoalAssignmentDataAccessTests : BaseIntegrationTest
    {
        [TestMethod]
        public async Task GameGoalAssignmentDataAccess_GetList_ReturnsAssignments()
        {
            // Arrange
            GameGoalAssignmentDataAccess da = new(base.Configuration);
            int tournamentCode = 19;

            // Act
            List<GameGoalAssignment> results = await da.GetList(tournamentCode);

            // Assert
            Assert.IsNotNull(results);
        }
    }
}
