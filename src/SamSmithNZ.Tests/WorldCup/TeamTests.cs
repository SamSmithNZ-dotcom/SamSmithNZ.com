using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithnNZ.Tests;
using SamSmithNZ.Service.Controllers.WorldCup;
using SamSmithNZ.Service.DataAccess.WorldCup;
using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Tests.WorldCup
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class TeamTests : BaseIntegrationTest
    {
        [TestMethod]
        public async Task TeamsExistTest()
        {
            //arrange
            TeamController controller = new(new TeamDataAccess(base.Configuration));

            //act
            List<Team> results = await controller.GetTeams();

            //assert
            Assert.IsTrue(results != null);
            Assert.IsNotEmpty(results);
        }

        [TestMethod()]
        public async Task TeamsFirstItemTest()
        {
            //Arrange
            TeamController controller = new(new TeamDataAccess(base.Configuration));

            //act
            List<Team> results = await controller.GetTeams();

            //assert
            Assert.IsTrue(results != null);
            Assert.IsNotEmpty(results);
            bool found1 = false;
            foreach (Team item in results)
            {
                if (item.TeamCode == 1)
                {
                    found1 = true;
                    TestNewZealandTeam(item);
                }
            }
            Assert.IsTrue(found1);
        }

        [TestMethod()]
        public async Task TeamGetNewZealandTest()
        {
            //arrange
            TeamController controller = new(new TeamDataAccess(base.Configuration));
            int TeamCode = 1;

            //act
            Team result = await controller.GetTeam(TeamCode);


            //assert
            Assert.IsTrue(result != null);
            TestNewZealandTeam(result);
        }

        private static void TestNewZealandTeam(Team item)
        {
            Assert.IsTrue(item.TeamCode == 1);
            Assert.IsTrue(item.TeamName == "New Zealand");
            Assert.IsTrue(item.FlagName == "22px-Flag_of_New_Zealand_svg.png");
        }

    }
}
