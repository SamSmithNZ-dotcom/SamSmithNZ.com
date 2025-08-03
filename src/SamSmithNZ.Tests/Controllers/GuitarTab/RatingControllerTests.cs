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
    public class RatingControllerTests
    {
        private IRatingDataAccess _mockRepo;
        private RatingController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockRepo = Substitute.For<IRatingDataAccess>();
            _controller = new RatingController(_mockRepo);
        }

        [TestMethod]
        public void RatingController_Constructor_InitializesCorrectly()
        {
            // Arrange & Act
            RatingController controller = new RatingController(_mockRepo);

            // Assert
            Assert.IsNotNull(controller);
        }

        [TestMethod]
        public async Task GetRatings_CallsDataAccess_ReturnsExpectedResult()
        {
            // Arrange
            List<Rating> expectedResult = new List<Rating>
            {
                new Rating { RatingCode = 1 },
                new Rating { RatingCode = 2 }
            };
            _mockRepo.GetList().Returns(expectedResult);

            // Act
            List<Rating> result = await _controller.GetRatings();

            // Assert
            await _mockRepo.Received(1).GetList();
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public async Task GetRatings_EmptyResult_ReturnsEmptyList()
        {
            // Arrange
            List<Rating> expectedResult = new List<Rating>();
            _mockRepo.GetList().Returns(expectedResult);

            // Act
            List<Rating> result = await _controller.GetRatings();

            // Assert
            await _mockRepo.Received(1).GetList();
            Assert.AreEqual(expectedResult, result);
            Assert.AreEqual(0, result.Count);
        }
    }
}