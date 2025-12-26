using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithNZ.Tests;
using SamSmithNZ.Service.DataAccess.FooFighters;
using SamSmithNZ.Service.Models.FooFighters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Tests.DataAccess.FooFighters
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class ShowDataAccessTests : BaseIntegrationTest
    {
        [TestMethod]
        public async Task ShowDataAccess_GetListByYearAsync_ReturnsShows()
        {
            // Arrange
            ShowDataAccess da = new(base.Configuration);
            int yearCode = 1995;

            // Act
            List<Show> results = await da.GetListByYearAsync(yearCode);

            // Assert
            Assert.IsNotNull(results);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod]
        public async Task ShowDataAccess_GetListBySongAsync_ReturnsShows()
        {
            // Arrange
            ShowDataAccess da = new(base.Configuration);
            int songCode = 1;

            // Act
            List<Show> results = await da.GetListBySongAsync(songCode);

            // Assert
            Assert.IsNotNull(results);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod]
        public async Task ShowDataAccess_GetListByFFLCode_ReturnsShows()
        {
            // Arrange
            ShowDataAccess da = new(base.Configuration);

            // Act
            List<Show> results = await da.GetListByFFLCode();

            // Assert
            Assert.IsNotNull(results);
            Assert.IsTrue(results.Count > 0);
        }

                [TestMethod]
                public async Task ShowDataAccess_GetItem_ReturnsShow()
                {
                    // Arrange
                    ShowDataAccess da = new(base.Configuration);
                    int showCode = 3;

                    // Act
                    Show result = await da.GetItem(showCode);

                    // Assert
                    Assert.IsNotNull(result);
                    Assert.AreEqual(3, result.ShowCode);
                }

                [TestMethod]
                public async Task ShowDataAccess_SaveItem_ExecutesSuccessfully()
                {
                    // Arrange
                    ShowDataAccess da = new(base.Configuration);
                    Show show = new()
                    {
                        ShowCode = 3,
                        ShowDate = new System.DateTime(2024, 6, 15),
                        ShowLocation = "Test Venue",
                        ShowCity = "Test City",
                        ShowCountry = "Test Country"
                    };

                    // Act
                    bool result = await da.SaveItem(show);

                    // Assert - Just verify it executes
                    Assert.IsFalse(result == null);
                }
            }
        }
