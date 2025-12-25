using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using SamSmithNZ.Service.Controllers.WorldCup;
using SamSmithNZ.Service.DataAccess.WorldCup.Interfaces;
using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Tests.Controllers.WorldCup
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class GameControllerTests
    {
        private IGameDataAccess _mockRepo;
        private GameController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockRepo = Substitute.For<IGameDataAccess>();
            _controller = new GameController(_mockRepo);
        }

        [TestMethod]
        public void GameController_Constructor_InitializesCorrectly()
        {
            // Arrange & Act
            GameController controller = new GameController(_mockRepo);

            // Assert
            Assert.IsNotNull(controller);
        }

        [TestMethod]
        public async Task GetGames_ValidParameters_ReturnsGameList()
        {
            // Arrange
            int tournamentCode = 40;
            int roundNumber = 1;
            string roundCode = "G";
            bool includeGoals = true;
            List<Game> expectedGames = new List<Game>
            {
                new Game { GameCode = 1, TournamentCode = 40, RoundNumber = 1 },
                new Game { GameCode = 2, TournamentCode = 40, RoundNumber = 1 }
            };

            _mockRepo.GetList(tournamentCode, roundNumber, roundCode, includeGoals).Returns(expectedGames);

            // Act
            List<Game> result = await _controller.GetGames(tournamentCode, roundNumber, roundCode, includeGoals);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            await _mockRepo.Received(1).GetList(tournamentCode, roundNumber, roundCode, includeGoals);
        }

        [TestMethod]
        public async Task GetGames_NoGames_ReturnsEmptyList()
        {
            // Arrange
            int tournamentCode = 99;
            int roundNumber = 5;
            string roundCode = "F";
            bool includeGoals = false;
            List<Game> expectedGames = new List<Game>();

            _mockRepo.GetList(tournamentCode, roundNumber, roundCode, includeGoals).Returns(expectedGames);

            // Act
            List<Game> result = await _controller.GetGames(tournamentCode, roundNumber, roundCode, includeGoals);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public async Task GetGamesByTournament_ValidTournament_ReturnsGameList()
        {
            // Arrange
            int tournamentCode = 40;
            List<Game> expectedGames = new List<Game>
            {
                new Game { GameCode = 1, TournamentCode = 40 },
                new Game { GameCode = 2, TournamentCode = 40 },
                new Game { GameCode = 3, TournamentCode = 40 }
            };

            _mockRepo.GetListByTournament(tournamentCode).Returns(expectedGames);

            // Act
            List<Game> result = await _controller.GetGamesByTournament(tournamentCode);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
            await _mockRepo.Received(1).GetListByTournament(tournamentCode);
        }

        [TestMethod]
        public async Task GetGamesByTeam_ValidTeam_ReturnsGameList()
        {
            // Arrange
            int teamCode = 10;
            List<Game> expectedGames = new List<Game>
            {
                new Game { GameCode = 1, Team1Code = 10 },
                new Game { GameCode = 2, Team2Code = 10 }
            };

            _mockRepo.GetListByTeam(teamCode).Returns(expectedGames);

            // Act
            List<Game> result = await _controller.GetGamesByTeam(teamCode);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            await _mockRepo.Received(1).GetListByTeam(teamCode);
        }

        [TestMethod]
        public async Task GetPlayoffGames_ValidParameters_ReturnsGameList()
        {
            // Arrange
            int tournamentCode = 40;
            int roundNumber = 4;
            bool includeGoals = true;
            List<Game> expectedGames = new List<Game>
            {
                new Game { GameCode = 100, TournamentCode = 40, RoundNumber = 4 }
            };

            _mockRepo.GetListByPlayoff(tournamentCode, roundNumber, includeGoals).Returns(expectedGames);

            // Act
            List<Game> result = await _controller.GetPlayoffGames(tournamentCode, roundNumber, includeGoals);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            await _mockRepo.Received(1).GetListByPlayoff(tournamentCode, roundNumber, includeGoals);
        }

        [TestMethod]
        public async Task GetPlayoffGames_NoPlayoffGames_ReturnsEmptyList()
        {
            // Arrange
            int tournamentCode = 50;
            int roundNumber = 6;
            bool includeGoals = false;
            List<Game> expectedGames = new List<Game>();

            _mockRepo.GetListByPlayoff(tournamentCode, roundNumber, includeGoals).Returns(expectedGames);

            // Act
            List<Game> result = await _controller.GetPlayoffGames(tournamentCode, roundNumber, includeGoals);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public async Task GetGame_ValidGameCode_ReturnsGame()
        {
            // Arrange
            int gameCode = 123;
            Game expectedGame = new Game 
            { 
                GameCode = 123, 
                Team1Code = 10, 
                Team2Code = 29,
                TournamentCode = 40
            };

            _mockRepo.GetItem(gameCode).Returns(expectedGame);

            // Act
            Game result = await _controller.GetGame(gameCode);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(123, result.GameCode);
            Assert.AreEqual(10, result.Team1Code);
            await _mockRepo.Received(1).GetItem(gameCode);
        }

        [TestMethod]
        public async Task GetGame_InvalidGameCode_ReturnsNull()
        {
            // Arrange
            int gameCode = 999;
            Game expectedGame = null;

            _mockRepo.GetItem(gameCode).Returns(expectedGame);

            // Act
            Game result = await _controller.GetGame(gameCode);

            // Assert
            Assert.IsNull(result);
            await _mockRepo.Received(1).GetItem(gameCode);
        }
    }
}
