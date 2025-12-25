using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using SamSmithNZ.Service.Controllers.WorldCup;
using SamSmithNZ.Service.DataAccess.WorldCup.Interfaces;
using System.Threading.Tasks;

namespace SamSmithNZ.Tests.Controllers.WorldCup
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class EloRatingControllerTests
    {
        private IEloRatingDataAccess _mockRepo;
        private IGameDataAccess _mockGameRepo;
        private EloRatingController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockRepo = Substitute.For<IEloRatingDataAccess>();
            _mockGameRepo = Substitute.For<IGameDataAccess>();
            _controller = new EloRatingController(_mockRepo, _mockGameRepo);
        }

        [TestMethod]
        public void EloRatingController_Constructor_InitializesCorrectly()
        {
            // Arrange & Act
            EloRatingController controller = new EloRatingController(_mockRepo, _mockGameRepo);

            // Assert
            Assert.IsNotNull(controller);
        }

        [TestMethod]
        public async Task RefreshTournamentELORatings_ValidTournament_ReturnsTrue()
        {
            // Arrange
            int tournamentCode = 40;
            _mockRepo.UpdateTournamentELORatings(tournamentCode).Returns(true);

            // Act
            bool result = await _controller.RefreshTournamentELORatings(tournamentCode);

            // Assert
            Assert.IsTrue(result);
            await _mockRepo.Received(1).UpdateTournamentELORatings(tournamentCode);
        }

        [TestMethod]
        public async Task RefreshTournamentELORatings_UpdateFails_ReturnsFalse()
        {
            // Arrange
            int tournamentCode = 99;
            _mockRepo.UpdateTournamentELORatings(tournamentCode).Returns(false);

            // Act
            bool result = await _controller.RefreshTournamentELORatings(tournamentCode);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
