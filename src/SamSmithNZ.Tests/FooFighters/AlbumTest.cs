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
    public class AlbumTest : BaseIntegrationTest
    {

        [TestMethod()]
        public async Task AlbumsExistTest()
        {
            //arrange
            AlbumController controller = new(new AlbumDataAccess(base.Configuration));

            //act
            List<Album> items = await controller.GetAlbums(); 

            //assert
            Assert.IsNotNull(items);
            Assert.IsNotEmpty(items); //There is more than one
            Assert.IsGreaterThan(0, items[0].AlbumCode); //The first item has an id
            Assert.IsGreaterThan(0, items[0].AlbumName.Length); //The first item has an name
        }

        [TestMethod()]
        public async Task AlbumsGetFooFightersTest()
        {
            //arrange
            AlbumController controller = new(new AlbumDataAccess(base.Configuration));
            int albumKey = 1;

            //act
            Album item = await controller.GetAlbum(albumKey);

            //assert
            Assert.IsNotNull(item);
            Assert.AreEqual("220px-FooFighters-FooFighters.jpg", item.AlbumImage);
            Assert.AreEqual(1, item.AlbumCode);
            Assert.AreEqual("Foo Fighters", item.AlbumName);
            Assert.IsTrue(item.AlbumReleaseDate >= DateTime.MinValue);
        }

    }
}