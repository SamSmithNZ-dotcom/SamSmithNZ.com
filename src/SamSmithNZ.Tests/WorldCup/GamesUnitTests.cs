using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using SamSmithNZ.Service.Controllers.WorldCup;
using SamSmithNZ.Service.DataAccess.WorldCup.Interfaces;
using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Tests.WorldCup
{
    [TestClass]
    [TestCategory("L0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class GamesUnitTests
    {
        [TestMethod]
        public async Task GamesUnitTest()
        {
            //arrange
            IGameDataAccess mock = Substitute.For<IGameDataAccess>();
            mock.GetList(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<string>(), Arg.Any<bool>()).Returns(Task.FromResult(GetGamesTestData()));
            GameController controller = new(mock);
            int tournamentCode = 19;
            int roundNumber = 1;
            string roundCode = "F";
            bool includeGoals = false;

            //act
            List<Game> results = await controller.GetGames(tournamentCode, roundNumber, roundCode, includeGoals);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsNotEmpty(results);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(14, results[0].TournamentCode);
        }

        [TestMethod]
        public void GameUnitTest()
        {
            //arrange
            Game game = new()
            {
                TournamentCode = 1,
                TournamentName = "MyTournament",
                RoundNumber = 1,
                RoundCode = "A",
                RoundName = "Group A",
                GameNumber = 1,
                GameCode = 1,
                GameTime = new(2021, 7, 9, 8, 52, 0),
                Location = "Boston, MA",
                RowType = 1,
                CoachFlag = "ABC",
                CoachName = "DEF",
                Team1Code = 10,
                Team1Name = "England",
                Team1FlagName = "Union Jack",
                Team1NormalTimeScore = 3,
                Team1ExtraTimeScore = null,
                Team1PenaltiesScore = null,
                Team1EloRating = 1050,
                Team1PreGameEloRating = 1000,
                Team1PostGameEloRating = 1100,
                Team1ResultWonGame = true,
                Team1Withdrew = false,
                Team2Code = 12,
                Team2Name = "France",
                Team2FlagName = "FrenchFlag",
                Team2NormalTimeScore = 1,
                Team2ExtraTimeScore = null,
                Team2PenaltiesScore = null,
                Team2EloRating = 950,
                Team2PreGameEloRating = 1000,
                Team2PostGameEloRating = 900,
                Team2ResultWonGame = true,
                Team2Withdrew = false,
                IsOwnGoal = false,
                IsPenalty = false,
                IsGoldenGoal = false,
                ShowPenaltyShootOutLabel = false
            };

            //act

            //assert
            Assert.IsTrue(game != null);
        }


        [TestMethod]
        public void GameTimeUnitTest()
        {
            //arrange
            Game gameWithTime = new();
            gameWithTime.GameTime = new(2021, 10, 14, 10, 24, 10);
            Game gameNoTime = new();
            gameNoTime.GameTime = new(2021, 10, 14, 0, 0, 0);

            //act
            string gameWithTimeString = gameWithTime.GameTimeString();
            string gameNoTimeString = gameNoTime.GameTimeString();

            //assert
            Assert.AreEqual("14-Oct-2021 10:24", gameWithTimeString);
            Assert.AreEqual("14-Oct-2021", gameNoTimeString);
        }

        private static List<Game> GetGamesTestData()
        {
            return new List<Game>() {
            new Game{
                TournamentCode = 14,
                TournamentName = "Test tournament 14"
            } };
        }


        [TestMethod]
        public void GamesTeam2IsWayBetterELOTest()
        {
            //arrange
            Game game = new();
            game.Team1Code = 1;
            game.Team2Code = 2;
            game.Team1EloRating = 1000;
            game.Team1PreGameEloRating = 1000;
            game.Team1PostGameEloRating = 1000;
            game.Team2EloRating = 2000;
            game.Team2PreGameEloRating = 2000;
            game.Team2PostGameEloRating = 2000;

            //act

            //assert
            Assert.IsTrue(game.Team1ChanceToWin < game.Team2ChanceToWin);
        }

        [TestMethod]
        public void GamesTeamsAreEqualELOTest()
        {
            //arrange
            Game game = new();
            game.Team1Code = 1;
            game.Team2Code = 2;
            game.Team1EloRating = 2000;
            game.Team1PreGameEloRating = 2000;
            game.Team1PostGameEloRating = 2000;
            game.Team2EloRating = 2000;
            game.Team2PreGameEloRating = 2000;
            game.Team2PostGameEloRating = 2000;

            //act

            //assert
            Assert.IsTrue(game.Team1ChanceToWin == game.Team2ChanceToWin);
        }

        [TestMethod]
        public void GamesTeamsAreEqualELONoTeamsTest()
        {
            //arrange
            Game game = new();
            game.Team1EloRating = 2000;
            game.Team1PreGameEloRating = 2000;
            game.Team1PostGameEloRating = 2000;
            game.Team2EloRating = 2000;
            game.Team2PreGameEloRating = 2000;
            game.Team2PostGameEloRating = 2000;
            game.GameCanEndInADraw = true;

            //act

            //assert
            Assert.AreEqual(game.Team1ChanceToWin, game.Team2ChanceToWin);
            Assert.AreEqual(43.48, game.Team1ChanceToWin);
            Assert.AreEqual(43.48, game.Team2ChanceToWin);
            Assert.AreEqual(13.04, game.TeamChanceToDraw);
        }

        [TestMethod]
        public void Team1TotalGoalsTest()
        {
            //arrange
            Game game = new();
            game.Team1NormalTimeScore = 1;
            game.Team1ExtraTimeScore = 2;

            //act

            //assert
            Assert.IsTrue(game.Team1TotalGoals == 3);
        }

        [TestMethod]
        public void Team2TotalGoalsTest()
        {
            //arrange
            Game game = new();
            game.Team2NormalTimeScore = 1;
            game.Team2ExtraTimeScore = 2;

            //act

            //assert
            Assert.IsTrue(game.Team2TotalGoals == 3);
        }

    }
}
