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
    public class TopArtistsControllerTests
    {
        private ITopArtistsDataAccess _mockRepo;
        private TopArtistsController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockRepo = Substitute.For<ITopArtistsDataAccess>();
            _controller = new TopArtistsController(_mockRepo);
        }

        [TestMethod]
        public void TopArtistsController_Constructor_InitializesCorrectly()
        {
            // Arrange & Act
            TopArtistsController controller = new TopArtistsController(_mockRepo);

            // Assert
            Assert.IsNotNull(controller);
        }

        [TestMethod]
        public async Task GetTopArtistsByPlaylist_ValidParameters_CallsDataAccessWithCorrectParameters()
        {
            // Arrange
            int playlistCode = 1;
            bool showJustSummary = true;
            List<TopArtists> expectedResult = new List<TopArtists>
            {
                new TopArtists { ArtistName = "Test Artist", ArtistCount = 10 }
            };
            _mockRepo.GetList(playlistCode, showJustSummary).Returns(expectedResult);

            // Act
            List<TopArtists> result = await _controller.GetTopArtistsByPlaylist(playlistCode, showJustSummary);

            // Assert
            await _mockRepo.Received(1).GetList(playlistCode, showJustSummary);
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public async Task GetTopArtistsByPlaylist_DifferentParameters_CallsDataAccessWithCorrectParameters()
        {
            // Arrange
            int playlistCode = 5;
            bool showJustSummary = false;
            List<TopArtists> expectedResult = new List<TopArtists>
            {
                new TopArtists { ArtistName = "Another Artist", ArtistCount = 25 }
            };
            _mockRepo.GetList(playlistCode, showJustSummary).Returns(expectedResult);

            // Act
            List<TopArtists> result = await _controller.GetTopArtistsByPlaylist(playlistCode, showJustSummary);

            // Assert
            await _mockRepo.Received(1).GetList(playlistCode, showJustSummary);
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public async Task GetTopArtistsSummary_ValidParameters_CallsDataAccessWithCorrectParameters()
        {
            // Arrange
            bool showJustSummary = true;
            List<TopArtists> expectedResult = new List<TopArtists>
            {
                new TopArtists { ArtistName = "Summary Artist", ArtistCount = 100 }
            };
            _mockRepo.GetList(showJustSummary).Returns(expectedResult);

            // Act
            List<TopArtists> result = await _controller.GetTopArtistsSummary(showJustSummary);

            // Assert
            await _mockRepo.Received(1).GetList(showJustSummary);
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public async Task GetTopArtistsSummary_FalseParameter_CallsDataAccessWithCorrectParameters()
        {
            // Arrange
            bool showJustSummary = false;
            List<TopArtists> expectedResult = new List<TopArtists>
            {
                new TopArtists { ArtistName = "Full Artist", ArtistCount = 200 }
            };
            _mockRepo.GetList(showJustSummary).Returns(expectedResult);

            // Act
            List<TopArtists> result = await _controller.GetTopArtistsSummary(showJustSummary);

            // Assert
            await _mockRepo.Received(1).GetList(showJustSummary);
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public async Task GetTopArtistsByPlaylist_EmptyResult_ReturnsEmptyList()
        {
            // Arrange
            int playlistCode = 1;
            bool showJustSummary = true;
            List<TopArtists> expectedResult = new List<TopArtists>();
            _mockRepo.GetList(playlistCode, showJustSummary).Returns(expectedResult);

            // Act
            List<TopArtists> result = await _controller.GetTopArtistsByPlaylist(playlistCode, showJustSummary);

            // Assert
            await _mockRepo.Received(1).GetList(playlistCode, showJustSummary);
            Assert.AreEqual(expectedResult, result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public async Task GetTopArtistsSummary_EmptyResult_ReturnsEmptyList()
        {
            // Arrange
            bool showJustSummary = true;
            List<TopArtists> expectedResult = new List<TopArtists>();
            _mockRepo.GetList(showJustSummary).Returns(expectedResult);

            // Act
            List<TopArtists> result = await _controller.GetTopArtistsSummary(showJustSummary);

            // Assert
            await _mockRepo.Received(1).GetList(showJustSummary);
            Assert.AreEqual(expectedResult, result);
            Assert.AreEqual(0, result.Count);
        }
    }
}