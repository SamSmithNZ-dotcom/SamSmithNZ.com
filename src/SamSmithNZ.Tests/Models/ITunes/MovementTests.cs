using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithNZ.Service.Models.ITunes;

namespace SamSmithNZ.Tests.Models.ITunes
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class MovementTests
    {
        [TestMethod]
        public void Movement_DefaultConstructor_CreatesInstance()
        {
            // Act
            Movement movement = new Movement();

            // Assert
            Assert.IsNotNull(movement);
            Assert.IsNull(movement.TrackName);
            Assert.AreEqual(0, movement.PlayCount);
            Assert.AreEqual(0, movement.ChangeThisMonth);
        }

        [TestMethod]
        public void Movement_SetTrackName_ReturnsCorrectValue()
        {
            // Arrange
            Movement movement = new Movement();
            string expectedTrackName = "Bohemian Rhapsody";

            // Act
            movement.TrackName = expectedTrackName;

            // Assert
            Assert.AreEqual(expectedTrackName, movement.TrackName);
        }

        [TestMethod]
        public void Movement_SetPlayCount_ReturnsCorrectValue()
        {
            // Arrange
            Movement movement = new Movement();
            int expectedPlayCount = 150;

            // Act
            movement.PlayCount = expectedPlayCount;

            // Assert
            Assert.AreEqual(expectedPlayCount, movement.PlayCount);
        }

        [TestMethod]
        public void Movement_SetChangeThisMonth_ReturnsCorrectValue()
        {
            // Arrange
            Movement movement = new Movement();
            int expectedChange = -5;

            // Act
            movement.ChangeThisMonth = expectedChange;

            // Assert
            Assert.AreEqual(expectedChange, movement.ChangeThisMonth);
        }

        [TestMethod]
        public void Movement_InitializeWithAllProperties_ReturnsAllValues()
        {
            // Arrange & Act
            Movement movement = new Movement
            {
                TrackName = "Stairway to Heaven",
                PlayCount = 250,
                ChangeThisMonth = 10
            };

            // Assert
            Assert.AreEqual("Stairway to Heaven", movement.TrackName);
            Assert.AreEqual(250, movement.PlayCount);
            Assert.AreEqual(10, movement.ChangeThisMonth);
        }

        [TestMethod]
        public void Movement_SetNegativePlayCount_ReturnsNegativeValue()
        {
            // Arrange
            Movement movement = new Movement();
            int expectedPlayCount = -1;

            // Act
            movement.PlayCount = expectedPlayCount;

            // Assert
            Assert.AreEqual(expectedPlayCount, movement.PlayCount);
        }
    }
}