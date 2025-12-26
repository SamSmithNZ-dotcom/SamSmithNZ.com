using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithNZ.Tests;
using SamSmithNZ.Service.DataAccess.ITunes;
using SamSmithNZ.Service.Models.ITunes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Tests.ITunes
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class MovementTests : BaseIntegrationTest
    {
        [TestMethod]
        public async Task MovementTest()
        {
            //Arrange
            MovementDataAccess da = new(base.Configuration);
            int playlistCode = 1;
            bool showJustSummary = true;

            //Act
            List<Movement> items = await da.GetList(playlistCode, showJustSummary);

            //Assert
            Assert.IsTrue(items != null);
            Assert.IsTrue(items.Count > 0);
            Assert.IsTrue(items[0].TrackName == "Frank Black - Song Of The Shrimp");
            Assert.IsTrue(items[0].PlayCount == 36);
            Assert.IsTrue(items[0].ChangeThisMonth == 36);
        }

        [TestMethod]
        public async Task MovementSummaryTest()
        {
            //Arrange
            MovementDataAccess da = new(base.Configuration);
            bool showJustSummary = true;

            //Act
            List<Movement> items = await da.GetList(showJustSummary);

            //Assert
            Assert.IsTrue(items != null);
            Assert.IsTrue(items.Count > 0);
            Assert.IsTrue(items[0].TrackName == "Queens of the Stone Age - In the Fade");
            Assert.IsTrue(items[0].PlayCount == 560);
            Assert.IsTrue(items[0].ChangeThisMonth == 560);
        }
    }
}
