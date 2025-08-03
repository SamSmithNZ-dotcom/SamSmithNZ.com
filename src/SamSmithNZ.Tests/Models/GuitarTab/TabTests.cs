using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithNZ.Service.Models.GuitarTab;
using System;

namespace SamSmithNZ.Tests.Models.GuitarTab
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class TabTests
    {
        [TestMethod]
        public void Tab_DefaultConstructor_CreatesInstance()
        {
            // Act
            Tab tab = new Tab();

            // Assert
            Assert.IsNotNull(tab);
            Assert.AreEqual(0, tab.TabCode);
            Assert.AreEqual(0, tab.AlbumCode);
            Assert.IsNull(tab.TabName);
            Assert.IsNull(tab.TabNameTrimed);
            Assert.IsNull(tab.TabText);
            Assert.AreEqual(0, tab.TabOrder);
            Assert.IsNull(tab.Rating);
            Assert.AreEqual(0, tab.TuningCode);
            Assert.IsNull(tab.TuningName);
            Assert.AreEqual(DateTime.MinValue, tab.LastUpdated);
        }

        [TestMethod]
        public void Tab_SetAllProperties_ReturnsAllValues()
        {
            // Arrange
            Tab tab = new Tab();
            var expectedTabCode = 1;
            var expectedAlbumCode = 2;
            var expectedTabName = "Stairway to Heaven";
            var expectedTabNameTrimed = "StairwaytoHeaven";
            var expectedTabText = "E|--0--3--5--";
            var expectedTabOrder = 1;
            var expectedRating = 5;
            var expectedTuningCode = 1;
            var expectedTuningName = "Standard";
            var expectedLastUpdated = DateTime.Now;

            // Act
            tab.TabCode = expectedTabCode;
            tab.AlbumCode = expectedAlbumCode;
            tab.TabName = expectedTabName;
            tab.TabNameTrimed = expectedTabNameTrimed;
            tab.TabText = expectedTabText;
            tab.TabOrder = expectedTabOrder;
            tab.Rating = expectedRating;
            tab.TuningCode = expectedTuningCode;
            tab.TuningName = expectedTuningName;
            tab.LastUpdated = expectedLastUpdated;

            // Assert
            Assert.AreEqual(expectedTabCode, tab.TabCode);
            Assert.AreEqual(expectedAlbumCode, tab.AlbumCode);
            Assert.AreEqual(expectedTabName, tab.TabName);
            Assert.AreEqual(expectedTabNameTrimed, tab.TabNameTrimed);
            Assert.AreEqual(expectedTabText, tab.TabText);
            Assert.AreEqual(expectedTabOrder, tab.TabOrder);
            Assert.AreEqual(expectedRating, tab.Rating);
            Assert.AreEqual(expectedTuningCode, tab.TuningCode);
            Assert.AreEqual(expectedTuningName, tab.TuningName);
            Assert.AreEqual(expectedLastUpdated, tab.LastUpdated);
        }

        [TestMethod]
        public void Tab_InitializeWithObjectInitializer_ReturnsAllValues()
        {
            // Arrange & Act
            var lastUpdated = DateTime.Now;
            Tab tab = new Tab
            {
                TabCode = 3,
                AlbumCode = 4,
                TabName = "Bohemian Rhapsody",
                TabNameTrimed = "BohemianRhapsody",
                TabText = "A|--2--0--3--",
                TabOrder = 2,
                Rating = 4,
                TuningCode = 2,
                TuningName = "Drop D",
                LastUpdated = lastUpdated
            };

            // Assert
            Assert.AreEqual(3, tab.TabCode);
            Assert.AreEqual(4, tab.AlbumCode);
            Assert.AreEqual("Bohemian Rhapsody", tab.TabName);
            Assert.AreEqual("BohemianRhapsody", tab.TabNameTrimed);
            Assert.AreEqual("A|--2--0--3--", tab.TabText);
            Assert.AreEqual(2, tab.TabOrder);
            Assert.AreEqual(4, tab.Rating);
            Assert.AreEqual(2, tab.TuningCode);
            Assert.AreEqual("Drop D", tab.TuningName);
            Assert.AreEqual(lastUpdated, tab.LastUpdated);
        }

        [TestMethod]
        public void Tab_NullRating_AllowsNullValue()
        {
            // Arrange
            Tab tab = new Tab();

            // Act
            tab.Rating = null;

            // Assert
            Assert.IsNull(tab.Rating);
        }
    }
}