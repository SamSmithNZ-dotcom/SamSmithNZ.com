using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithNZ.Tests;
using SamSmithNZ.Service.Controllers.GuitarTab;
using SamSmithNZ.Service.DataAccess.GuitarTab;
using SamSmithNZ.Service.Models.GuitarTab;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Tests.GuitarTab
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class TrackOrderDataAccessTest : BaseIntegrationTest
    {
        [TestMethod()]
        public async Task TrackOrderExistTest()
        {
            //arrange
            TrackOrderController controller = new(new TrackOrderDataAccess(base.Configuration));

            //act
            List<TrackOrder> results = await controller.GetTrackOrders();

            //assert
            Assert.IsNotNull(results);
            Assert.IsNotEmpty(results);
        }

        [TestMethod()]
        public async Task TuningsFirstItemTest()
        {
            //arrange
            TrackOrderController controller = new(new TrackOrderDataAccess(base.Configuration));

            //act
            List<TrackOrder> results = await controller.GetTrackOrders();

            //assert
            Assert.IsNotNull(results);
            Assert.IsGreaterThanOrEqualTo(2, results.Count);
            Assert.AreEqual(0, results[0].SortOrderCode);
            Assert.AreNotEqual("[unknown]", results[0].SortOrderName);
            Assert.AreEqual(1, results[1].SortOrderCode);
            Assert.AreNotEqual("", results[1].SortOrderName);
        }
    }
}
