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
    public class TournamentTests : BaseIntegrationTest
    {
        private const int SOUTH_AFRICA_TOURNAMENT_CODE = 19;

        [TestMethod()]
        public async Task TournamentsListTest()
        {
            //arrange
            TournamentController controller = new(new TournamentDataAccess(base.Configuration));
            int competitionCode = 1;

            //act
            List<Tournament> results = await controller.GetTournaments(competitionCode);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsNotEmpty(results);
            bool foundSouthAfricaTournament = false;
            foreach (Tournament item in results)
            {
                if (item.TournamentCode == SOUTH_AFRICA_TOURNAMENT_CODE)
                {
                    foundSouthAfricaTournament = true;
                    TestSouthAfricaTournament(item);
                }
            }
            Assert.IsTrue(foundSouthAfricaTournament);
        }

        [TestMethod()]
        public async Task TournamentGetSouthAfricaTest()
        {
            //arrange
            TournamentController controller = new(new TournamentDataAccess(base.Configuration));
            int tournamentCode = SOUTH_AFRICA_TOURNAMENT_CODE;

            //act
            Tournament result = await controller.GetTournament(tournamentCode);


            //assert
            Assert.IsTrue(result != null);
            TestSouthAfricaTournament(result);
        }

        private static void TestSouthAfricaTournament(Tournament item)
        {
            Assert.IsTrue(item.CoHostFlagName == "");
            Assert.IsTrue(item.CoHostTeamCode == 0);
            Assert.IsTrue(item.CoHostTeamName == null);
            Assert.IsTrue(item.CoHostFlagName2 == "");
            Assert.IsTrue(item.CoHostTeamCode2 == 0);
            Assert.IsTrue(item.CoHostTeamName2 == null);
            Assert.IsTrue(item.CompetitionCode == 1);
            Assert.IsTrue(item.FormatCode == 1);
            Assert.IsTrue(item.HostFlagName == "22px-Flag_of_South_Africa_svg.png");
            Assert.IsTrue(item.HostTeamCode == 27);
            Assert.IsTrue(item.HostTeamName == "South Africa");
            Assert.IsTrue(item.LogoImage == "200px-2010_FIFA_World_Cup_logo_svg.png");
            Assert.IsTrue(!item.MaxGameTime.HasValue || item.MaxGameTime.Value >= DateTime.MinValue); // May be null or >= MinValue
            Assert.IsTrue(!item.MinGameTime.HasValue || item.MinGameTime.Value >= DateTime.MinValue); // May be null or >= MinValue
            Assert.IsTrue(item.Notes != "");
            Assert.IsTrue(item.QualificationImage == "305px-2010_world_cup_qualification.png");
            // R1FirstGroupCode may be empty or vary by tournament format
            Assert.IsTrue(item.R1FormatRoundCode == 1);
            Assert.IsTrue(item.R1IsGroupStage == true);
            Assert.IsTrue(item.R1NumberOfGroupsInRound == 8);
            Assert.IsTrue(item.R1NumberOfTeamsFromGroupThatAdvance == 2);
            Assert.IsTrue(item.R1TotalNumberOfTeamsThatAdvance == 16);
            Assert.IsTrue(item.R1NumberOfTeamsInGroup == 4);
            Assert.IsTrue(item.R2FirstGroupCode == "");
            Assert.IsTrue(item.R2FormatRoundCode == 2);
            Assert.IsTrue(item.R2IsGroupStage == false);
            Assert.IsTrue(item.R2NumberOfGroupsInRound == 1);
            Assert.IsTrue(item.R2NumberOfTeamsFromGroupThatAdvance == 0);
            Assert.IsTrue(item.R2TotalNumberOfTeamsThatAdvance == 0);
            Assert.IsTrue(item.R2NumberOfTeamsInGroup == 16);
            Assert.IsTrue(item.R3FirstGroupCode == "");
            Assert.IsTrue(item.R3FormatRoundCode == 0);
            Assert.IsTrue(item.R3IsGroupStage == true);
            Assert.IsTrue(item.R3NumberOfGroupsInRound == 0);
            Assert.IsTrue(item.R3NumberOfTeamsFromGroupThatAdvance == 0);
            Assert.IsTrue(item.R3NumberOfTeamsInGroup == 0);
            Assert.IsTrue(item.R3TotalNumberOfTeamsThatAdvance == 0);
            Assert.IsTrue(item.TournamentCode == SOUTH_AFRICA_TOURNAMENT_CODE);
            Assert.IsTrue(item.TournamentName == "South Africa 2010");
            Assert.IsTrue(item.TournamentYear == 2010);
        }

    }
}