using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithNZ.Service;

namespace SamSmithNZ.Tests.Utility
{
    [TestClass]
    [TestCategory("L0")]
    public class UtilityTests
    {
        [TestMethod]
        public void GenerateRandomNumber_ValidRange_ReturnsNumberInRange()
        {
            // Arrange
            int lowerBound = 1;
            int upperBound = 10;

            // Act
            double result = Service.Utility.GenerateRandomNumber(lowerBound, upperBound);

            // Assert
            Assert.IsTrue(result >= lowerBound, $"Result {result} should be >= {lowerBound}");
            Assert.IsTrue(result <= upperBound, $"Result {result} should be <= {upperBound}");
        }

        [TestMethod]
        public void GenerateRandomNumber_SameUpperAndLowerBound_ReturnsThatNumber()
        {
            // Arrange
            int bound = 5;

            // Act
            double result = Service.Utility.GenerateRandomNumber(bound, bound);

            // Assert
            Assert.AreEqual(bound, result);
        }

        [TestMethod]
        public void GenerateRandomNumber_NegativeRange_ReturnsNumberInRange()
        {
            // Arrange
            int lowerBound = -10;
            int upperBound = -1;

            // Act
            double result = Service.Utility.GenerateRandomNumber(lowerBound, upperBound);

            // Assert
            Assert.IsTrue(result >= lowerBound, $"Result {result} should be >= {lowerBound}");
            Assert.IsTrue(result <= upperBound, $"Result {result} should be <= {upperBound}");
        }

        [TestMethod]
        public void GenerateRandomNumber_ZeroRange_ReturnsZero()
        {
            // Arrange
            int lowerBound = 0;
            int upperBound = 0;

            // Act
            double result = Service.Utility.GenerateRandomNumber(lowerBound, upperBound);

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void GenerateRandomNumber_MultipleCalls_ReturnsValidNumbers()
        {
            // Arrange
            int lowerBound = 1;
            int upperBound = 100;

            // Act & Assert
            for (int i = 0; i < 10; i++)
            {
                double result = Service.Utility.GenerateRandomNumber(lowerBound, upperBound);
                Assert.IsTrue(result >= lowerBound, $"Result {result} should be >= {lowerBound}");
                Assert.IsTrue(result <= upperBound, $"Result {result} should be <= {upperBound}");
            }
        }
    }
}