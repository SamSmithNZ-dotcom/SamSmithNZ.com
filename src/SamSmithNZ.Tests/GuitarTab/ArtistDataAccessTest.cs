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
    public class ArtistDataAccessTest : BaseIntegrationTest
    {
        [TestMethod()]
        public async Task ArtistsExistTest()
        {
            //arrange
            ArtistController controller = new(new ArtistDataAccess(base.Configuration));
            bool includeAllItems = false;

            //act
            List<Artist> results = await controller.GetArtists(includeAllItems);

            //assert
            Assert.IsNotNull(results);
            Assert.IsNotEmpty(results);
        }

        [TestMethod()]
        public async Task ArtistsFirstItemTest()
        {
            //arrange
            ArtistController controller = new(new ArtistDataAccess(base.Configuration));
            bool includeAllItems = false;

            //act
            List<Artist> results = await controller.GetArtists(includeAllItems);

            //assert
            Assert.IsNotNull(results);
            Assert.IsNotEmpty(results);
            Assert.AreEqual("Ash", results[0].ArtistName);
            Assert.AreEqual("Ash", results[0].ArtistNameTrimed);
        }

        [TestMethod()]
        public async Task ArtistsFirstItemIncludeAllTest()
        {
            //arrange
            ArtistController controller = new(new ArtistDataAccess(base.Configuration));
            bool includeAllItems = true;

            //act
            List<Artist> results = await controller.GetArtists(includeAllItems);

            //assert
            Assert.IsNotNull(results);
            Assert.IsNotEmpty(results);
            Assert.AreEqual("(Top Songs)", results[0].ArtistName);
            Assert.AreEqual("(TopSongs)", results[0].ArtistNameTrimed);
        }

                [TestMethod()]
                public async Task ArtistsIncludeAllItemsExistTest()
                {
                    //arrange
                    ArtistController controller = new(new ArtistDataAccess(base.Configuration));
                    bool includeAllItems = true;

                    //act
                    List<Artist> results = await controller.GetArtists(includeAllItems);

                    //assert
                    Assert.IsNotNull(results);
                    Assert.IsNotEmpty(results);
                }

            }
        }


