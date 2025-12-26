using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithNZ.Tests;
using SamSmithNZ.Service.Controllers.WorldCup;
using SamSmithNZ.Service.DataAccess.WorldCup;
using SamSmithNZ.Service.Models.WorldCup;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SamSmithNZ.Tests.WorldCup
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class GroupCodeTests : BaseIntegrationTest
    {
        [TestMethod]
        public async Task GroupCodesExistTest()
        {
            //arrange
            GroupCodeController controller = new(new GroupCodeDataAccess(base.Configuration));
            int tournamentCode = 19;
            int roundNumber = 1;

            //act
            List<GroupCode> results = await controller.GetGroupCodes(tournamentCode, roundNumber);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsNotEmpty(results);
        }

        [TestMethod()]
        public async Task GroupCodesFirstItemTest()
        {
            //arrange
            GroupCodeController controller = new(new GroupCodeDataAccess(base.Configuration));
            int tournamentCode = 19;
            int roundNumber = 1;

            //act
            List<GroupCode> results = await controller.GetGroupCodes(tournamentCode, roundNumber);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsNotEmpty(results);
            Assert.IsTrue(results[0].RoundCode == "A");
            Assert.IsTrue(results[0].IsLastRound == false);
        }

    }
}
