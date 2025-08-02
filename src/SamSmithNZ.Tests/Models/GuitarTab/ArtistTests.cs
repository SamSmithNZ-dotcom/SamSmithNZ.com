using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithNZ.Service.Models.GuitarTab;

namespace SamSmithNZ.Tests.Models.GuitarTab
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class ArtistTests
    {
        [TestMethod]
        public void Artist_SetAndGetArtistName_ReturnsCorrectValue()
        {
            // Arrange
            var artist = new Artist();
            var expectedArtistName = "Metallica";

            // Act
            artist.ArtistName = expectedArtistName;

            // Assert
            Assert.AreEqual(expectedArtistName, artist.ArtistName);
        }

        [TestMethod]
        public void Artist_SetAndGetArtistNameTrimed_ReturnsCorrectValue()
        {
            // Arrange
            var artist = new Artist();
            var expectedArtistNameTrimed = "metallica";

            // Act
            artist.ArtistNameTrimed = expectedArtistNameTrimed;

            // Assert
            Assert.AreEqual(expectedArtistNameTrimed, artist.ArtistNameTrimed);
        }

        [TestMethod]
        public void Artist_DefaultConstructor_CreatesInstance()
        {
            // Act
            var artist = new Artist();

            // Assert
            Assert.IsNotNull(artist);
            Assert.IsNull(artist.ArtistName);
            Assert.IsNull(artist.ArtistNameTrimed);
        }

        [TestMethod]
        public void Artist_InitializeWithAllProperties_ReturnsAllValues()
        {
            // Arrange
            var expectedArtistName = "Iron Maiden";
            var expectedArtistNameTrimed = "ironmaiden";

            // Act
            var artist = new Artist
            {
                ArtistName = expectedArtistName,
                ArtistNameTrimed = expectedArtistNameTrimed
            };

            // Assert
            Assert.AreEqual(expectedArtistName, artist.ArtistName);
            Assert.AreEqual(expectedArtistNameTrimed, artist.ArtistNameTrimed);
        }
    }
}