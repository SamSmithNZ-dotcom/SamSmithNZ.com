using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithnNZ.Tests;
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

        //[TestMethod()]
        //public async Task SongsExistTest()
        //{
        //    //arrange
        //    SongController controller = new(new SongDataAccess(base.Configuration));

        //    //act
        //    List<Song> items = await controller.GetSongs();

        //    //assert
        //    Assert.IsTrue(items != null);
        //    Assert.IsTrue(items.Count > 0);
        //}

        [TestMethod()]
        public async Task SongThisIsACallTest()
        {
            //arrange
            SongController controller = new(new SongDataAccess(base.Configuration));
            int songKey = 1;

            //act
            Song result = await controller.GetSong(songKey);

            //assert
            Assert.IsTrue(result != null);
            Assert.IsTrue(result.AlbumCode == 1);
            Assert.IsTrue(result.AlbumName == "Foo Fighters");
            Assert.IsTrue(result.FirstPlayed >= DateTime.MinValue);
            Assert.IsTrue(result.FirstPlayedShowCode == 3);
            Assert.IsTrue(result.LastPlayed >= DateTime.MinValue);
            Assert.IsTrue(result.LastPlayedShowCode > 0);
            Assert.IsTrue(result.SongImage == "images/thisisacall.jpg");
            Assert.IsTrue(result.SongCode == 1);
            Assert.IsTrue(string.IsNullOrEmpty(result.SongLyrics) == false);
            Assert.IsTrue(result.SongName == "This is a Call");
            Assert.IsTrue(string.IsNullOrEmpty(result.SongNotes) == true);
            Assert.IsTrue(result.SongOrder == 1);
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
            Assert.IsTrue(result != null);
            Assert.IsTrue(result.AlbumCode == 14);
            Assert.IsTrue(result.AlbumName == "Sonic Highways");
            Assert.IsTrue(result.FirstPlayed != null);
            Assert.IsTrue(result.FirstPlayedShowCode != null);
            Assert.IsTrue(result.LastPlayed != null);
            Assert.IsTrue(result.LastPlayedShowCode != null);
            Assert.IsTrue(result.SongImage == null);
            Assert.IsTrue(result.SongCode == 318);
            Assert.IsTrue(string.IsNullOrEmpty(result.SongLyrics) == true);
            Assert.IsTrue(result.SongName == "Something From Nothing");
            Assert.IsTrue(string.IsNullOrEmpty(result.SongNotes) == true);
            Assert.IsTrue(result.SongOrder == 1);
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
            Assert.IsTrue(items != null);
            Assert.IsTrue(items.Count > 0);
            Assert.IsTrue(items[0].AlbumCode == 1);
            Assert.IsTrue(items[0].AlbumName == "Foo Fighters");
            Assert.IsTrue(items[0].FirstPlayed >= DateTime.MinValue);
            Assert.IsTrue(items[0].FirstPlayedShowCode == 3);
            Assert.IsTrue(items[0].LastPlayed >= DateTime.MinValue);
            Assert.IsTrue(items[0].LastPlayedShowCode > 0);
            Assert.IsTrue(items[0].SongImage == "images/thisisacall.jpg");
            Assert.IsTrue(items[0].SongCode == 1);
            Assert.IsTrue(string.IsNullOrEmpty(items[0].SongLyrics) == false);
            Assert.IsTrue(items[0].SongName == "This is a Call");
            Assert.IsTrue(string.IsNullOrEmpty(items[0].SongNotes) == true);
            Assert.IsTrue(items[0].SongOrder == 1);
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
            Assert.IsTrue(items != null);
            Assert.IsTrue(items.Count > 0);
            Assert.IsTrue(items[2].AlbumCode == 1);
            Assert.IsTrue(items[2].AlbumName == "Foo Fighters");
            Assert.IsTrue(items[2].FirstPlayed >= DateTime.MinValue);
            Assert.IsTrue(items[2].FirstPlayedShowCode == 3);
            Assert.IsTrue(items[2].LastPlayed >= DateTime.MinValue);
            Assert.IsTrue(items[2].LastPlayedShowCode > 0);
            Assert.IsTrue(items[2].SongImage == "images/thisisacall.jpg");
            Assert.IsTrue(items[2].SongCode == 1);
            Assert.IsTrue(string.IsNullOrEmpty(items[2].SongLyrics) == false);
            Assert.IsTrue(items[2].SongName == "This is a Call");
            Assert.IsTrue(string.IsNullOrEmpty(items[2].SongNotes) == true);
            Assert.IsTrue(items[2].SongOrder == 3);
            Assert.IsTrue(items[2].TimesPlayed > 0);
        }
    }
}