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
    public class PenaltyShootoutTests : BaseIntegrationTest
    {
        [TestMethod]
        public async Task PenaltyShootoutsExistTest()
        {
            //arrange
            PenaltyShootoutGoalDataAccess da = new(base.Configuration);
            int gameCode = 7376;

            //act
            List<PenaltyShootoutGoal> results = await da.GetList(gameCode);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsNotEmpty(results);
        }

        [TestMethod()]
        public async Task PenaltyShootoutsFirstItemTest()
        {
            //arrange
            PenaltyShootoutGoalDataAccess da = new(base.Configuration);
            int gameCode = 7376;

            //act
            List<PenaltyShootoutGoal> results = await da.GetList(gameCode);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsNotEmpty(results);
            Assert.IsTrue(results[0].GameCode > 0);
            Assert.IsTrue(results[0].PenaltyCode > 0);
            Assert.IsTrue(results[0].PenaltyOrder == 1);
            Assert.IsTrue(results[0].PlayerCode > 0);
            Assert.IsTrue(results[0].PlayerName == "David Luiz");
            Assert.IsTrue(results[0].Scored == true);
            Assert.IsTrue(results[1].GameCode > 0);
            Assert.IsTrue(results[1].PenaltyCode > 0);
            Assert.IsTrue(results[1].PenaltyOrder == 2);
            Assert.IsTrue(results[1].PlayerCode > 0);
            Assert.IsTrue(results[1].PlayerName == "Mauricio Pinilla");
            Assert.IsTrue(results[1].Scored == false);
        }

        [TestMethod()]
        public async Task PenaltyShootoutsSaveItemTest()
        {
            //arrange
            PenaltyShootoutGoalDataAccess da = new(base.Configuration);
            int gameCode = 7376;

            //act
            List<PenaltyShootoutGoal> results = await da.GetList(gameCode);
            //Save the two goals
            await da.SaveItem(results[0]);
            await da.SaveItem(results[1]);
            //Test that the results haven't changed
            results = await da.GetList(gameCode);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsNotEmpty(results);
            Assert.IsTrue(results[0].GameCode > 0);
            Assert.IsTrue(results[0].PenaltyCode > 0);
            Assert.IsTrue(results[0].PenaltyOrder == 1);
            Assert.IsTrue(results[0].PlayerCode > 0);
            Assert.IsTrue(results[0].PlayerName == "David Luiz");
            Assert.IsTrue(results[0].Scored == true);
            Assert.IsTrue(results[1].GameCode > 0);
            Assert.IsTrue(results[1].PenaltyCode > 0);
            Assert.IsTrue(results[1].PenaltyOrder == 2);
            Assert.IsTrue(results[1].PlayerCode > 0);
            Assert.IsTrue(results[1].PlayerName == "Mauricio Pinilla");
            Assert.IsTrue(results[1].Scored == false);
        }
    }
}
