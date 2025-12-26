using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Tests.DataAccess.WorldCup
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class PlayoffDataAccessTests : SamSmithNZ.Tests.BaseIntegrationTest
    {
        [TestMethod]
        public async Task PlayoffDataAccess_GetList_ReturnsResults()
        {
            // Arrange
            Service.DataAccess.WorldCup.PlayoffDataAccess dataAccess = 
                new Service.DataAccess.WorldCup.PlayoffDataAccess(Configuration);

            // Act
            List<Playoff> results = await dataAccess.GetList(1);

            // Assert
            Assert.IsNotNull(results);
        }

        [TestMethod]
        public async Task PlayoffDataAccess_SaveItem_ExecutesSuccessfully()
        {
            // Arrange
            Service.DataAccess.WorldCup.PlayoffDataAccess dataAccess = 
                new Service.DataAccess.WorldCup.PlayoffDataAccess(Configuration);

            Playoff playoff = new Playoff
            {
                TournamentCode = 1,
                RoundCode = "16",
                GameNumber = 1,
                Team1Prereq = "A1",
                Team2Prereq = "B2",
                SortOrder = 1
            };

            // Act
            bool result = await dataAccess.SaveItem(playoff);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
