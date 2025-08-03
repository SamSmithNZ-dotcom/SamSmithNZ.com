using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using SamSmithNZ.Service.Controllers.ITunes;
using SamSmithNZ.Service.DataAccess.ITunes.Interfaces;
using SamSmithNZ.Service.Models.ITunes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Tests.Controllers.ITunes
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class MovementControllerTests
    {
        private IMovementDataAccess _mockRepo;
        private MovementController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockRepo = Substitute.For<IMovementDataAccess>();
            _controller = new MovementController(_mockRepo);
        }

        [TestMethod]
        public void MovementController_Constructor_InitializesCorrectly()
        {
            // Arrange & Act
            MovementController controller = new MovementController(_mockRepo);

            // Assert
            Assert.IsNotNull(controller);
        }

        [TestMethod]
        public async Task GetMovementsByPlaylist_ValidParameters_CallsDataAccessWithCorrectParameters()
        {
            // Arrange
            int playlistCode = 1;
            bool showJustSummary = true;
            List<Movement> expectedResult = new List<Movement>
            {
                new Movement { TrackName = "Test Movement", PlayCount = 10 }
            };
            _mockRepo.GetList(playlistCode, showJustSummary).Returns(expectedResult);

            // Act
            List<Movement> result = await _controller.GetMovementsByPlaylist(playlistCode, showJustSummary);

            // Assert
            await _mockRepo.Received(1).GetList(playlistCode, showJustSummary);
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public async Task GetMovementsByPlaylist_DifferentParameters_CallsDataAccessWithCorrectParameters()
        {
            // Arrange
            int playlistCode = 5;
            bool showJustSummary = false;
            List<Movement> expectedResult = new List<Movement>
            {
                new Movement { TrackName = "Another Movement", PlayCount = 20 }
            };
            _mockRepo.GetList(playlistCode, showJustSummary).Returns(expectedResult);

            // Act
            List<Movement> result = await _controller.GetMovementsByPlaylist(playlistCode, showJustSummary);

            // Assert
            await _mockRepo.Received(1).GetList(playlistCode, showJustSummary);
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public async Task GetMovementsSummary_ValidParameters_CallsDataAccessWithCorrectParameters()
        {
            // Arrange
            bool showJustSummary = true;
            List<Movement> expectedResult = new List<Movement>
            {
                new Movement { TrackName = "Summary Movement", PlayCount = 5 }
            };
            _mockRepo.GetList(showJustSummary).Returns(expectedResult);

            // Act
            List<Movement> result = await _controller.GetMovementsSummary(showJustSummary);

            // Assert
            await _mockRepo.Received(1).GetList(showJustSummary);
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public async Task GetMovementsSummary_FalseParameter_CallsDataAccessWithCorrectParameters()
        {
            // Arrange
            bool showJustSummary = false;
            List<Movement> expectedResult = new List<Movement>
            {
                new Movement { TrackName = "Full Movement", PlayCount = 15 }
            };
            _mockRepo.GetList(showJustSummary).Returns(expectedResult);

            // Act
            List<Movement> result = await _controller.GetMovementsSummary(showJustSummary);

            // Assert
            await _mockRepo.Received(1).GetList(showJustSummary);
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public async Task GetMovementsByPlaylist_EmptyResult_ReturnsEmptyList()
        {
            // Arrange
            int playlistCode = 1;
            bool showJustSummary = true;
            List<Movement> expectedResult = new List<Movement>();
            _mockRepo.GetList(playlistCode, showJustSummary).Returns(expectedResult);

            // Act
            List<Movement> result = await _controller.GetMovementsByPlaylist(playlistCode, showJustSummary);

            // Assert
            await _mockRepo.Received(1).GetList(playlistCode, showJustSummary);
            Assert.AreEqual(expectedResult, result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public async Task GetMovementsSummary_EmptyResult_ReturnsEmptyList()
        {
            // Arrange
            bool showJustSummary = true;
            List<Movement> expectedResult = new List<Movement>();
            _mockRepo.GetList(showJustSummary).Returns(expectedResult);

            // Act
            List<Movement> result = await _controller.GetMovementsSummary(showJustSummary);

            // Assert
            await _mockRepo.Received(1).GetList(showJustSummary);
            Assert.AreEqual(expectedResult, result);
            Assert.AreEqual(0, result.Count);
        }
    }
}