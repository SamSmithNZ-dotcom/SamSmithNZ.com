using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithNZ.Tests;
using SamSmithNZ.Service.Controllers.FooFighters;
using SamSmithNZ.Service.DataAccess.FooFighters;
using SamSmithNZ.Service.Models.FooFighters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Tests.FooFighters
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class SongTest:BaseIntegrationTest
    {

        [TestMethod()]
        public async Task SongsExistTest()
        {
            //arrange
            SongController controller = new(new SongDataAccess(base.Configuration));

            //act
            List<Song> items = await controller.GetSongs();

            //assert
            Assert.IsTrue(items != null);
            Assert.IsTrue(items.Count > 0);
        }

        [TestMethod()]
        public async Task GetListForShowAsyncTest()
        {
            //arrange
            SongDataAccess da = new(base.Configuration);
            int showCode = 3;

            //act
            List<Song> results = await da.GetListForShowAsync(showCode);

            //assert
            Assert.IsNotNull(results);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod()]
        public async Task GetItemTest()
        {
            //arrange
            SongDataAccess da = new(base.Configuration);
            int songCode = 1;

            //act
            Song result = await da.GetItem(songCode);

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.SongCode);
        }

        [TestMethod()]
        public async Task SongThisIsACallTest()
        {
            //arrange
            SongController controller = new(new SongDataAccess(base.Configuration));
            int songKey = 1;

            //act
            Song result = await controller.GetSong(songKey);

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.AlbumCode);
            Assert.AreEqual("Foo Fighters", result.AlbumName);
            Assert.IsTrue(result.FirstPlayed >= DateTime.MinValue);
            Assert.AreEqual(3, result.FirstPlayedShowCode);
            Assert.IsTrue(result.LastPlayed >= DateTime.MinValue);
            Assert.IsTrue(result.LastPlayedShowCode > 0);
            Assert.AreEqual("images/thisisacall.jpg", result.SongImage);
            Assert.AreEqual(1, result.SongCode);
            Assert.IsFalse(string.IsNullOrEmpty(result.SongLyrics));
            Assert.AreEqual("This is a Call", result.SongName);
            Assert.IsTrue(string.IsNullOrEmpty(result.SongNotes));
            Assert.AreEqual(1, result.SongOrder);
            Assert.IsTrue(result.TimesPlayed > 0);
        }

        [TestMethod()]
        public async Task SomeThingFromNothingNullDatesTest()
        {
            //arrange
            SongController controller = new(new SongDataAccess(base.Configuration));
            int songKey = 318;

            //act
            Song result = await controller.GetSong(songKey);

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(14, result.AlbumCode);
            Assert.AreEqual("Sonic Highways", result.AlbumName);
            Assert.IsNotNull(result.FirstPlayed);
            Assert.IsNotNull(result.FirstPlayedShowCode);
            Assert.IsNotNull(result.LastPlayed);
            Assert.IsNotNull(result.LastPlayedShowCode);
            Assert.IsNull(result.SongImage);
            Assert.AreEqual(318, result.SongCode);
            Assert.IsTrue(string.IsNullOrEmpty(result.SongLyrics));
            Assert.AreEqual("Something From Nothing", result.SongName);
            Assert.IsTrue(string.IsNullOrEmpty(result.SongNotes));
            Assert.AreEqual(1, result.SongOrder);
            Assert.IsTrue(result.TimesPlayed > 0);
        }

        [TestMethod()]
        public async Task SongsForFooFightersAlbumTest()
        {
            //arrange
            SongController controller = new(new SongDataAccess(base.Configuration));
            int albumKey = 1;

            //act
            List<Song> items = await controller.GetSongsByAlbum(albumKey);

            //assert
            Assert.IsNotNull(items);
            Assert.IsTrue(items.Count > 0);
            Assert.AreEqual(1, items[0].AlbumCode);
            Assert.AreEqual("Foo Fighters", items[0].AlbumName);
            Assert.IsTrue(items[0].FirstPlayed >= DateTime.MinValue);
            Assert.AreEqual(3, items[0].FirstPlayedShowCode);
            Assert.IsTrue(items[0].LastPlayed >= DateTime.MinValue);
            Assert.IsTrue(items[0].LastPlayedShowCode > 0);
            Assert.AreEqual("images/thisisacall.jpg", items[0].SongImage);
            Assert.AreEqual(1, items[0].SongCode);
            Assert.IsFalse(string.IsNullOrEmpty(items[0].SongLyrics));
            Assert.AreEqual("This is a Call", items[0].SongName);
            Assert.IsTrue(string.IsNullOrEmpty(items[0].SongNotes));
            Assert.AreEqual(1, items[0].SongOrder);
            Assert.IsTrue(items[0].TimesPlayed > 0);
        }

        [TestMethod()]
        public async Task SongShowTest()
        {
            //arrange
            SongController controller = new(new SongDataAccess(base.Configuration));
            int showKey = 3;

            //act
            List<Song> items = await controller.GetSongsByShow(showKey);

            //assert
            Assert.IsNotNull(items);
            Assert.IsNotEmpty(items);
            Assert.AreEqual(1, items[2].AlbumCode);
            Assert.AreEqual("Foo Fighters", items[2].AlbumName);
            Assert.IsTrue(items[2].FirstPlayed >= DateTime.MinValue);
            Assert.AreEqual(3, items[2].FirstPlayedShowCode);
            Assert.IsTrue(items[2].LastPlayed >= DateTime.MinValue);
            Assert.IsTrue(items[2].LastPlayedShowCode > 0);
            Assert.AreEqual("images/thisisacall.jpg", items[2].SongImage);
            Assert.AreEqual(1, items[2].SongCode);
            Assert.IsFalse(string.IsNullOrEmpty(items[2].SongLyrics));
            Assert.AreEqual("This is a Call", items[2].SongName);
            Assert.IsTrue(string.IsNullOrEmpty(items[2].SongNotes));
            Assert.AreEqual(3, items[2].SongOrder);
            Assert.IsGreaterThan(0, items[2].TimesPlayed);
        }
    }
}