using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithNZ.Tests;
using SamSmithNZ.Service.Models.WorldCup;

namespace SamSmithNZ.Tests.Models.WorldCup
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class TournamentTeamModelTests : BaseIntegrationTest
    {
        [TestMethod]
        public void TournamentTeam_ELORatingDifference_PositiveValue_ReturnsWithPlusSign()
        {
            // Arrange
            TournamentTeam team = new TournamentTeam
            {
                StartingEloRating = 1500,
                CurrentEloRating = 1550
            };

            // Act
            string result = team.ELORatingDifference;

            // Assert
            Assert.AreEqual("+50", result);
        }

        [TestMethod]
        public void TournamentTeam_ELORatingDifference_NegativeValue_ReturnsWithoutPlusSign()
        {
            // Arrange
            TournamentTeam team = new TournamentTeam
            {
                StartingEloRating = 1500,
                CurrentEloRating = 1450
            };

            // Act
            string result = team.ELORatingDifference;

            // Assert
            Assert.AreEqual("-50", result);
        }

        [TestMethod]
        public void TournamentTeam_ELORatingDifference_ZeroValue_ReturnsZero()
        {
            // Arrange
            TournamentTeam team = new TournamentTeam
            {
                StartingEloRating = 1500,
                CurrentEloRating = 1500
            };

            // Act
            string result = team.ELORatingDifference;

            // Assert
            Assert.AreEqual("0", result);
        }
    }
}
