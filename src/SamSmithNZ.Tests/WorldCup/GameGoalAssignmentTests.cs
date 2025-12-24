using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithnNZ.Tests;
using SamSmithNZ.Service.DataAccess.WorldCup;
using SamSmithNZ.Service.Models.WorldCup;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Tests.WorldCup
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class GameGoalAssignmentTests : BaseIntegrationTest
    {
        [TestMethod]
        public async Task GameGoalAssignmentsExistTest()
        {
            //arrange
            GameGoalAssignmentDataAccess da = new(base.Configuration);
            int tournamentCode = 19;

            //act
            List<GameGoalAssignment> results = await da.GetList(tournamentCode);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsNotEmpty(results);
        }

        [TestMethod()]
        public async Task GameGoalAssignmentsFirstItemTest()
        {
            //arrange
            GameGoalAssignmentDataAccess da = new(base.Configuration);
            int tournamentCode = 19;

            //act
            List<GameGoalAssignment> results = await da.GetList(tournamentCode);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsNotEmpty(results);
            Assert.IsTrue(results[0].GameCode > 0);
            Assert.IsTrue(results[0].GameNumber > 0);
            Assert.IsTrue(results[0].GameTime > DateTime.MinValue);
            Assert.IsTrue(results[0].Score != "");
            Assert.IsTrue(results[0].Team1Name != "");
            Assert.IsTrue(results[0].Team2Name != "");
            Assert.IsTrue(results[0].TotalGameTableGoals >= 0);
            Assert.IsTrue(results[0].TotalGameTablePenaltyShootoutGoals >= 0);
            Assert.IsTrue(results[0].TotalGoalTableGoals >= 0);
            Assert.IsTrue(results[0].TotalPenaltyShootoutTableGoals >= 0);
        }
    }
}
