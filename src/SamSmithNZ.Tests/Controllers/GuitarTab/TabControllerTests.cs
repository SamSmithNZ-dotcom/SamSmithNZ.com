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
    public class TabControllerTests
    {
        private ITabDataAccess _mockRepo;
        private TabController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockRepo = Substitute.For<ITabDataAccess>();
            _controller = new TabController(_mockRepo);
        }

        [TestMethod]
        public void TabController_Constructor_InitializesCorrectly()
        {
            // Arrange & Act
            TabController controller = new TabController(_mockRepo);

            // Assert
            Assert.IsNotNull(controller);
        }

        [TestMethod]
        public async Task GetTabs_ValidParameters_CallsDataAccessWithCorrectParameters()
        {
            // Arrange
            int albumCode = 1;
            int sortOrder = 0;
            List<Tab> expectedResult = new List<Tab>
            {
                new Tab { TabCode = 1, AlbumCode = albumCode, TabName = "Test Track" }
            };
            _mockRepo.GetList(albumCode, sortOrder).Returns(expectedResult);

            // Act
            List<Tab> result = await _controller.GetTabs(albumCode, sortOrder);

            // Assert
            await _mockRepo.Received(1).GetList(albumCode, sortOrder);
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public async Task GetTabs_DefaultSortOrder_CallsDataAccessWithDefaultSortOrder()
        {
            // Arrange
            int albumCode = 2;
            List<Tab> expectedResult = new List<Tab>
            {
                new Tab { TabCode = 2, AlbumCode = albumCode, TabName = "Another Track" }
            };
            _mockRepo.GetList(albumCode, 0).Returns(expectedResult);

            // Act
            List<Tab> result = await _controller.GetTabs(albumCode);

            // Assert
            await _mockRepo.Received(1).GetList(albumCode, 0);
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public async Task GetTab_ValidTabCode_CallsDataAccessWithCorrectParameters()
        {
            // Arrange
            int tabCode = 1;
            Tab expectedResult = new Tab { TabCode = tabCode, TabName = "Test Track" };
            _mockRepo.GetItem(tabCode).Returns(expectedResult);

            // Act
            Tab result = await _controller.GetTab(tabCode);

            // Assert
            await _mockRepo.Received(1).GetItem(tabCode);
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public async Task SaveTab_ValidTab_CallsDataAccessAndReturnsTrue()
        {
            // Arrange
            Tab tab = new Tab { TabCode = 1, TabName = "New Track" };
            _mockRepo.SaveItem(tab).Returns(true);

            // Act
            bool result = await _controller.SaveTab(tab);

            // Assert
            await _mockRepo.Received(1).SaveItem(tab);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task DeleteTab_ValidTabCode_CallsDataAccessAndReturnsTrue()
        {
            // Arrange
            int tabCode = 1;
            _mockRepo.DeleteItem(tabCode).Returns(true);

            // Act
            bool result = await _controller.DeleteTab(tabCode);

            // Assert
            await _mockRepo.Received(1).DeleteItem(tabCode);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task GetTabs_EmptyResult_ReturnsEmptyList()
        {
            // Arrange
            int albumCode = 999;
            List<Tab> expectedResult = new List<Tab>();
            _mockRepo.GetList(albumCode, 0).Returns(expectedResult);

            // Act
            List<Tab> result = await _controller.GetTabs(albumCode);

            // Assert
            await _mockRepo.Received(1).GetList(albumCode, 0);
            Assert.AreEqual(0, result.Count);
        }
    }
}