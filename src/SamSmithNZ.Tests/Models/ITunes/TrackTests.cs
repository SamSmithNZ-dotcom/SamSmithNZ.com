using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithNZ.Service.Models.ITunes;
using System;

namespace SamSmithNZ.Tests.Models.ITunes
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class TrackTests
    {
        [TestMethod]
        public void Track_DefaultConstructor_CreatesInstance()
        {
            // Act
            Track track = new Track();

            // Assert
            Assert.IsNotNull(track);
            Assert.AreEqual(0, track.PlaylistCode);
            Assert.IsNull(track.TrackName);
            Assert.IsNull(track.AlbumName);
            Assert.IsNull(track.ArtistName);
            Assert.AreEqual(0, track.PlayCount);
            Assert.AreEqual(0, track.PreviousPlayCount);
            Assert.AreEqual(0, track.Ranking);
            Assert.AreEqual(0, track.PreviousRanking);
            Assert.IsFalse(track.IsNewEntry);
            Assert.AreEqual(0, track.Rating);
            Assert.AreEqual(Guid.Empty, track.RecordId);
        }

        [TestMethod]
        public void Track_SetAllProperties_ReturnsAllValues()
        {
            // Arrange
            Track track = new Track();
            var expectedPlaylistCode = 1;
            var expectedTrackName = "Bohemian Rhapsody";
            var expectedAlbumName = "A Night at the Opera";
            var expectedArtistName = "Queen";
            var expectedPlayCount = 100;
            var expectedPreviousPlayCount = 90;
            var expectedRanking = 1;
            var expectedPreviousRanking = 2;
            var expectedIsNewEntry = true;
            var expectedRating = 5;
            var expectedRecordId = Guid.NewGuid();

            // Act
            track.PlaylistCode = expectedPlaylistCode;
            track.TrackName = expectedTrackName;
            track.AlbumName = expectedAlbumName;
            track.ArtistName = expectedArtistName;
            track.PlayCount = expectedPlayCount;
            track.PreviousPlayCount = expectedPreviousPlayCount;
            track.Ranking = expectedRanking;
            track.PreviousRanking = expectedPreviousRanking;
            track.IsNewEntry = expectedIsNewEntry;
            track.Rating = expectedRating;
            track.RecordId = expectedRecordId;

            // Assert
            Assert.AreEqual(expectedPlaylistCode, track.PlaylistCode);
            Assert.AreEqual(expectedTrackName, track.TrackName);
            Assert.AreEqual(expectedAlbumName, track.AlbumName);
            Assert.AreEqual(expectedArtistName, track.ArtistName);
            Assert.AreEqual(expectedPlayCount, track.PlayCount);
            Assert.AreEqual(expectedPreviousPlayCount, track.PreviousPlayCount);
            Assert.AreEqual(expectedRanking, track.Ranking);
            Assert.AreEqual(expectedPreviousRanking, track.PreviousRanking);
            Assert.AreEqual(expectedIsNewEntry, track.IsNewEntry);
            Assert.AreEqual(expectedRating, track.Rating);
            Assert.AreEqual(expectedRecordId, track.RecordId);
        }

        [TestMethod]
        public void Track_InitializeWithObjectInitializer_ReturnsAllValues()
        {
            // Arrange
            var expectedPlaylistCode = 2;
            var expectedTrackName = "Stairway to Heaven";
            var expectedAlbumName = "Led Zeppelin IV";
            var expectedArtistName = "Led Zeppelin";
            var expectedPlayCount = 200;
            var expectedPreviousPlayCount = 180;
            var expectedRanking = 1;
            var expectedPreviousRanking = 1;
            var expectedIsNewEntry = false;
            var expectedRating = 5;
            var expectedRecordId = Guid.NewGuid();

            // Act
            Track track = new Track
            {
                PlaylistCode = expectedPlaylistCode,
                TrackName = expectedTrackName,
                AlbumName = expectedAlbumName,
                ArtistName = expectedArtistName,
                PlayCount = expectedPlayCount,
                PreviousPlayCount = expectedPreviousPlayCount,
                Ranking = expectedRanking,
                PreviousRanking = expectedPreviousRanking,
                IsNewEntry = expectedIsNewEntry,
                Rating = expectedRating,
                RecordId = expectedRecordId
            };

            // Assert
            Assert.AreEqual(expectedPlaylistCode, track.PlaylistCode);
            Assert.AreEqual(expectedTrackName, track.TrackName);
            Assert.AreEqual(expectedAlbumName, track.AlbumName);
            Assert.AreEqual(expectedArtistName, track.ArtistName);
            Assert.AreEqual(expectedPlayCount, track.PlayCount);
            Assert.AreEqual(expectedPreviousPlayCount, track.PreviousPlayCount);
            Assert.AreEqual(expectedRanking, track.Ranking);
            Assert.AreEqual(expectedPreviousRanking, track.PreviousRanking);
            Assert.AreEqual(expectedIsNewEntry, track.IsNewEntry);
            Assert.AreEqual(expectedRating, track.Rating);
            Assert.AreEqual(expectedRecordId, track.RecordId);
        }
    }
}