using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using SamSmithNZ.Service.Controllers.ITunes;
using SamSmithNZ.Service.DataAccess.ITunes.Interfaces;
using SamSmithNZ.Service.Models.ITunes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Tests.Controllers.ITunes
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class PlaylistControllerTests
    {
        private IPlaylistDataAccess _mockRepo;
        private PlaylistController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockRepo = Substitute.For<IPlaylistDataAccess>();
            _controller = new PlaylistController(_mockRepo);
        }

        [TestMethod]
        public void PlaylistController_Constructor_InitializesCorrectly()
        {
            // Arrange & Act
            var controller = new PlaylistController(_mockRepo);

            // Assert
            Assert.IsNotNull(controller);
        }

        [TestMethod]
        public async Task GetPlaylists_CallsDataAccess_ReturnsPlaylistList()
        {
            // Arrange
            var expectedResult = new List<Playlist>
            {
                new Playlist { PlaylistCode = 1, PlaylistDate = new DateTime(2023, 1, 1) },
                new Playlist { PlaylistCode = 2, PlaylistDate = new DateTime(2023, 2, 1) }
            };
            _mockRepo.GetList().Returns(expectedResult);

            // Act
            var result = await _controller.GetPlaylists();

            // Assert
            await _mockRepo.Received(1).GetList();
            Assert.AreEqual(expectedResult, result);
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public async Task GetPlaylists_EmptyResult_ReturnsEmptyList()
        {
            // Arrange
            var expectedResult = new List<Playlist>();
            _mockRepo.GetList().Returns(expectedResult);

            // Act
            var result = await _controller.GetPlaylists();

            // Assert
            await _mockRepo.Received(1).GetList();
            Assert.AreEqual(expectedResult, result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public async Task GetPlaylist_ValidPlaylistCode_CallsDataAccessWithCorrectParameter()
        {
            // Arrange
            int playlistCode = 123;
            var expectedResult = new Playlist { PlaylistCode = playlistCode, PlaylistDate = new DateTime(2023, 5, 15) };
            _mockRepo.GetItem(playlistCode).Returns(expectedResult);

            // Act
            var result = await _controller.GetPlaylist(playlistCode);

            // Assert
            await _mockRepo.Received(1).GetItem(playlistCode);
            Assert.AreEqual(expectedResult, result);
            Assert.AreEqual(playlistCode, result.PlaylistCode);
        }

        [TestMethod]
        public async Task GetPlaylist_DifferentPlaylistCode_CallsDataAccessWithCorrectParameter()
        {
            // Arrange
            int playlistCode = 456;
            var expectedResult = new Playlist { PlaylistCode = playlistCode, PlaylistDate = new DateTime(2023, 10, 20) };
            _mockRepo.GetItem(playlistCode).Returns(expectedResult);

            // Act
            var result = await _controller.GetPlaylist(playlistCode);

            // Assert
            await _mockRepo.Received(1).GetItem(playlistCode);
            Assert.AreEqual(expectedResult, result);
            Assert.AreEqual(playlistCode, result.PlaylistCode);
        }

        [TestMethod]
        public async Task GetPlaylist_NullResult_ReturnsNull()
        {
            // Arrange
            int playlistCode = 999;
            Playlist expectedResult = null;
            _mockRepo.GetItem(playlistCode).Returns(expectedResult);

            // Act
            var result = await _controller.GetPlaylist(playlistCode);

            // Assert
            await _mockRepo.Received(1).GetItem(playlistCode);
            Assert.IsNull(result);
        }
    }
}