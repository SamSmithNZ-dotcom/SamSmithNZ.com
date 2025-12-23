using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using SamSmithnNZ.Tests;
using SamSmithNZ.Service.Controllers.GuitarTab;
using SamSmithNZ.Service.DataAccess.GuitarTab;
using SamSmithNZ.Service.DataAccess.GuitarTab.Interfaces;
using SamSmithNZ.Service.Models.GuitarTab;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Tests.GuitarTab
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class AlbumDataAccessTest : BaseIntegrationTest
    {

        [TestMethod()]
        public async Task AlbumsExistTest()
        {
            //arrange
            IAlbumDataAccess mock = Substitute.For<IAlbumDataAccess>();
            mock.GetList(Arg.Any<bool>()).Returns(Task.FromResult(GetAlbumsTestData()));
            AlbumController controller = new(mock);

            //act
            List<Album> results = await controller.GetAlbums(true);

            //assert
            Assert.IsNotNull(results);
            Assert.IsNotEmpty(results);
        }

        [TestMethod()]
        public async Task AlbumsFirstItemTest()
        {
            //arrange
            AlbumController controller = new(new AlbumDataAccess(base.Configuration));
            int albumCode = 14; //The Colour And The Shape

            //act
            Album results = await controller.GetAlbum(albumCode, true);

            //assert
            TestTCATS(results);
        }

        [TestMethod()]
        public async Task AlbumsTCATSTest()
        {
            //arrange
            AlbumController controller = new(new AlbumDataAccess(base.Configuration));
            int albumCode = 14; //The Colour And The Shape

            //act
            List<Album> results = await controller.GetAlbums(true);

            //assert
            bool foundTCATS = false;
            foreach (Album result in results)
            {
                if (result.AlbumCode == albumCode)
                {
                    TestTCATS(result);
                    foundTCATS = true;
                    break;
                }
            }
            Assert.IsTrue(foundTCATS);
        }

        private static void TestTCATS(Album results)
        {
            Assert.IsNotNull(results);
            Assert.AreEqual(14, results.AlbumCode);
            Assert.AreEqual("The Colour And The Shape", results.AlbumName);
            Assert.AreEqual(1997, results.AlbumYear);
            Assert.AreEqual("Foo Fighters", results.ArtistName);
            Assert.AreEqual("FooFighters", results.ArtistNameTrimed);
            Assert.AreEqual(5, results.AverageRating);
            Assert.IsTrue(results.IncludeInIndex);
            Assert.IsTrue(results.IncludeOnWebsite);
            Assert.IsFalse(results.IsBassTab);
            Assert.IsFalse(results.IsMiscCollectionAlbum);
            Assert.IsFalse(results.IsNewAlbum);
            Assert.IsFalse(results.IsLeadArtist);
            Assert.AreEqual(102, results.BassAlbumCode);
        }

        [TestMethod()]
        public async Task AlbumsSaveTest()
        {
            //arrange
            AlbumController controller = new(new AlbumDataAccess(base.Configuration));
            int albumCode = 242;

            //act
            Album item = await controller.GetAlbum(albumCode, true);

            //update
            string albumName;
            string artistName;
            if (item.AlbumName == "Test Album1")
            {
                albumName = "Test Album2";
                artistName = "Test Artist2";
            }
            else
            {
                albumName = "Test Album1";
                artistName = "Test Artist1";
            }
            item.AlbumName = albumName;
            item.ArtistName = artistName;
            await controller.SaveAlbum(item);

            //reload
            item = await controller.GetAlbum(albumCode, true);

            //assert
            Assert.IsNotNull(item);
            Assert.AreEqual(242, item.AlbumCode);
            Assert.AreEqual(albumName, item.AlbumName);
            Assert.AreEqual(2014, item.AlbumYear);
            Assert.AreEqual(artistName, item.ArtistName);
            Assert.AreEqual(artistName.Replace(" ", ""), item.ArtistNameTrimed);
            Assert.AreEqual(0, item.AverageRating);
            Assert.IsFalse(item.IncludeInIndex);
            Assert.IsFalse(item.IncludeOnWebsite);
            Assert.IsFalse(item.IsBassTab);
            Assert.IsFalse(item.IsMiscCollectionAlbum);
            Assert.IsFalse(item.IsNewAlbum);
        }

        private static List<Album> GetAlbumsTestData()
        {
            return new() {
                new Album{
                    AlbumCode = 14,
                    AlbumName = "The Colour And The Shape",
                    ArtistName = "Foo Fighters",
                    ArtistNameTrimed = "FooFighters",
                    AlbumYear = 1997,
                    IncludeInIndex = true,
                    IncludeOnWebsite = true,
                    IsBassTab = false,
                    IsMiscCollectionAlbum = false,
                    IsNewAlbum = false,
                    IsLeadArtist = false,
                    AverageRating = 5M,
                    BassAlbumCode = 102
                } 
            };
        }
    }
}