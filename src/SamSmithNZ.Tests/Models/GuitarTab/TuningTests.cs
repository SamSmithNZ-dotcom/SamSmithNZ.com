using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithNZ.Service.Models.GuitarTab;

namespace SamSmithNZ.Tests.Models.GuitarTab
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class TuningTests
    {
        [TestMethod]
        public void Tuning_DefaultConstructor_CreatesInstance()
        {
            // Act
            Tuning tuning = new Tuning();

            // Assert
            Assert.IsNotNull(tuning);
            Assert.AreEqual(0, tuning.TuningCode);
            Assert.IsNull(tuning.TuningName);
        }

        [TestMethod]
        public void Tuning_SetTuningCode_ReturnsCorrectValue()
        {
            // Arrange
            Tuning tuning = new Tuning();
            int expectedTuningCode = 1;

            // Act
            tuning.TuningCode = expectedTuningCode;

            // Assert
            Assert.AreEqual(expectedTuningCode, tuning.TuningCode);
        }

        [TestMethod]
        public void Tuning_SetTuningName_ReturnsCorrectValue()
        {
            // Arrange
            Tuning tuning = new Tuning();
            string expectedTuningName = "Standard";

            // Act
            tuning.TuningName = expectedTuningName;

            // Assert
            Assert.AreEqual(expectedTuningName, tuning.TuningName);
        }

        [TestMethod]
        public void Tuning_InitializeWithAllProperties_ReturnsAllValues()
        {
            // Arrange & Act
            Tuning tuning = new Tuning
            {
                TuningCode = 2,
                TuningName = "Drop D"
            };

            // Assert
            Assert.AreEqual(2, tuning.TuningCode);
            Assert.AreEqual("Drop D", tuning.TuningName);
        }

        [TestMethod]
        public void Tuning_SetEmptyTuningName_ReturnsEmptyString()
        {
            // Arrange
            Tuning tuning = new Tuning();
            string expectedTuningName = "";

            // Act
            tuning.TuningName = expectedTuningName;

            // Assert
            Assert.AreEqual(expectedTuningName, tuning.TuningName);
        }
    }
}