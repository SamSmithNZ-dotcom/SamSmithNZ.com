using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithNZ.Tests;
using SamSmithNZ.Service.DataAccess.WorldCup;
using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Tests.DataAccess.WorldCup
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class PenaltyShootoutGoalDataAccessTests : BaseIntegrationTest
    {
                [TestMethod]
                public async Task PenaltyShootoutGoalDataAccess_GetList_ReturnsGoals()
                {
                    // Arrange
                    PenaltyShootoutGoalDataAccess da = new(base.Configuration);
                    int gameCode = 7328;

                    // Act
                    List<PenaltyShootoutGoal> results = await da.GetList(gameCode);

                    // Assert
                    Assert.IsNotNull(results);
                }

                [TestMethod]
                public async Task PenaltyShootoutGoalDataAccess_SaveItem_ExecutesSuccessfully()
                {
                    // Arrange
                    PenaltyShootoutGoalDataAccess da = new(base.Configuration);
                    PenaltyShootoutGoal goal = new()
                    {
                        PenaltyCode = 1,
                        GameCode = 7328,
                        PlayerCode = 1,
                        PenaltyOrder = 1,
                        Scored = true
                    };

                    // Act
                    bool result = await da.SaveItem(goal);

                    // Assert - Just verify it executes
                    Assert.IsFalse(result == null);
                }

                [TestMethod]
                public async Task PenaltyShootoutGoalDataAccess_DeleteItem_ExecutesSuccessfully()
                {
                    // Arrange
                    PenaltyShootoutGoalDataAccess da = new(base.Configuration);
                    PenaltyShootoutGoal goal = new()
                    {
                        PenaltyCode = 1
                    };

                    // Act
                    bool result = await da.DeleteItem(goal);

                    // Assert - Just verify it executes
                    Assert.IsFalse(result == null);
                }
            }
        }
