using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithnNZ.Tests;
using SamSmithNZ.Service.DataAccess.WorldCup;
using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Tests.WorldCup
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class GoalTests : BaseIntegrationTest
    {
        [TestMethod()]
        public async Task GoalsFirstItemTest()
        {
            //arrange
            GoalDataAccess da = new(base.Configuration);
            int gameCode = 7328; //This is the perfect test game. It has a normal goal, injury time goal, penalty and own goal

            //act
            List<Goal> results = await da.GetListByGame(gameCode);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsNotEmpty(results);
            Assert.IsTrue(results[0].GameCode > 0);
            Assert.IsTrue(results[0].GoalCode > 0);
            Assert.IsTrue(results[0].GoalTime == 11);
            Assert.IsTrue(results[0].InjuryTime == 0);
            Assert.IsTrue(results[0].IsOwnGoal == true);
            Assert.IsTrue(results[0].IsPenalty == false);
            Assert.IsTrue(results[0].PlayerCode > 0);
            Assert.IsTrue(results[0].PlayerName == "Marcelo");
            Assert.IsTrue(results[1].GameCode > 0);
            Assert.IsTrue(results[1].GoalCode > 0);
            Assert.IsTrue(results[1].GoalTime == 29);
            Assert.IsTrue(results[1].InjuryTime == 0);
            Assert.IsTrue(results[1].IsOwnGoal == false);
            Assert.IsTrue(results[1].IsPenalty == false);
            Assert.IsTrue(results[1].PlayerCode > 0);
            Assert.IsTrue(results[1].PlayerName == "Neymar");
            Assert.IsTrue(results[2].GameCode > 0);
            Assert.IsTrue(results[2].GoalCode > 0);
            Assert.IsTrue(results[2].GoalTime == 71);
            Assert.IsTrue(results[2].InjuryTime == 0);
            Assert.IsTrue(results[2].IsOwnGoal == false);
            Assert.IsTrue(results[2].IsPenalty == true);
            Assert.IsTrue(results[2].PlayerCode > 0);
            Assert.IsTrue(results[2].PlayerName == "Neymar");
            Assert.IsTrue(results[3].GameCode > 0);
            Assert.IsTrue(results[3].GoalCode > 0);
            Assert.IsTrue(results[3].GoalTime == 90);
            Assert.IsTrue(results[3].InjuryTime == 1);
            Assert.IsTrue(results[3].IsOwnGoal == false);
            Assert.IsTrue(results[3].IsPenalty == false);
            Assert.IsTrue(results[3].PlayerCode > 0);
            Assert.IsTrue(results[3].PlayerName == "Oscar");
        }

        [TestMethod()]
        public async Task GoalsSaveItemTest()
        {
            //arrange
            GoalDataAccess da = new(base.Configuration);
            int gameCode = 7328; //This is the perfect test game. It has a normal goal, injury time goal, penalty and own goal

            //act
            List<Goal> results = await da.GetListByGame(gameCode);
            //Save the four goals
            await da.SaveItem(results[0]);
            await da.SaveItem(results[1]);
            await da.SaveItem(results[2]);
            await da.SaveItem(results[3]);
            //Test that the results haven't changed
            results = await da.GetListByGame(gameCode);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsNotEmpty(results);
            Assert.IsTrue(results[0].GameCode > 0);
            Assert.IsTrue(results[0].GoalCode > 0);
            Assert.IsTrue(results[0].GoalTime == 11);
            Assert.IsTrue(results[0].InjuryTime == 0);
            Assert.IsTrue(results[0].IsOwnGoal == true);
            Assert.IsTrue(results[0].IsPenalty == false);
            Assert.IsTrue(results[0].PlayerCode > 0);
            Assert.IsTrue(results[0].PlayerName == "Marcelo");
            Assert.IsTrue(results[1].GameCode > 0);
            Assert.IsTrue(results[1].GoalCode > 0);
            Assert.IsTrue(results[1].GoalTime == 29);
            Assert.IsTrue(results[1].InjuryTime == 0);
            Assert.IsTrue(results[1].IsOwnGoal == false);
            Assert.IsTrue(results[1].IsPenalty == false);
            Assert.IsTrue(results[1].PlayerCode > 0);
            Assert.IsTrue(results[1].PlayerName == "Neymar");
            Assert.IsTrue(results[2].GameCode > 0);
            Assert.IsTrue(results[2].GoalCode > 0);
            Assert.IsTrue(results[2].GoalTime == 71);
            Assert.IsTrue(results[2].InjuryTime == 0);
            Assert.IsTrue(results[2].IsOwnGoal == false);
            Assert.IsTrue(results[2].IsPenalty == true);
            Assert.IsTrue(results[2].PlayerCode > 0);
            Assert.IsTrue(results[2].PlayerName == "Neymar");
            Assert.IsTrue(results[3].GameCode > 0);
            Assert.IsTrue(results[3].GoalCode > 0);
            Assert.IsTrue(results[3].GoalTime == 90);
            Assert.IsTrue(results[3].InjuryTime == 1);
            Assert.IsTrue(results[3].IsOwnGoal == false);
            Assert.IsTrue(results[3].IsPenalty == false);
            Assert.IsTrue(results[3].PlayerCode > 0);
            Assert.IsTrue(results[3].PlayerName == "Oscar");
        }

        [TestMethod]
        public async Task GoalsInjuryTimeAreInRightPlaceTest()
        {
            //arrange
            GoalDataAccess da = new(base.Configuration);

            //act
            List<Goal> results = await da.GetList();
            int injuryTimeGoalsNotInInjuryTime = 0;
            int gameCode = 0;
            foreach (Goal item in results)
            {
                if (item.GoalTime < 90 && item.GoalTime != 45 && item.InjuryTime > 0)
                {
                    gameCode = item.GameCode;
                    injuryTimeGoalsNotInInjuryTime++;
                }
                else if (item.GoalTime > 90 && item.GoalTime > 105 && item.GoalTime < 120 && item.InjuryTime > 0)
                {
                    gameCode = item.GameCode;
                    injuryTimeGoalsNotInInjuryTime++;
                }
                else if (item.GoalTime > 90 && item.GoalTime < 105 && item.InjuryTime > 0)
                {
                    gameCode = item.GameCode;
                    injuryTimeGoalsNotInInjuryTime++;
                }
            }

            //assert
            Assert.IsTrue(results != null);
            Assert.IsNotEmpty(results);
            Assert.AreEqual(0, gameCode);
            Assert.AreEqual(0, injuryTimeGoalsNotInInjuryTime);
        }
    }
}
