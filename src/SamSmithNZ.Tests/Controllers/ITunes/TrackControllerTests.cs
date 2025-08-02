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
    public class TrackControllerTests
    {
        private ITrackDataAccess _mockRepo;
        private TrackController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockRepo = Substitute.For<ITrackDataAccess>();
            _controller = new TrackController(_mockRepo);
        }

        [TestMethod]
        public void TrackController_Constructor_InitializesCorrectly()
        {
            // Arrange & Act
            var controller = new TrackController(_mockRepo);

            // Assert
            Assert.IsNotNull(controller);
        }

        [TestMethod]
        public async Task GetTracks_ValidParameters_CallsDataAccessWithCorrectParameters()
        {
            // Arrange
            int playlistCode = 1;
            bool showJustSummary = true;
            var expectedResult = new List<Track>
            {
                new Track 
                { 
                    PlaylistCode = playlistCode, 
                    TrackName = "Test Track", 
                    ArtistName = "Test Artist",
                    AlbumName = "Test Album",
                    PlayCount = 10
                }
            };
            _mockRepo.GetList(playlistCode, showJustSummary).Returns(expectedResult);

            // Act
            var result = await _controller.GetTracks(playlistCode, showJustSummary);

            // Assert
            await _mockRepo.Received(1).GetList(playlistCode, showJustSummary);
            Assert.AreEqual(expectedResult, result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Test Track", result[0].TrackName);
        }

        [TestMethod]
        public async Task GetTracks_DifferentParameters_CallsDataAccessWithCorrectParameters()
        {
            // Arrange
            int playlistCode = 5;
            bool showJustSummary = false;
            var expectedResult = new List<Track>
            {
                new Track 
                { 
                    PlaylistCode = playlistCode, 
                    TrackName = "Another Track", 
                    ArtistName = "Another Artist",
                    AlbumName = "Another Album",
                    PlayCount = 25
                }
            };
            _mockRepo.GetList(playlistCode, showJustSummary).Returns(expectedResult);

            // Act
            var result = await _controller.GetTracks(playlistCode, showJustSummary);

            // Assert
            await _mockRepo.Received(1).GetList(playlistCode, showJustSummary);
            Assert.AreEqual(expectedResult, result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Another Track", result[0].TrackName);
        }

        [TestMethod]
        public async Task GetTracks_EmptyResult_ReturnsEmptyList()
        {
            // Arrange
            int playlistCode = 1;
            bool showJustSummary = true;
            var expectedResult = new List<Track>();
            _mockRepo.GetList(playlistCode, showJustSummary).Returns(expectedResult);

            // Act
            var result = await _controller.GetTracks(playlistCode, showJustSummary);

            // Assert
            await _mockRepo.Received(1).GetList(playlistCode, showJustSummary);
            Assert.AreEqual(expectedResult, result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public async Task GetTracks_MultipleTracksResult_ReturnsAllTracks()
        {
            // Arrange
            int playlistCode = 2;
            bool showJustSummary = false;
            var expectedResult = new List<Track>
            {
                new Track 
                { 
                    PlaylistCode = playlistCode, 
                    TrackName = "Track 1", 
                    ArtistName = "Artist 1",
                    PlayCount = 15
                },
                new Track 
                { 
                    PlaylistCode = playlistCode, 
                    TrackName = "Track 2", 
                    ArtistName = "Artist 2",
                    PlayCount = 30
                }
            };
            _mockRepo.GetList(playlistCode, showJustSummary).Returns(expectedResult);

            // Act
            var result = await _controller.GetTracks(playlistCode, showJustSummary);

            // Assert
            await _mockRepo.Received(1).GetList(playlistCode, showJustSummary);
            Assert.AreEqual(expectedResult, result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Track 1", result[0].TrackName);
            Assert.AreEqual("Track 2", result[1].TrackName);
        }
    }
}