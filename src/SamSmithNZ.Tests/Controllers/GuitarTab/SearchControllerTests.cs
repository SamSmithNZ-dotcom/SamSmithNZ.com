using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using SamSmithNZ.Service.Controllers.GuitarTab;
using SamSmithNZ.Service.DataAccess.GuitarTab.Interfaces;
using SamSmithNZ.Service.Models.GuitarTab;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Tests.Controllers.GuitarTab
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class SearchControllerTests
    {
        private ISearchDataAccess _mockRepo;
        private SearchController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockRepo = Substitute.For<ISearchDataAccess>();
            _controller = new SearchController(_mockRepo);
        }

        [TestMethod]
        public void SearchController_Constructor_InitializesCorrectly()
        {
            // Arrange & Act
            SearchController controller = new SearchController(_mockRepo);

            // Assert
            Assert.IsNotNull(controller);
        }

        [TestMethod]
        public async Task GetSearchResults_WithRecordId_CallsGetListWithRecordId()
        {
            // Arrange
            string searchText = "test search";
            Guid recordId = Guid.NewGuid();
            List<Search> expectedResult = new List<Search>
            {
                new Search { SearchText = searchText, AlbumCode = 1 }
            };
            _mockRepo.GetList(recordId).Returns(expectedResult);

            // Act
            List<Search> result = await _controller.GetSearchResults(searchText, recordId);

            // Assert
            await _mockRepo.Received(1).GetList(recordId);
            await _mockRepo.DidNotReceive().SaveItem(Arg.Any<string>());
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public async Task GetSearchResults_WithoutRecordId_SavesItemThenCallsGetList()
        {
            // Arrange
            string searchText = "new search";
            Guid savedRecordId = Guid.NewGuid();
            List<Search> expectedResult = new List<Search>
            {
                new Search { SearchText = searchText, AlbumCode = 2 }
            };
            _mockRepo.SaveItem(searchText).Returns(savedRecordId);
            _mockRepo.GetList(savedRecordId).Returns(expectedResult);

            // Act
            List<Search> result = await _controller.GetSearchResults(searchText);

            // Assert
            await _mockRepo.Received(1).SaveItem(searchText);
            await _mockRepo.Received(1).GetList(savedRecordId);
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public async Task GetSearchResults_WithNullRecordId_SavesItemThenCallsGetList()
        {
            // Arrange
            string searchText = "null search";
            Guid? recordId = null;
            Guid savedRecordId = Guid.NewGuid();
            List<Search> expectedResult = new List<Search>
            {
                new Search { SearchText = searchText, AlbumCode = 4 }
            };
            _mockRepo.SaveItem(searchText).Returns(savedRecordId);
            _mockRepo.GetList(savedRecordId).Returns(expectedResult);

            // Act
            List<Search> result = await _controller.GetSearchResults(searchText, recordId);

            // Assert
            await _mockRepo.Received(1).SaveItem(searchText);
            await _mockRepo.Received(1).GetList(savedRecordId);
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public async Task GetSearchResults_EmptyResult_ReturnsEmptyList()
        {
            // Arrange
            string searchText = "empty search";
            Guid recordId = Guid.NewGuid();
            List<Search> expectedResult = new List<Search>();
            _mockRepo.GetList(recordId).Returns(expectedResult);

            // Act
            List<Search> result = await _controller.GetSearchResults(searchText, recordId);

            // Assert
            await _mockRepo.Received(1).GetList(recordId);
            Assert.AreEqual(expectedResult, result);
            Assert.AreEqual(0, result.Count);
        }
    }
}