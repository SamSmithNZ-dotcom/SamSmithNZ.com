﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithnNZ.Tests;
using SamSmithNZ.Service.DataAccess.WorldCup;
using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Tests.WorldCup
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class PlayerTests : BaseIntegrationTest
    {
        [TestMethod]
        public async Task PlayersExistTest()
        {
            //arrange
            PlayerDataAccess da = new(base.Configuration);
            int gameCode = 7328;

            //act
            List<Player> results = await da.GetList(gameCode);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod()]
        public async Task PlayersFirstItemTest()
        {
            //arrange
            PlayerDataAccess da = new(base.Configuration);
            int gameCode = 7328;

            //act
            List<Player> results = await da.GetList(gameCode);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
            Assert.IsTrue(results[0].Number == 2);
            Assert.IsTrue(results[0].PlayerCode > 0);
            Assert.IsTrue(results[0].PlayerName == "Alves, Dani (Brazil)");
            Assert.IsTrue(results[0].Position == "DF");
            Assert.IsTrue(results[0].TeamName == "Brazil");
        }


    }
}
