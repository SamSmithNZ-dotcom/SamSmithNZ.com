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
    public class ShowControllerTests
    {
        private IShowDataAccess _mockRepo;
        private ShowController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockRepo = Substitute.For<IShowDataAccess>();
            _controller = new ShowController(_mockRepo);
        }

        [TestMethod]
        public void ShowController_Constructor_InitializesCorrectly()
        {
            // Arrange & Act
            ShowController controller = new ShowController(_mockRepo);

            // Assert
            Assert.IsNotNull(controller);
        }

        [TestMethod]
        public async Task GetShowsByYear_ValidYear_ReturnsShows()
        {
            // Arrange
            int yearCode = 2023;
            List<Show> expectedShows = new List<Show>
            {
                new Show { ShowCode = 1, ShowDate = new System.DateTime(2023, 6, 1) },
                new Show { ShowCode = 2, ShowDate = new System.DateTime(2023, 8, 15) }
            };

            _mockRepo.GetListByYearAsync(yearCode).Returns(expectedShows);

            // Act
            List<Show> result = await _controller.GetShowsByYear(yearCode);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            await _mockRepo.Received(1).GetListByYearAsync(yearCode);
        }

        [TestMethod]
        public async Task GetShowsByYear_NoShows_ReturnsEmptyList()
        {
            // Arrange
            int yearCode = 1990;
            List<Show> expectedShows = new List<Show>();

            _mockRepo.GetListByYearAsync(yearCode).Returns(expectedShows);

            // Act
            List<Show> result = await _controller.GetShowsByYear(yearCode);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public async Task GetShowsBySong_ValidSongCode_ReturnsShows()
        {
            // Arrange
            int songCode = 10;
            List<Show> expectedShows = new List<Show>
            {
                new Show { ShowCode = 100 },
                new Show { ShowCode = 101 },
                new Show { ShowCode = 102 }
            };

            _mockRepo.GetListBySongAsync(songCode).Returns(expectedShows);

            // Act
            List<Show> result = await _controller.GetShowsBySong(songCode);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
            await _mockRepo.Received(1).GetListBySongAsync(songCode);
        }

        [TestMethod]
        public async Task GetShowsBySong_NoShows_ReturnsEmptyList()
        {
            // Arrange
            int songCode = 999;
            List<Show> expectedShows = new List<Show>();

            _mockRepo.GetListBySongAsync(songCode).Returns(expectedShows);

            // Act
            List<Show> result = await _controller.GetShowsBySong(songCode);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public async Task GetListByFFLCode_ReturnsShows()
        {
            // Arrange
            List<Show> expectedShows = new List<Show>
            {
                new Show { ShowCode = 1, FFLCode = 1001 },
                new Show { ShowCode = 2, FFLCode = 1002 }
            };

            _mockRepo.GetListByFFLCode().Returns(expectedShows);

            // Act
            List<Show> result = await _controller.GetListByFFLCode();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            await _mockRepo.Received(1).GetListByFFLCode();
        }

        [TestMethod]
        public async Task GetListByFFLCode_NoShows_ReturnsEmptyList()
        {
            // Arrange
            List<Show> expectedShows = new List<Show>();

            _mockRepo.GetListByFFLCode().Returns(expectedShows);

            // Act
            List<Show> result = await _controller.GetListByFFLCode();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public async Task GetShow_ValidShowCode_ReturnsShow()
        {
            // Arrange
            int showCode = 42;
            Show expectedShow = new Show 
            { 
                ShowCode = 42, 
                ShowDate = new System.DateTime(2022, 5, 20),
                FFLCode = 42
            };

            _mockRepo.GetItem(showCode).Returns(expectedShow);

            // Act
            Show result = await _controller.GetShow(showCode);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(42, result.ShowCode);
            await _mockRepo.Received(1).GetItem(showCode);
        }

        [TestMethod]
        public async Task GetShow_InvalidShowCode_ReturnsNull()
        {
            // Arrange
            int showCode = 999;
            Show expectedShow = null;

            _mockRepo.GetItem(showCode).Returns(expectedShow);

            // Act
            Show result = await _controller.GetShow(showCode);

            // Assert
            Assert.IsNull(result);
            await _mockRepo.Received(1).GetItem(showCode);
        }
    }
}
