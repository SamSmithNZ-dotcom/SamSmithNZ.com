using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using SamSmithNZ.Service.Controllers.WorldCup;
using SamSmithNZ.Service.DataAccess.WorldCup.Interfaces;
using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Tests.Controllers.WorldCup
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class InsightsControllerTests
    {
        private IGoalInsightsDataAccess _mockRepo;
        private InsightsController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockRepo = Substitute.For<IGoalInsightsDataAccess>();
            _controller = new InsightsController(_mockRepo);
        }

        [TestMethod]
        public void InsightsController_Constructor_InitializesCorrectly()
        {
            // Arrange & Act
            InsightsController controller = new InsightsController(_mockRepo);

            // Assert
            Assert.IsNotNull(controller);
        }

        [TestMethod]
        public async Task GetGames_AnalyzeExtraTimeFalse_ReturnsGoalInsightsList()
        {
            // Arrange
            bool analyzeExtraTime = false;
            List<GoalInsight> expectedInsights = new List<GoalInsight>
            {
                new GoalInsight 
                { 
                    GoalTime = 15.5, 
                    GoalCount = 120,
                    GoalCountPercent = 10.5
                },
                new GoalInsight 
                { 
                    GoalTime = 45.0, 
                    GoalCount = 95,
                    GoalCountPercent = 8.3
                }
            };
            _mockRepo.GetList(analyzeExtraTime).Returns(expectedInsights);

            // Act
            List<GoalInsight> result = await _controller.GetGames(analyzeExtraTime);

            // Assert
            await _mockRepo.Received(1).GetList(analyzeExtraTime);
            Assert.AreEqual(expectedInsights, result);
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public async Task GetGames_AnalyzeExtraTimeTrue_ReturnsGoalInsightsWithExtraTime()
        {
            // Arrange
            bool analyzeExtraTime = true;
            List<GoalInsight> expectedInsights = new List<GoalInsight>
            {
                new GoalInsight 
                { 
                    GoalTime = 90.5, 
                    GoalCount = 45,
                    GoalCountPercent = 3.9
                },
                new GoalInsight 
                { 
                    GoalTime = 105.0, 
                    GoalCount = 22,
                    GoalCountPercent = 1.9
                }
            };
            _mockRepo.GetList(analyzeExtraTime).Returns(expectedInsights);

            // Act
            List<GoalInsight> result = await _controller.GetGames(analyzeExtraTime);

            // Assert
            await _mockRepo.Received(1).GetList(analyzeExtraTime);
            Assert.AreEqual(expectedInsights, result);
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result[0].GoalTime > 90);
        }

        [TestMethod]
        public async Task GetGames_EmptyList_ReturnsEmptyList()
        {
            // Arrange
            bool analyzeExtraTime = false;
            List<GoalInsight> expectedInsights = new List<GoalInsight>();
            _mockRepo.GetList(analyzeExtraTime).Returns(expectedInsights);

            // Act
            List<GoalInsight> result = await _controller.GetGames(analyzeExtraTime);

            // Assert
            await _mockRepo.Received(1).GetList(analyzeExtraTime);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public async Task GetGames_CallsRepositoryWithCorrectParameter()
        {
            // Arrange
            bool analyzeExtraTime = true;
            List<GoalInsight> insights = new List<GoalInsight> 
            { 
                new GoalInsight { GoalTime = 12.0, GoalCount = 50, GoalCountPercent = 4.5 } 
            };
            _mockRepo.GetList(analyzeExtraTime).Returns(insights);

            // Act
            await _controller.GetGames(analyzeExtraTime);

            // Assert
            await _mockRepo.Received(1).GetList(analyzeExtraTime);
            await _mockRepo.DidNotReceive().GetList(!analyzeExtraTime);
        }

        [TestMethod]
        public async Task GetGames_VerifiesDataIntegrity()
        {
            // Arrange
            bool analyzeExtraTime = false;
            List<GoalInsight> expectedInsights = new List<GoalInsight>
            {
                new GoalInsight { GoalTime = 30.0, GoalCount = 100, GoalCountPercent = 8.7 },
                new GoalInsight { GoalTime = 60.0, GoalCount = 85, GoalCountPercent = 7.4 },
                new GoalInsight { GoalTime = 75.0, GoalCount = 70, GoalCountPercent = 6.1 }
            };
            _mockRepo.GetList(analyzeExtraTime).Returns(expectedInsights);

            // Act
            List<GoalInsight> result = await _controller.GetGames(analyzeExtraTime);

            // Assert
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(30.0, result[0].GoalTime);
            Assert.AreEqual(100, result[0].GoalCount);
            Assert.AreEqual(8.7, result[0].GoalCountPercent);
        }
    }
}
