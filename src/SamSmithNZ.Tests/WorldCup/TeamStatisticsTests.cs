using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithNZ.Tests;
using SamSmithNZ.Service.Controllers.WorldCup;
using SamSmithNZ.Service.DataAccess.WorldCup;
using SamSmithNZ.Service.DataAccess.WorldCup.Interfaces;
using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Tests.WorldCup
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class TeamStatisticsTests : BaseIntegrationTest
    {

        [TestMethod()]
        public async Task GetMatchUpForGameTest()
        {
            //arrange
            TeamStatisticsController controller = new(
                new TeamDataAccess(base.Configuration),
                new GameDataAccess(base.Configuration));
            int team1Code = 11; //France
            int team2Code = 43; //Poland

            //act
            TeamMatchup result = await controller.GetTeamMatchup(team1Code, team2Code);

            //assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Team1Statistics);
            Assert.IsNotNull(result.Team2Statistics);
            Assert.IsNotNull(result.Games);
            Assert.IsTrue(result.Games.Count >= 2); //At least 2 - 2022 + 1982        
            foreach (Game game in result.Games)
            {
                if (game.TournamentCode == 22)
                {
                    Assert.AreEqual(1983, game.Team1PreGameEloRating);
                    Assert.AreEqual(1835, game.Team2PreGameEloRating);
                    Assert.AreEqual(2028, game.Team1PostGameEloRating);
                    Assert.AreEqual(1790, game.Team2PostGameEloRating);
                    Assert.AreEqual("16", game.RoundCode);
                    Assert.IsFalse(game.GameCanEndInADraw);
                    Assert.AreEqual(70.1, game.Team1ChanceToWin);
                    Assert.AreEqual(29.9, game.Team2ChanceToWin);
                    Assert.AreEqual(0, game.TeamChanceToDraw);
                }
                else if (game.TournamentCode == 317)
                {
                    Assert.AreEqual(2085, game.Team1PreGameEloRating);
                    Assert.AreEqual(1674, game.Team2PreGameEloRating);
                    Assert.AreEqual(2044, game.Team1PostGameEloRating);
                    Assert.AreEqual(1715, game.Team2PostGameEloRating);
                    Assert.AreEqual("D", game.RoundCode);
                    Assert.IsTrue(game.GameCanEndInADraw);
                    Assert.AreEqual(89.12, game.Team1ChanceToWin);
                    Assert.AreEqual(8.37, game.Team2ChanceToWin);
                    Assert.AreEqual(2.51, game.TeamChanceToDraw);
                }

            }
        }

    }

}
