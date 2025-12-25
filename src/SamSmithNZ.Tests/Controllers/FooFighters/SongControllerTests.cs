using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using SamSmithNZ.Service.Controllers.FooFighters;
using SamSmithNZ.Service.DataAccess.FooFighters.Interfaces;
using SamSmithNZ.Service.Models.FooFighters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Tests.Controllers.FooFighters
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class SongControllerTests
    {
        private ISongDataAccess _mockRepo;
        private SongController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockRepo = Substitute.For<ISongDataAccess>();
            _controller = new SongController(_mockRepo);
        }

        [TestMethod]
        public void SongController_Constructor_InitializesCorrectly()
        {
            // Arrange & Act
            SongController controller = new SongController(_mockRepo);

            // Assert
            Assert.IsNotNull(controller);
        }

        [TestMethod]
        public async Task GetSongs_ReturnsAllSongs()
        {
            // Arrange
            List<Song> expectedSongs = new List<Song>
            {
                new Song { SongCode = 1, SongName = "Everlong" },
                new Song { SongCode = 2, SongName = "My Hero" },
                new Song { SongCode = 3, SongName = "The Pretender" }
            };

            _mockRepo.GetList().Returns(expectedSongs);

            // Act
            List<Song> result = await _controller.GetSongs();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
            await _mockRepo.Received(1).GetList();
        }

        [TestMethod]
        public async Task GetSongs_NoSongs_ReturnsEmptyList()
        {
            // Arrange
            List<Song> expectedSongs = new List<Song>();

            _mockRepo.GetList().Returns(expectedSongs);

            // Act
            List<Song> result = await _controller.GetSongs();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public async Task GetSongsByAlbum_ValidAlbumCode_ReturnsSongs()
        {
            // Arrange
            int albumCode = 5;
            List<Song> expectedSongs = new List<Song>
            {
                new Song { SongCode = 10, SongName = "Everlong", AlbumCode = 5 },
                new Song { SongCode = 11, SongName = "My Hero", AlbumCode = 5 }
            };

            _mockRepo.GetListForAlbumAsync(albumCode).Returns(expectedSongs);

            // Act
            List<Song> result = await _controller.GetSongsByAlbum(albumCode);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            await _mockRepo.Received(1).GetListForAlbumAsync(albumCode);
        }

        [TestMethod]
        public async Task GetSongsByAlbum_NoSongs_ReturnsEmptyList()
        {
            // Arrange
            int albumCode = 99;
            List<Song> expectedSongs = new List<Song>();

            _mockRepo.GetListForAlbumAsync(albumCode).Returns(expectedSongs);

            // Act
            List<Song> result = await _controller.GetSongsByAlbum(albumCode);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public async Task GetSongsByShow_ValidShowCode_ReturnsSongs()
        {
            // Arrange
            int showCode = 100;
            List<Song> expectedSongs = new List<Song>
            {
                new Song { SongCode = 1, SongName = "Times Like These" },
                new Song { SongCode = 2, SongName = "Best of You" },
                new Song { SongCode = 3, SongName = "Learn to Fly" }
            };

            _mockRepo.GetListForShowAsync(showCode).Returns(expectedSongs);

            // Act
            List<Song> result = await _controller.GetSongsByShow(showCode);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
            await _mockRepo.Received(1).GetListForShowAsync(showCode);
        }

        [TestMethod]
        public async Task GetSongsByShow_NoSongs_ReturnsEmptyList()
        {
            // Arrange
            int showCode = 999;
            List<Song> expectedSongs = new List<Song>();

            _mockRepo.GetListForShowAsync(showCode).Returns(expectedSongs);

            // Act
            List<Song> result = await _controller.GetSongsByShow(showCode);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public async Task GetSong_ValidSongCode_ReturnsSong()
        {
            // Arrange
            int songCode = 42;
            Song expectedSong = new Song 
            { 
                SongCode = 42, 
                SongName = "Everlong",
                AlbumCode = 5
            };

            _mockRepo.GetItem(songCode).Returns(expectedSong);

            // Act
            Song result = await _controller.GetSong(songCode);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(42, result.SongCode);
            Assert.AreEqual("Everlong", result.SongName);
            await _mockRepo.Received(1).GetItem(songCode);
        }

        [TestMethod]
        public async Task GetSong_InvalidSongCode_ReturnsNull()
        {
            // Arrange
            int songCode = 999;
            Song expectedSong = null;

            _mockRepo.GetItem(songCode).Returns(expectedSong);

            // Act
            Song result = await _controller.GetSong(songCode);

            // Assert
            Assert.IsNull(result);
            await _mockRepo.Received(1).GetItem(songCode);
        }
    }
}
