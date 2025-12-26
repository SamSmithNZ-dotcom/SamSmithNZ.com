using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithNZ.Tests;
using SamSmithNZ.Service.Controllers.FooFighters;
using SamSmithNZ.Service.DataAccess.FooFighters;
using SamSmithNZ.Service.Models.FooFighters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Tests.FooFighters
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class AverageSongTest : BaseIntegrationTest
    {

        [TestMethod()]
        public async Task AverageSetlistExistTest()
        {
            //arrange
            AverageSetlistController controller = new(new AverageSetlistDataAccess(base.Configuration));
            int yearCode = 2015;
            int minimumSongCount = 0;
            bool showAllSongs = true;

            //act
            List<AverageSetlist> items = await controller.GetAverageSetlist(yearCode, minimumSongCount, showAllSongs);

            //assert
            Assert.IsNotNull(items);
            Assert.IsNotEmpty(items);
        }

        [TestMethod()]
        public async Task AverageSetlist2015Test()
        {
            //arrange
            AverageSetlistController controller = new(new AverageSetlistDataAccess(base.Configuration));
            int yearCode = 2015;
            int minimumSongCount = 0;
            bool showAllSongs = true;

            //act
            List<AverageSetlist> items = await controller.GetAverageSetlist(yearCode, minimumSongCount, showAllSongs);

            //assert
            Assert.IsNotNull(items);
            Assert.IsNotEmpty(items);
            Assert.IsGreaterThan(0, items[0].SongCode);
            Assert.AreNotEqual("", items[0].SongName);
            Assert.IsGreaterThan(0, items[0].AvgShowSongOrder);
            Assert.IsGreaterThan(0, items[0].SongCount);
            Assert.IsGreaterThan(0, items[0].SongRank);
            Assert.IsGreaterThan(0, items[0].YearCode);
        }

    }
}