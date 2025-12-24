using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithnNZ.Tests;
using SamSmithNZ.Service.Controllers.WorldCup;
using SamSmithNZ.Service.DataAccess.WorldCup;
using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Tests.WorldCup
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class TournamentImportStatusTests : BaseIntegrationTest
    {
        [TestMethod()]
        public async Task TournamentImportStatusAllFormerTournamentsListTest()
        {
            //arrange
            TournamentImportStatusController controller = new(new TournamentImportStatusDataAccess(base.Configuration));

            //act
            List<TournamentImportStatus> results = await controller.GetTournamentsImportStatus();

            //assert
            foreach (TournamentImportStatus item in results)
            {
                if (item.TournamentCode != 23 && //WC 2026
                    item.TournamentCode != 318) //Euro 2028
                {
                    //Import is complete and is exactly 1.0 (100%)
                    Assert.AreEqual(1.0M, item.ImportingTotalPercentComplete);
                }
            }
        }

        [TestMethod()]
        public async Task TournamentImportStatusListTest()
        {
            //arrange
            TournamentImportStatusController controller = new(new TournamentImportStatusDataAccess(base.Configuration));
            int competitionCode = 1;

            //act
            List<TournamentImportStatus> results = await controller.GetTournamentsImportStatus(competitionCode);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsNotEmpty(results);
            bool found19 = false;
            foreach (TournamentImportStatus item in results)
            {
                if (item.TournamentCode == 19)
                {
                    found19 = true;
                    TestSouthAfricaTournament(item);
                }
            }
            Assert.IsTrue(found19);
        }

        private static void TestSouthAfricaTournament(TournamentImportStatus item)
        {
            Assert.IsTrue(item.CompetitionCode == 1);
            Assert.IsTrue(item.TournamentCode == 19);
            Assert.IsTrue(item.TournamentYear == 2010);
            Assert.IsTrue(item.TotalGames == 64);
            Assert.IsTrue(item.TotalGamesCompleted == 64);
            Assert.IsTrue(item.TotalGoals == 145);
            Assert.IsTrue(item.TotalShootoutGoals == 14);
            Assert.IsTrue(item.TotalPenalties == 9);
            Assert.IsTrue(item.ImportingGamePercent == 1);
            Assert.IsTrue(item.ImportingGoalsPercent == 1);
            Assert.IsTrue(item.ImportingPenaltyShootoutGoalsPercent == 1);
            Assert.IsTrue(item.ImportingPlayerPercent == 1);
            Assert.IsTrue(item.ImportingTeamPercent == 1);
            Assert.IsTrue(item.ImportingTotalPercentComplete == 1);
        }

    }
}