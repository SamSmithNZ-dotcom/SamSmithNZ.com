using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using SamSmithNZ.Service.Controllers.GuitarTab;
using SamSmithNZ.Service.DataAccess.GuitarTab.Interfaces;
using SamSmithNZ.Service.Models.GuitarTab;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Tests.Controllers.GuitarTab
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class ArtistControllerTests
    {
        private IArtistDataAccess _mockRepo;
        private ArtistController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockRepo = Substitute.For<IArtistDataAccess>();
            _controller = new ArtistController(_mockRepo);
        }

        [TestMethod]
        public void ArtistController_Constructor_InitializesCorrectly()
        {
            // Arrange & Act
            ArtistController controller = new ArtistController(_mockRepo);

            // Assert
            Assert.IsNotNull(controller);
        }

        [TestMethod]
        public async Task GetArtists_DefaultParameters_CallsDataAccessWithCorrectParameters()
        {
            // Arrange
            bool includeAllItems = false;
            bool isAdmin = false;
            List<Artist> expectedResult = new List<Artist>
            {
                new Artist { ArtistName = "Test Artist", ArtistNameTrimed = "TestArtist" }
            };
            _mockRepo.GetList(includeAllItems, isAdmin).Returns(expectedResult);

            // Act
            List<Artist> result = await _controller.GetArtists();

            // Assert
            await _mockRepo.Received(1).GetList(includeAllItems, isAdmin);
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public async Task GetArtists_IncludeAllItemsTrue_CallsDataAccessWithCorrectParameters()
        {
            // Arrange
            bool includeAllItems = true;
            bool isAdmin = false;
            List<Artist> expectedResult = new List<Artist>
            {
                new Artist { ArtistName = "All Items Artist", ArtistNameTrimed = "AllItemsArtist" }
            };
            _mockRepo.GetList(includeAllItems, isAdmin).Returns(expectedResult);

            // Act
            var result = await _controller.GetArtists(includeAllItems);

            // Assert
            await _mockRepo.Received(1).GetList(includeAllItems, isAdmin);
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public async Task GetArtists_IsAdminTrue_CallsDataAccessWithCorrectParameters()
        {
            // Arrange
            bool includeAllItems = false;
            bool isAdmin = true;
            List<Artist> expectedResult = new List<Artist>
            {
                new Artist { ArtistName = "Admin Artist", ArtistNameTrimed = "AdminArtist" }
            };
            _mockRepo.GetList(includeAllItems, isAdmin).Returns(expectedResult);

            // Act
            var result = await _controller.GetArtists(includeAllItems, isAdmin);

            // Assert
            await _mockRepo.Received(1).GetList(includeAllItems, isAdmin);
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public async Task GetArtists_BothParametersTrue_CallsDataAccessWithCorrectParameters()
        {
            // Arrange
            bool includeAllItems = true;
            bool isAdmin = true;
            List<Artist> expectedResult = new List<Artist>
            {
                new Artist { ArtistName = "Full Access Artist", ArtistNameTrimed = "FullAccessArtist" }
            };
            _mockRepo.GetList(includeAllItems, isAdmin).Returns(expectedResult);

            // Act
            var result = await _controller.GetArtists(includeAllItems, isAdmin);

            // Assert
            await _mockRepo.Received(1).GetList(includeAllItems, isAdmin);
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public async Task GetArtists_EmptyResult_ReturnsEmptyList()
        {
            // Arrange
            bool includeAllItems = false;
            bool isAdmin = false;
            List<Artist> expectedResult = new List<Artist>();
            _mockRepo.GetList(includeAllItems, isAdmin).Returns(expectedResult);

            // Act
            List<Artist> result = await _controller.GetArtists();

            // Assert
            await _mockRepo.Received(1).GetList(includeAllItems, isAdmin);
            Assert.AreEqual(expectedResult, result);
            Assert.AreEqual(0, result.Count);
        }
    }
}