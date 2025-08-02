using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithNZ.Service.Models.ITunes;

namespace SamSmithNZ.Tests.Models.ITunes
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class TopArtistsTests
    {
        [TestMethod]
        public void TopArtists_SetAndGetArtistName_ReturnsCorrectValue()
        {
            // Arrange
            var topArtist = new TopArtists();
            var expectedArtistName = "The Beatles";

            // Act
            topArtist.ArtistName = expectedArtistName;

            // Assert
            Assert.AreEqual(expectedArtistName, topArtist.ArtistName);
        }

        [TestMethod]
        public void TopArtists_SetAndGetArtistCount_ReturnsCorrectValue()
        {
            // Arrange
            var topArtist = new TopArtists();
            var expectedArtistCount = 42;

            // Act
            topArtist.ArtistCount = expectedArtistCount;

            // Assert
            Assert.AreEqual(expectedArtistCount, topArtist.ArtistCount);
        }

        [TestMethod]
        public void TopArtists_DefaultConstructor_CreatesInstance()
        {
            // Act
            var topArtist = new TopArtists();

            // Assert
            Assert.IsNotNull(topArtist);
            Assert.IsNull(topArtist.ArtistName);
            Assert.AreEqual(0, topArtist.ArtistCount);
        }

        [TestMethod]
        public void TopArtists_InitializeWithAllProperties_ReturnsAllValues()
        {
            // Arrange
            var expectedArtistName = "Queen";
            var expectedArtistCount = 123;

            // Act
            var topArtist = new TopArtists
            {
                ArtistName = expectedArtistName,
                ArtistCount = expectedArtistCount
            };

            // Assert
            Assert.AreEqual(expectedArtistName, topArtist.ArtistName);
            Assert.AreEqual(expectedArtistCount, topArtist.ArtistCount);
        }
    }
}