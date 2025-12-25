using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithnNZ.Tests;
using SamSmithNZ.Service.DataAccess.FooFighters;
using SamSmithNZ.Service.Models.FooFighters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Tests.DataAccess.FooFighters
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class SongDataAccessTests : BaseIntegrationTest
    {
        [TestMethod]
        public async Task SongDataAccess_GetList_ReturnsSongs()
        {
            // Arrange
            SongDataAccess da = new(base.Configuration);

            // Act
            List<Song> results = await da.GetList();

            // Assert
            Assert.IsNotNull(results);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod]
        public async Task SongDataAccess_GetListForAlbumAsync_ReturnsSongs()
        {
            // Arrange
            SongDataAccess da = new(base.Configuration);
            int albumCode = 1;

            // Act
            List<Song> results = await da.GetListForAlbumAsync(albumCode);

            // Assert
            Assert.IsNotNull(results);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod]
        public async Task SongDataAccess_GetListForShowAsync_ReturnsSongs()
        {
            // Arrange
            SongDataAccess da = new(base.Configuration);
            int showCode = 3;

            // Act
            List<Song> results = await da.GetListForShowAsync(showCode);

            // Assert
            Assert.IsNotNull(results);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod]
        public async Task SongDataAccess_GetItem_ReturnsSong()
        {
            // Arrange
            SongDataAccess da = new(base.Configuration);
            int songCode = 1;

            // Act
            Song result = await da.GetItem(songCode);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.SongCode);
        }
    }
}
