using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithNZ.Tests;
using SamSmithNZ.Service.Controllers.WorldCup;
using SamSmithNZ.Service.DataAccess.WorldCup;
using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamSmithNZ.Tests.WorldCup
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class ELOTournamentTests : BaseIntegrationTest
    {
        //[TestMethod()]
        //public async Task RefreshELORatingsForEntireCompetitonTest()
        //{
        //    //arrange
        //    EloRatingController controller = new(
        //        new EloRatingDataAccess(base.Configuration),
        //        new GameDataAccess(base.Configuration));
        //    int competitionCode = 1;
        //    TournamentController controllerT = new(new TournamentDataAccess(base.Configuration));

        //    //act
        //    List<Tournament> tournaments = await controllerT.GetTournaments(competitionCode);
        //    foreach (Tournament item in tournaments)
        //    {
        //        //Only process tournaments that have started
        //        if (item.MinGameTime != null)
        //        {
        //            bool result = await controller.RefreshTournamentELORatings(item.TournamentCode);

        //            //assert
        //            if (result == false)
        //            {
        //                Assert.AreEqual(0, item.TournamentCode);
        //            }
        //            Assert.IsTrue(result);
        //        }
        //    }
        //}

        //team_a_win_prob = 1.0/(10.0^((team_b - team_a)/400.0) + 1.0)
        [TestMethod()]
        public async Task ELOTournamentRefresh()
        {
            //Arrange
            TournamentController controller = new(new TournamentDataAccess(base.Configuration));
            int tournamentCode = 22;

            //Act
            Tournament tournament = await controller.GetTournament(tournamentCode);

            //Assert
            EloRatingDataAccess daELO = new(base.Configuration);
            bool result = await daELO.UpdateTournamentELORatings(tournament.TournamentCode);

            Assert.IsTrue(result);
            //Assert.IsTrue(results[0].Draws >= 0);
            //Assert.IsTrue(results[0].ELORating >= 0);
            //Assert.IsTrue(results[0].GameCount >= 0);
            //Assert.IsTrue(results[0].Losses >= 0);
            //Assert.IsTrue(results[0].TeamCode > 0);
            //Assert.IsTrue(results[0].TeamName != "");
            //Assert.IsTrue(results[0].TournamentCode >= 0);
            //Assert.IsTrue(results[0].Wins >= 0);

            //Assert.AreEqual(2239, results[0].ELORating);
            //Assert.AreEqual(1964, results[5].ELORating);
            //Assert.AreEqual(1519, results[^1].ELORating);
        }


        [TestMethod()]
        public async Task Spain2010ELOTournamentRefreshTest()
        {
            //Arrange
            GameDataAccess da = new(base.Configuration);
            int tournamentCode = 19;
            int spainTeamCode = 29;

            //Act
            EloRatingDataAccess daELO = new(base.Configuration);
            bool result = await daELO.UpdateTournamentELORatings(tournamentCode);
            List<Game> games = await da.GetListByTournament(tournamentCode);
            games = games.OrderBy(o => o.GameTime).ToList();

            //Assert
            int i = 0;
            foreach (Game game in games)
            {
                //look for every spain game, and check the ELO rating for each game
                if (game.Team1Code == spainTeamCode || game.Team2Code == spainTeamCode)
                {
                    int? eloPreGameRating = null;
                    int? eloPostGameRating = null;
                    if (game.Team1Code == spainTeamCode)
                    {
                        eloPreGameRating = game.Team1PreGameEloRating;
                        eloPostGameRating = game.Team1PostGameEloRating;
                    }
                    else if (game.Team2Code == spainTeamCode)
                    {
                        eloPreGameRating = game.Team2PreGameEloRating;
                        eloPostGameRating = game.Team2PostGameEloRating;
                    }
                    i++;
                    switch (i)
                    {
                        case 1:
                            Assert.AreEqual(2112, eloPreGameRating);
                            Assert.AreEqual(2024, eloPostGameRating);
                            break;
                        case 2:
                            Assert.AreEqual(2024, eloPreGameRating);
                            Assert.AreEqual(2043, eloPostGameRating);
                            break;
                        case 3:
                            Assert.AreEqual(2043, eloPreGameRating);
                            Assert.AreEqual(2081, eloPostGameRating);
                            break;
                        case 4:
                            Assert.AreEqual(2081, eloPreGameRating);
                            Assert.AreEqual(2111, eloPostGameRating);
                            break;
                        case 5:
                            Assert.AreEqual(2111, eloPreGameRating);
                            Assert.AreEqual(2130, eloPostGameRating);
                            break;
                        case 6:
                            Assert.AreEqual(2130, eloPreGameRating);
                            Assert.AreEqual(2197, eloPostGameRating);
                            break;
                        case 7:
                            Assert.AreEqual(2197, eloPreGameRating);
                            Assert.AreEqual(2245, eloPostGameRating);
                            break;
                    }
                }
            }
        }
    }
}
