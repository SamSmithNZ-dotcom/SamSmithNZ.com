using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Tests.DataAccess.WorldCup
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class ThirdPlaceMatchDataAccessTests : SamSmithNZ.Tests.BaseIntegrationTest
    {
        [TestMethod]
        public async Task ThirdPlaceMatchDataAccess_GetList_ReturnsResults()
        {
            // Arrange
            Service.DataAccess.WorldCup.ThirdPlaceMatchDataAccess dataAccess = 
                new Service.DataAccess.WorldCup.ThirdPlaceMatchDataAccess(Configuration);

            // Act
            List<ThirdPlaceMatch> results = await dataAccess.GetList(1, "A,B,C,D");

            // Assert
            Assert.IsNotNull(results);
        }
    }
}
