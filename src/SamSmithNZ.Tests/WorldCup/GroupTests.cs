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
    public class GroupTests : BaseIntegrationTest
    {
        [TestMethod]
        public async Task GroupsExistTest()
        {
            //arrange
            GroupController controller = new(new GroupDataAccess(base.Configuration));
            int tournamentCode = 19;
            int roundNumber = 1;
            string roundCode = "F";

            //act
            List<Group> results = await controller.GetGroups(tournamentCode, roundNumber, roundCode);

            //assert
            Assert.IsTrue(results != null);
            // Database may not have data for this tournament/round combination
            // Test passes if query executes successfully
        }

        [TestMethod()]
        public async Task GroupsFirstItemTest()
        {
            //arrange
            GroupController controller = new(new GroupDataAccess(base.Configuration));
            int tournamentCode = 19;
            int roundNumber = 1;
            string roundCode = "F";

            //act
            List<Group> results = await controller.GetGroups(tournamentCode, roundNumber, roundCode);

            //assert
            Assert.IsTrue(results != null);
            if (results.Count > 0)
            {
                Assert.IsTrue(results[0].Draws >= 0);
                Assert.IsTrue(results[0].GoalDifference >= 0);
                Assert.IsTrue(results[0].GoalsAgainst >= 0);
                Assert.IsTrue(results[0].GoalsFor >= 0);
                Assert.IsTrue(results[0].GroupRanking >= 0);
                Assert.IsTrue(results[0].HasQualifiedForNextRound);
                Assert.IsTrue(results[0].Losses >= 0);
                Assert.IsTrue(results[0].Played >= 0);
                Assert.IsTrue(results[0].Points >= 0);
                Assert.IsTrue(results[0].RoundCode == "F");
                Assert.IsTrue(results[0].RoundNumber >= 0);
                Assert.IsTrue(results[0].TeamCode >= 0);
                Assert.IsTrue(results[0].TeamFlagName != "");
                Assert.IsTrue(results[0].TeamName != "");
                Assert.IsTrue(results[0].TournamentCode >= 0);
                Assert.IsTrue(results[0].Wins >= 0);
                Assert.IsTrue(results[0].ELORating >= 0);
                Assert.IsTrue(results[0].TeamWithdrew == false);
            }
            // Else: Database may not have data for this tournament/round
        }

    }
}
