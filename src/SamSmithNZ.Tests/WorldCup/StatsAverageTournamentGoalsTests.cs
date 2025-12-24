using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithnNZ.Tests;
using SamSmithNZ.Service.Controllers.WorldCup;
using SamSmithNZ.Service.DataAccess.WorldCup;
using SamSmithNZ.Service.Models.WorldCup;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SamSmithNZ.Tests.WorldCup
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class StatsAverageTournamentGoalsTests : BaseIntegrationTest
    {
        [TestMethod()]
        public async Task StatsAverageTournamentGoalsListTest()
        {
            //arrange
            StatsAverageTournamentGoalsController controller = new(new StatsAverageTournamentGoalsDataAccess(base.Configuration));
            int competitionCode = 1;

            //act
            List<StatsAverageTournamentGoals> results = await controller.GetStatsAverageTournamentGoalsList(competitionCode);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsNotEmpty(results);
            bool found19 = false;
            foreach (StatsAverageTournamentGoals item in results)
            {
                if (item.TournamentCode == 19)
                {
                    found19 = true;
                    Assert.AreEqual(64, item.TotalGamesCompleted);
                    Assert.AreEqual(145, item.TotalGoals);
                    Assert.AreEqual(2.27M, item.AverageGoalsPerGame);
                }
            }
            Assert.IsTrue(found19);
        }

        [TestMethod()]
        public async Task TournamentGetSouthAfricaTest()
        {
            //arrange
            StatsAverageTournamentGoalsController controller = new(new StatsAverageTournamentGoalsDataAccess(base.Configuration));
            int tournamentCode = 19;

            //act
            StatsAverageTournamentGoals item = await controller.GetStatsAverageTournamentGoals(tournamentCode);


            //assert
            Assert.IsTrue(item != null);
            Assert.AreEqual(64, item.TotalGamesCompleted);
            Assert.AreEqual(145, item.TotalGoals);
            Assert.AreEqual(2.27M, item.AverageGoalsPerGame);
        }

    }
}