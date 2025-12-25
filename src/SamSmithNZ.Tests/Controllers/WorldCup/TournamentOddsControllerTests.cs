using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithNZ.Service.Controllers.WorldCup;

namespace SamSmithNZ.Tests.Controllers.WorldCup
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class TournamentOddsControllerTests
    {
        [TestMethod]
        public void TournamentOddsController_Index_ReturnsView()
        {
            // Arrange
            TournamentOddsController controller = new TournamentOddsController();

            // Act
            IActionResult result = controller.Index();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
