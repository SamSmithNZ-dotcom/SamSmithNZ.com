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
    public class AlbumControllerTests
    {
        private IAlbumDataAccess _mockRepo;
        private AlbumController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockRepo = Substitute.For<IAlbumDataAccess>();
            _controller = new AlbumController(_mockRepo);
        }

        [TestMethod]
        public void AlbumController_Constructor_InitializesCorrectly()
        {
            // Arrange & Act
            AlbumController controller = new AlbumController(_mockRepo);

            // Assert
            Assert.IsNotNull(controller);
        }

        [TestMethod]
        public async Task GetAlbums_DefaultParameters_CallsDataAccessWithCorrectParameters()
        {
            // Arrange
            bool isAdmin = false;
            List<Album> expectedResult = new List<Album>
            {
                new Album { AlbumName = "Test Album", ArtistName = "Test Artist" }
            };
            _mockRepo.GetList(isAdmin).Returns(expectedResult);

            // Act
            List<Album> result = await _controller.GetAlbums();

            // Assert
            await _mockRepo.Received(1).GetList(isAdmin);
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public async Task GetAlbums_IsAdminTrue_CallsDataAccessWithCorrectParameters()
        {
            // Arrange
            bool isAdmin = true;
            List<Album> expectedResult = new List<Album>
            {
                new Album { AlbumName = "Admin Album", ArtistName = "Admin Artist" }
            };
            _mockRepo.GetList(isAdmin).Returns(expectedResult);

            // Act
            List<Album> result = await _controller.GetAlbums(isAdmin);

            // Assert
            await _mockRepo.Received(1).GetList(isAdmin);
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public async Task GetAlbums_EmptyResult_ReturnsEmptyList()
        {
            // Arrange
            bool isAdmin = false;
            List<Album> expectedResult = new List<Album>();
            _mockRepo.GetList(isAdmin).Returns(expectedResult);

            // Act
            List<Album> result = await _controller.GetAlbums();

            // Assert
            await _mockRepo.Received(1).GetList(isAdmin);
            Assert.AreEqual(expectedResult, result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public async Task GetAlbum_ValidParameters_CallsDataAccessWithCorrectParameters()
        {
            // Arrange
            int albumCode = 1;
            bool isAdmin = false;
            Album expectedResult = new Album { AlbumCode = albumCode, AlbumName = "Test Album" };
            _mockRepo.GetItem(albumCode, isAdmin).Returns(expectedResult);

            // Act
            Album result = await _controller.GetAlbum(albumCode);

            // Assert
            await _mockRepo.Received(1).GetItem(albumCode, isAdmin);
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public async Task GetAlbum_IsAdminTrue_CallsDataAccessWithCorrectParameters()
        {
            // Arrange
            int albumCode = 2;
            bool isAdmin = true;
            Album expectedResult = new Album { AlbumCode = albumCode, AlbumName = "Admin Album" };
            _mockRepo.GetItem(albumCode, isAdmin).Returns(expectedResult);

            // Act
            Album result = await _controller.GetAlbum(albumCode, isAdmin);

            // Assert
            await _mockRepo.Received(1).GetItem(albumCode, isAdmin);
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public async Task SaveAlbum_ValidAlbum_CallsDataAccessWithCorrectParameters()
        {
            // Arrange
            Album inputAlbum = new Album { AlbumName = "New Album", ArtistName = "New Artist" };
            Album expectedResult = new Album { AlbumCode = 1, AlbumName = "New Album", ArtistName = "New Artist" };
            _mockRepo.SaveItem(inputAlbum).Returns(expectedResult);

            // Act
            Album result = await _controller.SaveAlbum(inputAlbum);

            // Assert
            await _mockRepo.Received(1).SaveItem(inputAlbum);
            Assert.AreEqual(expectedResult, result);
        }
    }
}