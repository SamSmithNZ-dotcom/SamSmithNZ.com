using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithNZ.Tests;
using SamSmithNZ.Service.Controllers.WorldCup;
using SamSmithNZ.Service.DataAccess.WorldCup;
using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Tests.WorldCup
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class TournamentTopGoalScorerTests : BaseIntegrationTest
    {
        [TestMethod]
        public async Task TournamentTopGoalScorerTest()
        {
            //arrange
            TournamentTopGoalScorerController controller = new(new TournamentTopGoalScorerDataAccess(base.Configuration));
            int tournamentCode = 21;
            bool getOwnGoals = false;

            //act
            List<TournamentTopGoalScorer> results = await controller.GetTournamentTopGoalScorers(tournamentCode, getOwnGoals);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsNotEmpty(results);
            Assert.IsTrue(results[0].PlayerName == "Harry Kane");
            Assert.IsTrue(results[0].FlagName != "");
            Assert.IsTrue(results[0].GoalsScored > 0);
            Assert.IsTrue(results[0].IsActive == false);
            Assert.IsTrue(results[0].TeamCode == 10);
            Assert.IsTrue(results[0].TeamName == "England");
        }

        [TestMethod]
        public async Task TournamentTopOwnGoalScorerTest()
        {
            //arrange
            TournamentTopGoalScorerController controller = new(new TournamentTopGoalScorerDataAccess(base.Configuration));
            int tournamentCode = 21;
            bool getOwnGoals = true;

            //act
            List<TournamentTopGoalScorer> results = await controller.GetTournamentTopGoalScorers(tournamentCode, getOwnGoals);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsNotEmpty(results);
            Assert.IsTrue(results[0].PlayerName == "Ahmed Fathy");
            Assert.IsTrue(results[0].FlagName != "");
            Assert.IsTrue(results[0].GoalsScored > 0);
            Assert.IsTrue(results[0].IsActive == false);
            Assert.IsTrue(results[0].TeamCode == 66);
            Assert.IsTrue(results[0].TeamName == "Egypt");
        }
    }
}
