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
    public class TopArtistsTests : BaseIntegrationTest
    {
        [TestMethod]
        public async Task TopArtistsTest()
        {
            //Arrange
            TopArtistsDataAccess da = new(base.Configuration);
            int playlistCode = 1;
            bool showJustSummary = true;

            //Act
            List<TopArtists> items = await da.GetList(playlistCode, showJustSummary);

            //Assert
            Assert.IsTrue(items != null);
            Assert.IsTrue(items.Count > 0);
            Assert.IsTrue(items[0].ArtistCount == 7);
            Assert.IsTrue(items[0].ArtistName == "Betchadupa");
        }

        [TestMethod]
        public async Task TopArtistsSummaryTest()
        {
            //Arrange
            TopArtistsDataAccess da = new(base.Configuration);
            bool showJustSummary = true;

            //Act
            List<TopArtists> items = await da.GetList(showJustSummary);

            //Assert
            Assert.IsTrue(items != null);
            Assert.IsTrue(items.Count > 0);
            Assert.IsTrue(items[0].ArtistCount >= 1661);
            Assert.IsTrue(items[0].ArtistName == "Foo Fighters");
        }
    }
}
