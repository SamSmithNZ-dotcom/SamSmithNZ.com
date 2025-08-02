using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithNZ.Service.Models.ITunes;
using System;

namespace SamSmithNZ.Tests.Models.ITunes
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class PlaylistTests
    {
        [TestMethod]
        public void Playlist_DefaultConstructor_CreatesInstance()
        {
            // Act
            Playlist playlist = new Playlist();

            // Assert
            Assert.IsNotNull(playlist);
            Assert.AreEqual(0, playlist.PlaylistCode);
            Assert.AreEqual(DateTime.MinValue, playlist.PlaylistDate);
        }

        [TestMethod]
        public void Playlist_SetAndGetPlaylistCode_ReturnsCorrectValue()
        {
            // Arrange
            Playlist playlist = new Playlist();
            var expectedPlaylistCode = 123;

            // Act
            playlist.PlaylistCode = expectedPlaylistCode;

            // Assert
            Assert.AreEqual(expectedPlaylistCode, playlist.PlaylistCode);
        }

        [TestMethod]
        public void Playlist_SetAndGetPlaylistDate_ReturnsCorrectValue()
        {
            // Arrange
            Playlist playlist = new Playlist();
            var expectedPlaylistDate = new DateTime(2023, 12, 25);

            // Act
            playlist.PlaylistDate = expectedPlaylistDate;

            // Assert
            Assert.AreEqual(expectedPlaylistDate, playlist.PlaylistDate);
        }

        [TestMethod]
        public void Playlist_InitializeWithObjectInitializer_ReturnsAllValues()
        {
            // Arrange
            var expectedPlaylistCode = 456;
            var expectedPlaylistDate = new DateTime(2024, 1, 1);

            // Act
            Playlist playlist = new Playlist
            {
                PlaylistCode = expectedPlaylistCode,
                PlaylistDate = expectedPlaylistDate
            };

            // Assert
            Assert.AreEqual(expectedPlaylistCode, playlist.PlaylistCode);
            Assert.AreEqual(expectedPlaylistDate, playlist.PlaylistDate);
        }
    }
}