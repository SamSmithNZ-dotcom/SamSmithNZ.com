using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithNZ.Service.Controllers.MandMCounter;

namespace SamSmithNZ.Tests.Controllers.MandMCounter
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class MandMCounterControllerTests
    {
        private MandMCounterController _controller;

        [TestInitialize]
        public void Setup()
        {
            _controller = new MandMCounterController();
        }

        [TestMethod]
        public void MandMCounterController_Constructor_InitializesCorrectly()
        {
            // Arrange & Act
            MandMCounterController controller = new MandMCounterController();

            // Assert
            Assert.IsNotNull(controller);
        }

        #region GetDataForUnit Tests

        [TestMethod]
        public void GetDataForUnit_CupWith1Quantity_ReturnsPositiveValue()
        {
            // Arrange
            string unit = "cup";
            float quantity = 1;

            // Act
            float result = _controller.GetDataForUnit(unit, quantity);

            // Assert
            Assert.IsTrue(result > 0, "Result should be greater than 0");
        }

        [TestMethod]
        public void GetDataForUnit_CupWith2Quantity_ReturnsDoubleTheAmount()
        {
            // Arrange
            string unit = "cup";
            float quantity1 = 1;
            float quantity2 = 2;

            // Act
            float result1 = _controller.GetDataForUnit(unit, quantity1);
            float result2 = _controller.GetDataForUnit(unit, quantity2);

            // Assert
            Assert.AreEqual(result1 * 2, result2, 0.001f, "Double quantity should give double the result");
        }

        [TestMethod]
        public void GetDataForUnit_ZeroQuantity_ReturnsZero()
        {
            // Arrange
            string unit = "cup";
            float quantity = 0;

            // Act
            float result = _controller.GetDataForUnit(unit, quantity);

            // Assert
            Assert.AreEqual(0, result, 0.001f);
        }

        [TestMethod]
        public void GetDataForUnit_DifferentUnits_ReturnsDifferentValues()
        {
            // Arrange
            float quantity = 1;

            // Act
            float cupResult = _controller.GetDataForUnit("cup", quantity);
            float quartResult = _controller.GetDataForUnit("quart", quantity);

            // Assert
            Assert.AreNotEqual(cupResult, quartResult, "Different units should return different values");
        }

        [TestMethod]
        public void GetDataForUnit_FractionalQuantity_ReturnsProportionalValue()
        {
            // Arrange
            string unit = "cup";
            float quantity = 0.5f;

            // Act
            float fullResult = _controller.GetDataForUnit(unit, 1);
            float halfResult = _controller.GetDataForUnit(unit, quantity);

            // Assert
            Assert.AreEqual(fullResult / 2, halfResult, 0.001f, "Half quantity should give half the result");
        }

        #endregion

        #region GetDataForRectangle Tests

        [TestMethod]
        public void GetDataForRectangle_ValidDimensions_ReturnsPositiveValue()
        {
            // Arrange
            string unit = "cm";
            float height = 10;
            float width = 20;
            float length = 30;

            // Act
            float result = _controller.GetDataForRectangle(unit, height, width, length);

            // Assert
            Assert.IsTrue(result > 0, "Result should be greater than 0 for valid dimensions");
        }

        [TestMethod]
        public void GetDataForRectangle_DoubleDimensions_ReturnsEightTimesTheAmount()
        {
            // Arrange
            string unit = "cm";
            float height = 5;
            float width = 10;
            float length = 15;

            // Act
            float result1 = _controller.GetDataForRectangle(unit, height, width, length);
            float result2 = _controller.GetDataForRectangle(unit, height * 2, width * 2, length * 2);

            // Assert
            Assert.AreEqual(result1 * 8, result2, 0.1f, "Doubling all dimensions should give 8x volume");
        }

        [TestMethod]
        public void GetDataForRectangle_ZeroHeight_ReturnsZero()
        {
            // Arrange
            string unit = "cm";
            float height = 0;
            float width = 10;
            float length = 20;

            // Act
            float result = _controller.GetDataForRectangle(unit, height, width, length);

            // Assert
            Assert.AreEqual(0, result, 0.001f);
        }

        [TestMethod]
        public void GetDataForRectangle_ZeroWidth_ReturnsZero()
        {
            // Arrange
            string unit = "cm";
            float height = 10;
            float width = 0;
            float length = 20;

            // Act
            float result = _controller.GetDataForRectangle(unit, height, width, length);

            // Assert
            Assert.AreEqual(0, result, 0.001f);
        }

        [TestMethod]
        public void GetDataForRectangle_ZeroLength_ReturnsZero()
        {
            // Arrange
            string unit = "cm";
            float height = 10;
            float width = 20;
            float length = 0;

            // Act
            float result = _controller.GetDataForRectangle(unit, height, width, length);

            // Assert
            Assert.AreEqual(0, result, 0.001f);
        }

        [TestMethod]
        public void GetDataForRectangle_DifferentUnits_ReturnsDifferentValues()
        {
            // Arrange
            float height = 10;
            float width = 10;
            float length = 10;

            // Act
            float cmResult = _controller.GetDataForRectangle("cm", height, width, length);
            float inchResult = _controller.GetDataForRectangle("inch", height, width, length);

            // Assert
            Assert.AreNotEqual(cmResult, inchResult, "Different units should return different values");
        }

        #endregion

        #region GetDataForCylinder Tests

        [TestMethod]
        public void GetDataForCylinder_ValidDimensions_ReturnsPositiveValue()
        {
            // Arrange
            string unit = "cm";
            float height = 10;
            float radius = 5;

            // Act
            float result = _controller.GetDataForCylinder(unit, height, radius);

            // Assert
            Assert.IsTrue(result > 0, "Result should be greater than 0 for valid dimensions");
        }

        [TestMethod]
        public void GetDataForCylinder_DoubleRadius_ReturnsFourTimesTheAmount()
        {
            // Arrange
            string unit = "cm";
            float height = 10;
            float radius = 5;

            // Act
            float result1 = _controller.GetDataForCylinder(unit, height, radius);
            float result2 = _controller.GetDataForCylinder(unit, height, radius * 2);

            // Assert
            Assert.AreEqual(result1 * 4, result2, 0.1f, "Doubling radius should quadruple the volume");
        }

        [TestMethod]
        public void GetDataForCylinder_DoubleHeight_ReturnsDoubleTheAmount()
        {
            // Arrange
            string unit = "cm";
            float height = 10;
            float radius = 5;

            // Act
            float result1 = _controller.GetDataForCylinder(unit, height, radius);
            float result2 = _controller.GetDataForCylinder(unit, height * 2, radius);

            // Assert
            Assert.AreEqual(result1 * 2, result2, 0.1f, "Doubling height should double the volume");
        }

        [TestMethod]
        public void GetDataForCylinder_ZeroHeight_ReturnsZero()
        {
            // Arrange
            string unit = "cm";
            float height = 0;
            float radius = 5;

            // Act
            float result = _controller.GetDataForCylinder(unit, height, radius);

            // Assert
            Assert.AreEqual(0, result, 0.001f);
        }

        [TestMethod]
        public void GetDataForCylinder_ZeroRadius_ReturnsZero()
        {
            // Arrange
            string unit = "cm";
            float height = 10;
            float radius = 0;

            // Act
            float result = _controller.GetDataForCylinder(unit, height, radius);

            // Assert
            Assert.AreEqual(0, result, 0.001f);
        }

        [TestMethod]
        public void GetDataForCylinder_DifferentUnits_ReturnsDifferentValues()
        {
            // Arrange
            float height = 10;
            float radius = 5;

            // Act
            float cmResult = _controller.GetDataForCylinder("cm", height, radius);
            float inchResult = _controller.GetDataForCylinder("inch", height, radius);

            // Assert
            Assert.AreNotEqual(cmResult, inchResult, "Different units should return different values");
        }

        #endregion
    }
}
