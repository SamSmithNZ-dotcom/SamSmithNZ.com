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
    public class TuningDataAccessTest : BaseIntegrationTest
    {
        [TestMethod()]
        public async Task TuningsExistTest()
        {
            //arrange
            TuningController controller = new(new TuningDataAccess(base.Configuration));

            //act
            List<Tuning> results = await controller.GetTunings();

            //assert
            Assert.IsNotNull(results);
            Assert.IsNotEmpty(results);
        }

        [TestMethod()]
        public async Task TuningsFirstItemTest()
        {
            //arrange
            TuningController controller = new(new TuningDataAccess(base.Configuration));

            //act
            List<Tuning> results = await controller.GetTunings();

            //assert
            Assert.IsNotNull(results);
            Assert.IsNotEmpty(results);
            Assert.AreEqual(0, results[0].TuningCode);
            Assert.AreEqual("[unknown]", results[0].TuningName);
        }

    }
}


