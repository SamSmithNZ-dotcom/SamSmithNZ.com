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
    public class PlayoffControllerTests
    {
        private IPlayoffDataAccess _mockRepo;
        private PlayoffController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockRepo = Substitute.For<IPlayoffDataAccess>();
            _controller = new PlayoffController(_mockRepo);
        }

        [TestMethod]
        public void PlayoffController_Constructor_InitializesCorrectly()
        {
            // Arrange & Act
            PlayoffController controller = new PlayoffController(_mockRepo);

            // Assert
            Assert.IsNotNull(controller);
        }

        [TestMethod]
        public async Task GetPlayoffs_ValidTournamentCode_ReturnsPlayoffList()
        {
            // Arrange
            int tournamentCode = 22;
            List<Playoff> expectedPlayoffs = new List<Playoff>
            {
                new Playoff 
                { 
                    TournamentCode = 22, 
                    RoundCode = "R16", 
                    GameNumber = 49,
                    Team1Prereq = "1A",
                    Team2Prereq = "2B",
                    SortOrder = 1
                },
                new Playoff 
                { 
                    TournamentCode = 22, 
                    RoundCode = "R16", 
                    GameNumber = 50,
                    Team1Prereq = "1C",
                    Team2Prereq = "2D",
                    SortOrder = 2
                }
            };
            _mockRepo.GetList(tournamentCode).Returns(expectedPlayoffs);

            // Act
            List<Playoff> result = await _controller.GetPlayoffs(tournamentCode);

            // Assert
            await _mockRepo.Received(1).GetList(tournamentCode);
            Assert.AreEqual(expectedPlayoffs, result);
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public async Task GetPlayoffs_EmptyList_ReturnsEmptyList()
        {
            // Arrange
            int tournamentCode = 99;
            List<Playoff> expectedPlayoffs = new List<Playoff>();
            _mockRepo.GetList(tournamentCode).Returns(expectedPlayoffs);

            // Act
            List<Playoff> result = await _controller.GetPlayoffs(tournamentCode);

            // Assert
            await _mockRepo.Received(1).GetList(tournamentCode);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public async Task GetPlayoffs_DifferentTournamentCodes_CallsRepositoryWithCorrectParameter()
        {
            // Arrange
            int tournamentCode1 = 19;
            int tournamentCode2 = 22;
            List<Playoff> playoffs1 = new List<Playoff> 
            { 
                new Playoff { TournamentCode = 19, RoundCode = "SF", GameNumber = 61 } 
            };
            List<Playoff> playoffs2 = new List<Playoff> 
            { 
                new Playoff { TournamentCode = 22, RoundCode = "SF", GameNumber = 61 } 
            };
            
            _mockRepo.GetList(tournamentCode1).Returns(playoffs1);
            _mockRepo.GetList(tournamentCode2).Returns(playoffs2);

            // Act
            List<Playoff> result1 = await _controller.GetPlayoffs(tournamentCode1);
            List<Playoff> result2 = await _controller.GetPlayoffs(tournamentCode2);

            // Assert
            await _mockRepo.Received(1).GetList(tournamentCode1);
            await _mockRepo.Received(1).GetList(tournamentCode2);
            Assert.AreEqual(19, result1[0].TournamentCode);
            Assert.AreEqual(22, result2[0].TournamentCode);
        }
    }
}
