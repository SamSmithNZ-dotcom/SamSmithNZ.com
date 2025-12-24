using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithnNZ.Tests;
using SamSmithNZ.Service.Controllers.GuitarTab;
using SamSmithNZ.Service.DataAccess.GuitarTab;
using SamSmithNZ.Service.Models.GuitarTab;
using System.Threading.Tasks;

namespace SamSmithNZ.Tests.GuitarTab
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class TabDataAccessTest : BaseIntegrationTest
    {

        //1. Tabs exist
        [TestMethod()]
        public async Task TabsExistTest()
        {
            //arrange
            TabController controller = new(new TabDataAccess(base.Configuration));
            int albumCode = 14;
            int sortOrder = 0; //order by track order

            //act
            List<Tab> results = await controller.GetTabs(albumCode, sortOrder); 

            //assert
            Assert.IsNotNull(results);
            Assert.IsNotEmpty(results);
        }

        //3. Get tab 500/Everlong
        [TestMethod()]
        public async Task TabFirstItemTest()
        {
            //arrange
            TabController controller = new(new TabDataAccess(base.Configuration));
            int tabCode = 500;

            //act
            Tab results = await controller.GetTab(tabCode); //Tab code of 500/Everlong

            //assert
            Assert.IsNotNull(results);
            Assert.AreEqual(14, results.AlbumCode);
            Assert.AreEqual(5, results.Rating);
            Assert.AreEqual(500, results.TabCode);
            Assert.AreEqual("Everlong", results.TabName);
            Assert.AreEqual("Everlong", results.TabNameTrimed);
            Assert.AreEqual(11, results.TabOrder);
            Assert.AreEqual(7477, results.TabText.Length);
            Assert.AreEqual(2, results.TuningCode);
            Assert.AreEqual("Drop D Tuning", results.TuningName);
            Assert.IsTrue(results.LastUpdated > DateTime.MinValue);
        }

        //3. Get tab 0/null
        [TestMethod()]
        public async Task Tab0ItemTest()
        {
            //arrange
            TabController controller = new(new TabDataAccess(base.Configuration));
            int tabCode = 0;

            //act
            Tab results = await controller.GetTab(tabCode); //Tab code of 0/nothing

            //assert
            Assert.IsNull(results);
        }

        //Test album 14, sorted by track
        [TestMethod()]
        public async Task TabAlbumSortedbyTrackOrderTest()
        {
            //arrange
            TabController controller = new(new TabDataAccess(base.Configuration));
            int albumCode = 14;
            int sortOrder = 0; //order by track order

            //act
            List<Tab> results = await controller.GetTabs(albumCode, sortOrder); 

            //assert
            Assert.IsNotNull(results);
            Assert.AreEqual(13, results.Count);
            int i = 0;
            foreach (Tab result in results)
            {
                i++;
                if (result.TabCode == 500)
                {
                    Assert.AreEqual(11, i);
                    Assert.AreEqual(14, result.AlbumCode);
                    Assert.AreEqual(5, result.Rating);
                    Assert.AreEqual(500, result.TabCode);
                    Assert.AreEqual("Everlong", result.TabName);
                    Assert.AreEqual("Everlong", result.TabNameTrimed);
                    Assert.AreEqual(11, result.TabOrder);
                    Assert.AreEqual(7477, result.TabText.Length);
                    Assert.AreEqual(2, result.TuningCode);
                    Assert.AreEqual("Drop D Tuning", result.TuningName);
                    Assert.IsTrue(result.LastUpdated > DateTime.MinValue);
                    break;
                }
            }

        }

        //Test album 14
        [TestMethod()]
        public async Task TabAlbumSortedbyTuningTest()
        {
            //arrange
            TabController controller = new(new TabDataAccess(base.Configuration));
            int albumCode = 14;
            int sortOrder = 1; //order by tuning

            //act
            List<Tab> results = await controller.GetTabs(albumCode, sortOrder);

            //assert
            int i = 0;
            foreach (Tab result in results)
            {
                i++;
                if (result.TabCode == 500)
                {
                    Assert.AreEqual(5, i); //Is 5th in the tab order when sorting by tuning
                    Assert.AreEqual(14, result.AlbumCode);
                    Assert.AreEqual(5, result.Rating);
                    Assert.AreEqual(500, result.TabCode);
                    Assert.AreEqual("Everlong", result.TabName);
                    Assert.AreEqual("Everlong", result.TabNameTrimed);
                    Assert.AreEqual(11, result.TabOrder);
                    Assert.AreEqual(7477, result.TabText.Length);
                    Assert.AreEqual(2, result.TuningCode);
                    Assert.AreEqual("Drop D Tuning", result.TuningName);
                    Assert.IsTrue(result.LastUpdated > DateTime.MinValue);
                    break;
                }
            }

        }

        [TestMethod()]
        public async Task TabSaveAndDeleteTest()
        {
            //arrange
            TabController controller = new(new TabDataAccess(base.Configuration));
            int albumCode = 246;
            int sortOrder = 0;
            Tab newTab = new();
            newTab.TabCode = 0;
            newTab.AlbumCode = albumCode;
            newTab.TabName = "Test track 14";
            newTab.TabText = "Test track text 14";

            //act part 1: create the track
            bool result = await controller.SaveTab(newTab);

            //assert part 1: check the track was created
            Assert.IsTrue(result);

            //act part 2: get the tracks for the album
            List<Tab> results = await controller.GetTabs(albumCode, sortOrder);

            //assert part 2: check that the track is correct
            Assert.IsNotNull(results);
            Assert.IsNotEmpty(results);
            Assert.IsGreaterThan(0, results[0].TabCode);
            Assert.AreEqual(albumCode, results[0].AlbumCode);
            Assert.AreEqual("Test track 14", results[0].TabName);
            Assert.AreEqual("Testtrack14", results[0].TabNameTrimed);
            Assert.AreEqual("Test track text 14", results[0].TabText);
            Assert.IsTrue(results[0].LastUpdated > DateTime.MinValue);

            //act part 3: delete the tracks
            foreach (Tab item in results)
            {
                await controller.DeleteTab(item.TabCode);
            }

            //act part 4: get the tracks for the album
            results = await controller.GetTabs(albumCode, sortOrder);

            //assert part 4: check that the tracks have all been deleted
            Assert.IsNotNull(results);
            Assert.IsEmpty(results);
        }

    }
}


