using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithNZ.Tests;
using SamSmithNZ.Service.DataAccess.WorldCup;
using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Tests.WorldCup
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class GoalInsightsTests : BaseIntegrationTest
    {
        [TestMethod()]
        public async Task GoalInsightsRegularTimeTest()
        {
            //arrange
            GoalInsightsDataAccess da = new(base.Configuration);
            bool analyzeExtraTime = false;

            //act
            List<GoalInsight> results = await da.GetList(analyzeExtraTime);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsNotEmpty(results);
            Assert.IsTrue(results[0].GoalCount > 0);
            Assert.IsTrue(results[0].GoalCountPercent > 0);
            Assert.IsTrue(results[0].GoalTime == 1);
        }

        [TestMethod()]
        public async Task GoalInsightsExtraTimeTest()
        {
            //arrange
            GoalInsightsDataAccess da = new(base.Configuration);
            bool analyzeExtraTime = true;

            //act
            List<GoalInsight> results = await da.GetList(analyzeExtraTime);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsNotEmpty(results);
            Assert.IsTrue(results[0].GoalCount > 0);
            Assert.IsTrue(results[0].GoalCountPercent > 0);
            Assert.IsTrue(results[0].GoalTime == 91);
        }

    }
}