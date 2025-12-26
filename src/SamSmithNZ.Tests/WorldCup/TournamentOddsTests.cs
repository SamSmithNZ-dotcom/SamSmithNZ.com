using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithNZ.Tests;
using SamSmithNZ.Service.Controllers.WorldCup;
using SamSmithNZ.Service.DataAccess.WorldCup;
using SamSmithNZ.Service.Models.WorldCup;
using System.Threading.Tasks;
using System.Collections.Generic;
using SamSmithNZ.Service;
using System.Diagnostics;

namespace SamSmithNZ.Tests.WorldCup
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class TournamentOddsTests : BaseIntegrationTest
    {
        [TestMethod()]
        public async Task TouramentEuro2024OddsTest()
        {
            //arrange
            GameController gameController = new(new GameDataAccess(base.Configuration));
            int tournamentCode = 317;

            //act
            List<Game> games = await gameController.GetGamesByTournament(tournamentCode);
            foreach (Game game in games)
            {
                if (game.RoundCode == "A")
                {
                    double team1ChanceToWin = game.Team1ChanceToWin;
                    double team2ChanceToWin = game.Team2ChanceToWin;
                    double teamChanceToDraw = game.TeamChanceToDraw;
                    Debug.WriteLine(game.Team1Name + " has a " + team1ChanceToWin + "% chance to win against " + game.Team2Name + ", with a " + teamChanceToDraw + "% chance to draw");
                }
            }

        }
    }
}