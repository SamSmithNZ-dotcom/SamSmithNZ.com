﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSNZ.GuitarTab.Data;
using SSNZ.GuitarTab.Models;
using System.Threading.Tasks;

namespace SSNZ.GuitarTab.UnitTests
{
    [TestClass]
    public class SearchDataAccessTest
    {

        //1. Search items exist
        [TestMethod()]
        public async Task SearchTabItemsExistTest()
        {
            //arrange
            SearchDataAccess da = new SearchDataAccess();
            Guid recordId = new Guid("7075FE23-B1AC-4169-AC87-D78F1E66BACB");

            //act
            List<Search> results = await da.GetDataAsync(recordId);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count == 2);
        }

        //3. Specific Search Tab 14
        [TestMethod()]
        public async Task SearchTabFirstItemTest()
        {
            //arrange
            SearchDataAccess da = new SearchDataAccess();
            Guid recordId = new Guid("7075FE23-B1AC-4169-AC87-D78F1E66BACB");

            //act
            List<Search> results = await da.GetDataAsync(recordId);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count >= 0);
            Assert.IsTrue(results[0] != null);
            Assert.IsTrue(results[0].AlbumCode == 14);
            Assert.IsTrue(results[0].ArtistAlbumResult == "Foo Fighters - The Colour And The Shape");
            Assert.IsTrue(results[0].IsBassTab == false);
            Assert.IsTrue(results[0].TrackName == "Everlong");
            Assert.IsTrue(results[0].TrackResult == "11. Everlong");
        }

    }
}


