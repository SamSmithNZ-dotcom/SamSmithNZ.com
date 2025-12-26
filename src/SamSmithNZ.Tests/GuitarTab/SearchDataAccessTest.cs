using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithNZ.Tests;
using SamSmithNZ.Service.Controllers.GuitarTab;
using SamSmithNZ.Service.DataAccess.GuitarTab;
using SamSmithNZ.Service.Models.GuitarTab;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Tests.GuitarTab
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class SearchDataAccessTest : BaseIntegrationTest
    {

        //1. Search items exist
        [TestMethod()]
        public async Task SearchTabItemsExistTest()
        {
            //arrange
            SearchController controller = new(new SearchDataAccess(base.Configuration));
            string searchText = "";
            Guid recordId = new("7075FE23-B1AC-4169-AC87-D78F1E66BACB");

            //act
            List<Search> results = await controller.GetSearchResults(searchText, recordId);

            //assert
            Assert.IsNotNull(results);
            Assert.HasCount(2, results);
        }

        //3. Specific Search Tab 14
        [TestMethod()]
        public async Task SearchTabFirstItemTest()
        {
            //arrange
            SearchController controller = new(new SearchDataAccess(base.Configuration));
            string searchText = "";
            Guid recordId = new("7075FE23-B1AC-4169-AC87-D78F1E66BACB");

            //act
            List<Search> results = await controller.GetSearchResults(searchText, recordId);

            //assert
            Assert.IsNotNull(results);
            Assert.IsGreaterThanOrEqualTo(0, results.Count);
            Assert.IsNotNull(results[0]);
            Assert.AreEqual(14, results[0].AlbumCode);
            Assert.AreEqual("Foo Fighters - The Colour And The Shape", results[0].ArtistAlbumResult);
            Assert.IsFalse(results[0].IsBassTab);
            Assert.AreEqual("Everlong", results[0].TrackName);
            Assert.AreEqual("11. Everlong", results[0].TrackResult);
            Assert.AreEqual("Everlong", results[0].SearchText);

        }

        [TestMethod()]
        public async Task SearchBigTest()
        {
            //arrange
            SearchController controller = new(new SearchDataAccess(base.Configuration));
            string searchText = "home";

            //act 
            List<Search> results = await controller.GetSearchResults(searchText);

            //assert 
            Assert.IsNotNull(results);
            Assert.IsGreaterThanOrEqualTo(19, results.Count);
            Assert.IsNotNull(results[0]);
            Assert.AreEqual(168, results[0].AlbumCode);
            Assert.AreEqual("Cure - Disintegration", results[0].ArtistAlbumResult);
            Assert.IsFalse(results[0].IsBassTab);
            Assert.AreEqual("Homesick", results[0].TrackName);
            Assert.AreEqual("11. Homesick", results[0].TrackResult);
            Assert.AreEqual("home", results[0].SearchText);
            Assert.IsNotNull(results[1]);
            Assert.AreEqual(203, results[1].AlbumCode);
            Assert.AreEqual("Foo Fighters - Echoes, Silence, Patience And Grace", results[1].ArtistAlbumResult);
            Assert.IsFalse(results[1].IsBassTab);
            Assert.AreEqual("Home", results[1].TrackName);
            Assert.AreEqual("12. Home", results[1].TrackResult);
            Assert.AreEqual("home", results[1].SearchText);
        }

        [TestMethod()]
        public async Task SearchEncodedTextTest()
        {
            //arrange
            SearchController controller = new(new SearchDataAccess(base.Configuration));
            string searchText = "<foo>";

            //act 
            List<Search> results = await controller.GetSearchResults(searchText);

            //assert 
            Assert.IsNotNull(results);
            Assert.IsEmpty(results);
        }

    }
}


