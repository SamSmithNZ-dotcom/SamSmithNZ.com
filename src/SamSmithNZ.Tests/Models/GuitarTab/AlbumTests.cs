using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithNZ.Service.Models.GuitarTab;

namespace SamSmithNZ.Tests.Models.GuitarTab
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class AlbumTests
    {
        [TestMethod]
        public void Album_DefaultConstructor_CreatesInstance()
        {
            // Act
            var album = new Album();

            // Assert
            Assert.IsNotNull(album);
            Assert.AreEqual(0, album.AlbumCode);
            Assert.IsNull(album.ArtistName);
            Assert.IsNull(album.ArtistNameTrimed);
            Assert.IsFalse(album.IsLeadArtist);
            Assert.IsNull(album.AlbumName);
            Assert.AreEqual(0, album.AlbumYear);
            Assert.AreEqual(0, album.BassAlbumCode);
            Assert.IsFalse(album.IsBassTab);
            Assert.IsFalse(album.IsNewAlbum);
            Assert.IsFalse(album.IsMiscCollectionAlbum);
            Assert.IsFalse(album.IncludeInIndex);
            Assert.IsFalse(album.IncludeOnWebsite);
            Assert.AreEqual(0m, album.AverageRating);
        }

        [TestMethod]
        public void Album_SetAllProperties_ReturnsAllValues()
        {
            // Arrange
            var album = new Album();
            var expectedAlbumCode = 123;
            var expectedArtistName = "Metallica";
            var expectedArtistNameTrimed = "metallica";
            var expectedIsLeadArtist = true;
            var expectedAlbumName = "Master of Puppets";
            var expectedAlbumYear = 1986;
            var expectedBassAlbumCode = 456;
            var expectedIsBassTab = true;
            var expectedIsNewAlbum = false;
            var expectedIsMiscCollectionAlbum = false;
            var expectedIncludeInIndex = true;
            var expectedIncludeOnWebsite = true;
            var expectedAverageRating = 4.8m;

            // Act
            album.AlbumCode = expectedAlbumCode;
            album.ArtistName = expectedArtistName;
            album.ArtistNameTrimed = expectedArtistNameTrimed;
            album.IsLeadArtist = expectedIsLeadArtist;
            album.AlbumName = expectedAlbumName;
            album.AlbumYear = expectedAlbumYear;
            album.BassAlbumCode = expectedBassAlbumCode;
            album.IsBassTab = expectedIsBassTab;
            album.IsNewAlbum = expectedIsNewAlbum;
            album.IsMiscCollectionAlbum = expectedIsMiscCollectionAlbum;
            album.IncludeInIndex = expectedIncludeInIndex;
            album.IncludeOnWebsite = expectedIncludeOnWebsite;
            album.AverageRating = expectedAverageRating;

            // Assert
            Assert.AreEqual(expectedAlbumCode, album.AlbumCode);
            Assert.AreEqual(expectedArtistName, album.ArtistName);
            Assert.AreEqual(expectedArtistNameTrimed, album.ArtistNameTrimed);
            Assert.AreEqual(expectedIsLeadArtist, album.IsLeadArtist);
            Assert.AreEqual(expectedAlbumName, album.AlbumName);
            Assert.AreEqual(expectedAlbumYear, album.AlbumYear);
            Assert.AreEqual(expectedBassAlbumCode, album.BassAlbumCode);
            Assert.AreEqual(expectedIsBassTab, album.IsBassTab);
            Assert.AreEqual(expectedIsNewAlbum, album.IsNewAlbum);
            Assert.AreEqual(expectedIsMiscCollectionAlbum, album.IsMiscCollectionAlbum);
            Assert.AreEqual(expectedIncludeInIndex, album.IncludeInIndex);
            Assert.AreEqual(expectedIncludeOnWebsite, album.IncludeOnWebsite);
            Assert.AreEqual(expectedAverageRating, album.AverageRating);
        }

        [TestMethod]
        public void Album_InitializeWithObjectInitializer_ReturnsAllValues()
        {
            // Arrange
            var expectedAlbumCode = 789;
            var expectedArtistName = "Iron Maiden";
            var expectedArtistNameTrimed = "ironmaiden";
            var expectedIsLeadArtist = true;
            var expectedAlbumName = "The Number of the Beast";
            var expectedAlbumYear = 1982;
            var expectedBassAlbumCode = 101;
            var expectedIsBassTab = false;
            var expectedIsNewAlbum = true;
            var expectedIsMiscCollectionAlbum = false;
            var expectedIncludeInIndex = true;
            var expectedIncludeOnWebsite = true;
            var expectedAverageRating = 4.9m;

            // Act
            var album = new Album
            {
                AlbumCode = expectedAlbumCode,
                ArtistName = expectedArtistName,
                ArtistNameTrimed = expectedArtistNameTrimed,
                IsLeadArtist = expectedIsLeadArtist,
                AlbumName = expectedAlbumName,
                AlbumYear = expectedAlbumYear,
                BassAlbumCode = expectedBassAlbumCode,
                IsBassTab = expectedIsBassTab,
                IsNewAlbum = expectedIsNewAlbum,
                IsMiscCollectionAlbum = expectedIsMiscCollectionAlbum,
                IncludeInIndex = expectedIncludeInIndex,
                IncludeOnWebsite = expectedIncludeOnWebsite,
                AverageRating = expectedAverageRating
            };

            // Assert
            Assert.AreEqual(expectedAlbumCode, album.AlbumCode);
            Assert.AreEqual(expectedArtistName, album.ArtistName);
            Assert.AreEqual(expectedArtistNameTrimed, album.ArtistNameTrimed);
            Assert.AreEqual(expectedIsLeadArtist, album.IsLeadArtist);
            Assert.AreEqual(expectedAlbumName, album.AlbumName);
            Assert.AreEqual(expectedAlbumYear, album.AlbumYear);
            Assert.AreEqual(expectedBassAlbumCode, album.BassAlbumCode);
            Assert.AreEqual(expectedIsBassTab, album.IsBassTab);
            Assert.AreEqual(expectedIsNewAlbum, album.IsNewAlbum);
            Assert.AreEqual(expectedIsMiscCollectionAlbum, album.IsMiscCollectionAlbum);
            Assert.AreEqual(expectedIncludeInIndex, album.IncludeInIndex);
            Assert.AreEqual(expectedIncludeOnWebsite, album.IncludeOnWebsite);
            Assert.AreEqual(expectedAverageRating, album.AverageRating);
        }
    }
}