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
    public class PenaltyShootoutGoalDataAccessTests : BaseIntegrationTest
    {
        [TestMethod]
        public async Task PenaltyShootoutGoalDataAccess_GetList_ReturnsGoals()
        {
            // Arrange
            PenaltyShootoutGoalDataAccess da = new(base.Configuration);
            int gameCode = 7328;

            // Act
            List<PenaltyShootoutGoal> results = await da.GetList(gameCode);

            // Assert
            Assert.IsNotNull(results);
        }
    }
}
