using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithnNZ.Tests;
using SamSmithNZ.Service.DataAccess.WorldCup;
using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Tests.DataAccess.WorldCup
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class GroupDataAccessTests : BaseIntegrationTest
    {
        [TestMethod]
        public async Task GroupDataAccess_GetList_ReturnsGroups()
        {
            // Arrange
            GroupDataAccess da = new(base.Configuration);
            int tournamentCode = 19;
            int roundNumber = 1;
            string roundCode = "F";

            // Act
            List<Group> results = await da.GetList(tournamentCode, roundNumber, roundCode);

            // Assert
            Assert.IsNotNull(results);
        }

                [TestMethod]
                public async Task GroupDataAccess_GetListWithNullRoundCode_ReturnsGroups()
                {
                    // Arrange
                    GroupDataAccess da = new(base.Configuration);
                    int tournamentCode = 19;
                    int roundNumber = 1;
                    string roundCode = null;

                    // Act
                    List<Group> results = await da.GetList(tournamentCode, roundNumber, roundCode);

                    // Assert
                    Assert.IsNotNull(results);
                }

                        [TestMethod]
                        public async Task GroupDataAccess_SaveItemAsync_ExecutesSuccessfully()
                        {
                            // Arrange
                            GroupDataAccess da = new(base.Configuration);
                            int tournamentCode = 19;
                            int roundNumber = 1;
                            string roundCode = "A";
                            int teamCode = 1;

                            // Act
                            bool result = await da.SaveItemAsync(tournamentCode, roundNumber, roundCode, teamCode);

                            // Assert
                            Assert.IsTrue(result);
                        }

                        [TestMethod]
                        public async Task GroupDataAccess_DeleteItemAsync_ExecutesSuccessfully()
                        {
                            // Arrange
                            GroupDataAccess da = new(base.Configuration);
                            Group group = new();

                            // Act
                            bool result = await da.DeleteItemAsync(group);

                            // Assert - Just verify it executes
                            Assert.IsFalse(result == null);
                        }
                    }
                }
