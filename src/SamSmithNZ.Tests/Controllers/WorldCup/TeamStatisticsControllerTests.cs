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
    public class TeamStatisticsControllerTests
    {
        private ITeamDataAccess _mockTeamRepo;
        private IGameDataAccess _mockGameRepo;
        private TeamStatisticsController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockTeamRepo = Substitute.For<ITeamDataAccess>();
            _mockGameRepo = Substitute.For<IGameDataAccess>();
            _controller = new TeamStatisticsController(_mockTeamRepo, _mockGameRepo);
        }

        [TestMethod]
        public void TeamStatisticsController_Constructor_InitializesCorrectly()
        {
            // Arrange & Act
            TeamStatisticsController controller = new TeamStatisticsController(_mockTeamRepo, _mockGameRepo);

            // Assert
            Assert.IsNotNull(controller);
        }

        [TestMethod]
        public async Task GetTeamStatistics_ValidTeamCode_ReturnsTeamStatisticsWithData()
        {
            // Arrange
            int teamCode = 10;
            Team team = new Team { TeamCode = 10, TeamName = "Brazil" };
            List<Game> games = new List<Game>
            {
                new Game { GameCode = 1, Team1Code = 10, Team2Code = 29 },
                new Game { GameCode = 2, Team1Code = 5, Team2Code = 10 }
            };

            _mockTeamRepo.GetItem(teamCode).Returns(team);
            _mockGameRepo.GetListByTeam(teamCode).Returns(games);

            // Act
            TeamStatistics result = await _controller.GetTeamStatistics(teamCode);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Team);
            Assert.AreEqual("Brazil", result.Team.TeamName);
            Assert.AreEqual(2, result.Games.Count);
            await _mockTeamRepo.Received(1).GetItem(teamCode);
            await _mockGameRepo.Received(1).GetListByTeam(teamCode);
        }

        [TestMethod]
        public async Task GetTeamStatistics_NoGames_ReturnsEmptyGamesList()
        {
            // Arrange
            int teamCode = 99;
            Team team = new Team { TeamCode = 99, TeamName = "New Team" };
            List<Game> games = new List<Game>();

            _mockTeamRepo.GetItem(teamCode).Returns(team);
            _mockGameRepo.GetListByTeam(teamCode).Returns(games);

            // Act
            TeamStatistics result = await _controller.GetTeamStatistics(teamCode);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Games.Count);
        }

        [TestMethod]
        public async Task GetTeamMatchup_ValidTeamCodes_ReturnsMatchupWithBothTeams()
        {
            // Arrange
            int team1Code = 10;
            int team2Code = 29;
            
            Team team1 = new Team { TeamCode = 10, TeamName = "Brazil" };
            Team team2 = new Team { TeamCode = 29, TeamName = "Spain" };
            
            List<Game> team1Games = new List<Game>
            {
                new Game { GameCode = 1, Team1Code = 10, Team2Code = 29 },
                new Game { GameCode = 2, Team1Code = 10, Team2Code = 5 }
            };
            
            List<Game> team2Games = new List<Game>
            {
                new Game { GameCode = 1, Team1Code = 10, Team2Code = 29 },
                new Game { GameCode = 3, Team1Code = 29, Team2Code = 8 }
            };

            _mockTeamRepo.GetItem(team1Code).Returns(team1);
            _mockTeamRepo.GetItem(team2Code).Returns(team2);
            _mockGameRepo.GetListByTeam(team1Code).Returns(team1Games);
            _mockGameRepo.GetListByTeam(team2Code).Returns(team2Games);

            // Act
            TeamMatchup result = await _controller.GetTeamMatchup(team1Code, team2Code);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Team1Statistics);
            Assert.IsNotNull(result.Team2Statistics);
            Assert.AreEqual("Brazil", result.Team1Statistics.Team.TeamName);
            Assert.AreEqual("Spain", result.Team2Statistics.Team.TeamName);
            Assert.AreEqual(2, result.Team1Statistics.Games.Count);
            Assert.AreEqual(2, result.Team2Statistics.Games.Count);
            Assert.AreEqual(1, result.Games.Count); // Only GameCode 1 has both teams
            
            await _mockTeamRepo.Received(1).GetItem(team1Code);
            await _mockTeamRepo.Received(1).GetItem(team2Code);
            await _mockGameRepo.Received(1).GetListByTeam(team1Code);
            await _mockGameRepo.Received(1).GetListByTeam(team2Code);
        }

        [TestMethod]
        public async Task GetTeamMatchup_TeamsNeverPlayed_ReturnsMatchupWithAllGames()
        {
            // Arrange
            int team1Code = 10;
            int team2Code = 50;
            
            Team team1 = new Team { TeamCode = 10, TeamName = "Brazil" };
            Team team2 = new Team { TeamCode = 50, TeamName = "Iceland" };
            
            List<Game> team1Games = new List<Game>
            {
                new Game { GameCode = 1, Team1Code = 10, Team2Code = 29 }
            };
            
            List<Game> team2Games = new List<Game>
            {
                new Game { GameCode = 2, Team1Code = 50, Team2Code = 8 }
            };

            _mockTeamRepo.GetItem(team1Code).Returns(team1);
            _mockTeamRepo.GetItem(team2Code).Returns(team2);
            _mockGameRepo.GetListByTeam(team1Code).Returns(team1Games);
            _mockGameRepo.GetListByTeam(team2Code).Returns(team2Games);

            // Act
            TeamMatchup result = await _controller.GetTeamMatchup(team1Code, team2Code);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Games.Count); // Teams never played each other
        }
    }
}
