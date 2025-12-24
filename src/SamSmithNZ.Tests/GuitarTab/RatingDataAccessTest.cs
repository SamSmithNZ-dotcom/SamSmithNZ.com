using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithnNZ.Tests;
using SamSmithNZ.Service.Controllers.GuitarTab;
using SamSmithNZ.Service.DataAccess.GuitarTab;
using SamSmithNZ.Service.Models.GuitarTab;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Tests.GuitarTab
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class RatingDataAccessTest : BaseIntegrationTest
    {
        [TestMethod()]
        public async Task RatingsExistTest()
        {
            //arrange
            RatingController controller = new(new RatingDataAccess(base.Configuration));

            //act
            List<Rating> results = await controller.GetRatings();

            //assert
            Assert.IsNotNull(results);
            Assert.IsNotEmpty(results);
        }

        [TestMethod()]
        public async Task RatingsFirstItemTest()
        {
            //arrange
            RatingController controller = new(new RatingDataAccess(base.Configuration));

            //act
            List<Rating> results = await controller.GetRatings();

            //assert
            Assert.IsNotNull(results);
            Assert.IsNotEmpty(results);
            Assert.AreEqual(0, results[0].RatingCode);
            Assert.AreEqual(1, results[1].RatingCode);
        }

    }
}