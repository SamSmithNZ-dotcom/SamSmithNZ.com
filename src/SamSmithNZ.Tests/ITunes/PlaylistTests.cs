using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithNZ.Tests;
using SamSmithNZ.Service.DataAccess.ITunes;
using SamSmithNZ.Service.Models.ITunes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Tests.ITunes
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class PlaylistTests : BaseIntegrationTest
    {
        [TestMethod]
        public async Task PlayListTest()
        {
            //Arrange
            PlaylistDataAccess da = new(base.Configuration);

            //Act
            List<Playlist> items = await da.GetList();

            //Assert
            Assert.IsTrue(items != null);
            Assert.IsTrue(items.Count > 0);
            Assert.IsTrue(items[^1].PlaylistCode == 1);
            Assert.IsTrue(items[^1].PlaylistDate.Year == 2006);
            Assert.IsTrue(items[^1].PlaylistDate.Month == 8);
        }

        [TestMethod]
        public async Task PlayListItemTest()
        {
            //Arrange
            PlaylistDataAccess da = new(base.Configuration);
            int playlistCode = 1;
            //Act
            Playlist item = await da.GetItem(playlistCode);

            //Assert
            Assert.IsTrue(item != null);
            Assert.IsTrue(item.PlaylistCode == 1);
            Assert.IsTrue(item.PlaylistDate.Year == 2006);
            Assert.IsTrue(item.PlaylistDate.Month == 8);
        }

        //[TestMethod]
        //public async Task BigComplicatedPlaylistTest()
        //{
        //    PlaylistDataAccess da = new(base.Configuration);
        //    DateTime newDate = new DateTime(2020, 1, 1);

        //    //Act
        //    //delete the playlist if it exists - this is essentially a special cleanup step if the last step failed.
        //    List<Playlist> items = await da.GetList();
        //    Playlist item = GetTestPlaylist(newDate, items);
        //    if (item != null)
        //    {
        //        await da.DeleteItem(item.PlaylistCode);
        //    }

        //    //add the new playlist
        //    await da.SaveItem(newDate);
        //    items = await da.GetList();
        //    item = GetTestPlaylist(newDate, items);

        //    //Run the special track functions
        //    TrackDataAccess da2 = new(base.Configuration);

        //    Track track = new Track
        //    {
        //        PlaylistCode = item.PlaylistCode,
        //        AlbumName = "Test album",
        //        ArtistName = "Test artist",
        //        RecordId = Guid.NewGuid(),
        //        TrackName = "Test track name"
        //    };
        //    await da2.SaveItem(track);
        //    await da2.SetTrackRanksForPlaylist(item.PlaylistCode);
        //    await da2.ValidateTracksForPlaylist(item.PlaylistCode);

        //    //Clean up and delete the playlist
        //    items = await da.GetList();
        //    item = GetTestPlaylist(newDate, items);
        //    if (item != null)
        //    {
        //        await da.DeleteItem(item.PlaylistCode);
        //    }

        //    //No Asserts. Just run the code and don't crash...
        //}

        //private static Playlist GetTestPlaylist(DateTime newDate, List<Playlist> items)
        //{
        //    foreach (Playlist item in items)
        //    {
        //        if (item.PlaylistDate == newDate)
        //        {
        //            return item;
        //        }
        //    }
        //    return null;
        //}
    }
}
