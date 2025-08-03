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
    public class TuningControllerTests
    {
        private ITuningDataAccess _mockRepo;
        private TuningController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockRepo = Substitute.For<ITuningDataAccess>();
            _controller = new TuningController(_mockRepo);
        }

        [TestMethod]
        public void TuningController_Constructor_InitializesCorrectly()
        {
            // Arrange & Act
            TuningController controller = new TuningController(_mockRepo);

            // Assert
            Assert.IsNotNull(controller);
        }

        [TestMethod]
        public async Task GetTunings_CallsDataAccess_ReturnsExpectedResult()
        {
            // Arrange
            List<Tuning> expectedResult = new List<Tuning>
            {
                new Tuning { TuningCode = 1, TuningName = "Standard" },
                new Tuning { TuningCode = 2, TuningName = "Drop D" }
            };
            _mockRepo.GetList().Returns(expectedResult);

            // Act
            List<Tuning> result = await _controller.GetTunings();

            // Assert
            await _mockRepo.Received(1).GetList();
            Assert.AreEqual(expectedResult, result);
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public async Task GetTunings_EmptyResult_ReturnsEmptyList()
        {
            // Arrange
            List<Tuning> expectedResult = new List<Tuning>();
            _mockRepo.GetList().Returns(expectedResult);

            // Act
            List<Tuning> result = await _controller.GetTunings();

            // Assert
            await _mockRepo.Received(1).GetList();
            Assert.AreEqual(expectedResult, result);
            Assert.AreEqual(0, result.Count);
        }
    }
}