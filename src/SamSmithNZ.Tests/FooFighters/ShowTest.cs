using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithNZ.Service.DataAccess.FooFighters;
using SamSmithNZ.Service.Models.FooFighters;
using System.Threading.Tasks;
using SamSmithNZ.Service.Controllers.FooFighters;
using SamSmithnNZ.Tests;

namespace SamSmithNZ.Tests.FooFighters
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class ShowTest : BaseIntegrationTest
    {

        [TestMethod()]
        public async Task ShowsExistForSongTest()
        {
            //arrange
            ShowController controller = new(new ShowDataAccess(base.Configuration));
            int songKey = 1;

            //act
            List<Show> items = await controller.GetShowsBySong(songKey);

            //assert
            Assert.IsNotNull(items);
            Assert.IsTrue(items.Count > 0);
        }

        [TestMethod()]
        public async Task ShowsForSongTest()
        {
            //arrange
            ShowController controller = new(new ShowDataAccess(base.Configuration));
            int songKey = 1;

            //act
            List<Show> items = await controller.GetShowsBySong(songKey);

            //assert
            Assert.IsNotNull(items);
            Assert.IsTrue(items.Count > 0);
            Assert.AreNotEqual("", items[0].Notes);
            Assert.IsTrue(items[0].NumberOfSongsPlayed >= 0);
            Assert.AreEqual("Portland, OR", items[0].ShowCity);
            Assert.IsNull(items[0].ShowCountry);
            Assert.IsTrue(items[0].ShowDate >= DateTime.MinValue);
            Assert.AreEqual(3, items[0].ShowCode);
            Assert.AreNotEqual("", items[0].ShowLocation);
            Assert.IsTrue(items[0].LastUpdated > DateTime.MinValue);
            Assert.AreEqual(0, items[0].FFLCode);
            Assert.IsNull(items[0].FFLURL);
        }

        [TestMethod()]
        public async Task ShowsExistForYearTest()
        {
            //arrange
            ShowController controller = new(new ShowDataAccess(base.Configuration));
            int yearCode = 1995;

            //act
            List<Show> items = await controller.GetShowsByYear(yearCode);

            //assert
            Assert.IsNotNull(items);
            Assert.IsTrue(items.Count > 0);
        }

        [TestMethod()]
        public async Task ShowsForYearTest()
        {
            //arrange
            ShowController controller = new(new ShowDataAccess(base.Configuration));
            int yearCode = 1995;

            //act
            List<Show> items = await controller.GetShowsByYear(yearCode);

            //assert
            Assert.IsNotNull(items);
            Assert.IsTrue(items.Count > 0);
            Assert.IsTrue(items[2].NumberOfSongsPlayed >= 0);
            Assert.AreEqual("Seattle, WA", items[2].ShowCity);
            Assert.IsTrue(items[2].ShowDate >= DateTime.MinValue);
            Assert.AreEqual(4, items[2].ShowCode);
            Assert.AreNotEqual("", items[2].ShowLocation);
        }


        [TestMethod()]
        public async Task Show3Test()
        {
            //arrange
            ShowController controller = new(new ShowDataAccess(base.Configuration));
            int showKey = 3;

            //act
            Show result = await controller.GetShow(showKey);

            //assert
            Assert.IsNotNull(result);
            //Assert.IsTrue(result.IsCancelledShow == false);
            //Assert.IsTrue(result.IsPostponedShow == false);
            //Assert.IsTrue(result.Notes != "");
            //Assert.IsTrue(result.NumberOfRecordings >= 0);
            Assert.IsGreaterThanOrEqualTo(0, result.NumberOfSongsPlayed);
            //Assert.IsTrue(result.NumberOfUnconfirmedRecordings >= 0);
            //Assert.IsTrue(result.OtherPerformers != "");
            Assert.AreEqual("Portland, OR", result.ShowCity);
            //Assert.IsTrue(result.ShowCountry == "United States");
            Assert.IsTrue(result.ShowDate >= DateTime.MinValue);
            Assert.AreEqual(3, result.ShowCode);
            Assert.AreNotEqual("", result.ShowLocation);
        }

                [TestMethod()]
                public async Task Show4Test()
                {
                    //arrange
                    ShowController controller = new(new ShowDataAccess(base.Configuration));
                    int showKey = 842;

                    //act
                    Show result = await controller.GetShow(showKey);

                    //assert
                    Assert.IsNull(result);
                    ////Assert.IsTrue(result.IsCancelledShow == false);
                    ////Assert.IsTrue(result.IsPostponedShow == false);
                    ////Assert.IsTrue(result.Notes != "");
                    ////Assert.IsTrue(result.NumberOfRecordings >= 0);
                    //Assert.IsTrue(result.NumberOfSongsPlayed >= 0);
                    ////Assert.IsTrue(result.NumberOfUnconfirmedRecordings >= 0);
                    ////Assert.IsTrue(result.OtherPerformers != "");
                    //Assert.IsTrue(result.ShowCity == "Portland, OR");
                    ////Assert.IsTrue(result.ShowCountry == "United States");
                    //Assert.IsTrue(result.ShowDate >= DateTime.MinValue);
                    //Assert.IsTrue(result.ShowCode == 3);
                    //Assert.IsTrue(result.ShowLocation != "");
                }

                [TestMethod()]
                public async Task GetListByFFLCodeTest()
                {
                    //arrange
                    ShowDataAccess da = new(base.Configuration);

                    //act
                    List<Show> results = await da.GetListByFFLCode();

                    //assert
                    Assert.IsNotNull(results);
                    Assert.IsTrue(results.Count > 0);
                }

            }
        }