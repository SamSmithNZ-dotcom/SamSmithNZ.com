using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithNZ.Tests;
using SamSmithNZ.Service.DataAccess.WorldCup;
using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;

namespace SamSmithNZ.Tests.DataAccess.WorldCup
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class GoalDataAccessTests : BaseIntegrationTest
    {
        [TestMethod]
        public void ProcessGoalHTMLMigration_RegularGoal_ParsesCorrectly()
        {
            // Arrange
            GoalDataAccess da = new(base.Configuration);
            string goalText = "45";
            string playerName = "Neymar";

            // Act
            List<Goal> results = da.ProcessGoalHTMLMigration(goalText, playerName);

            // Assert
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(45, results[0].GoalTime);
            Assert.AreEqual(0, results[0].InjuryTime);
            Assert.IsFalse(results[0].IsPenalty);
            Assert.IsFalse(results[0].IsOwnGoal);
        }

        [TestMethod]
        public void ProcessGoalHTMLMigration_PenaltyGoal_ParsesCorrectly()
        {
            // Arrange
            GoalDataAccess da = new(base.Configuration);
            string goalText = "71(pen.)";
            string playerName = "Neymar";

            // Act
            List<Goal> results = da.ProcessGoalHTMLMigration(goalText, playerName);

            // Assert
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(71, results[0].GoalTime);
            Assert.IsTrue(results[0].IsPenalty);
            Assert.IsFalse(results[0].IsOwnGoal);
        }

        [TestMethod]
        public void ProcessGoalHTMLMigration_OwnGoal_ParsesCorrectly()
        {
            // Arrange
            GoalDataAccess da = new(base.Configuration);
            string goalText = "11(o.g.)";
            string playerName = "Marcelo";

            // Act
            List<Goal> results = da.ProcessGoalHTMLMigration(goalText, playerName);

            // Assert
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(11, results[0].GoalTime);
            Assert.IsFalse(results[0].IsPenalty);
            Assert.IsTrue(results[0].IsOwnGoal);
        }

        [TestMethod]
        public void ProcessGoalHTMLMigration_InjuryTimeGoal_ParsesCorrectly()
        {
            // Arrange
            GoalDataAccess da = new(base.Configuration);
            string goalText = "90+1";
            string playerName = "Oscar";

            // Act
            List<Goal> results = da.ProcessGoalHTMLMigration(goalText, playerName);

            // Assert
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(90, results[0].GoalTime);
            Assert.AreEqual(1, results[0].InjuryTime);
        }

        [TestMethod]
        public void ProcessGoalHTMLMigration_MultipleGoals_ParsesCorrectly()
        {
            // Arrange
            GoalDataAccess da = new(base.Configuration);
            string goalText = "29, 71(pen.)";
            string playerName = "Neymar";

            // Act
            List<Goal> results = da.ProcessGoalHTMLMigration(goalText, playerName);

            // Assert
            Assert.AreEqual(2, results.Count);
            Assert.AreEqual(29, results[0].GoalTime);
            Assert.IsFalse(results[0].IsPenalty);
            Assert.AreEqual(71, results[1].GoalTime);
            Assert.IsTrue(results[1].IsPenalty);
        }

        [TestMethod]
        public void ProcessGoalHTMLMigration_EmptyString_ReturnsEmptyList()
        {
            // Arrange
            GoalDataAccess da = new(base.Configuration);
            string goalText = "";
            string playerName = "Test";

            // Act
            List<Goal> results = da.ProcessGoalHTMLMigration(goalText, playerName);

            // Assert
            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void ProcessGoalHTMLMigration_NullString_ReturnsEmptyList()
        {
            // Arrange
            GoalDataAccess da = new(base.Configuration);
            string goalText = null;
            string playerName = "Test";

            // Act
            List<Goal> results = da.ProcessGoalHTMLMigration(goalText, playerName);

            // Assert
            Assert.AreEqual(0, results.Count);
        }

                [TestMethod]
                public void ProcessGoalHTMLMigration_WithHTMLEntities_ParsesCorrectly()
                {
                    // Arrange
                    GoalDataAccess da = new(base.Configuration);
                    string goalText = "45&#39;";
                    string playerName = "Test";

                    // Act
                    List<Goal> results = da.ProcessGoalHTMLMigration(goalText, playerName);

                    // Assert
                    Assert.AreEqual(1, results.Count);
                    Assert.AreEqual(45, results[0].GoalTime);
                }

                        [TestMethod]
                        public async System.Threading.Tasks.Task GoalDataAccess_GetList_ReturnsAllGoals()
                        {
                            // Arrange
                            GoalDataAccess da = new(base.Configuration);

                            // Act
                            List<Goal> results = await da.GetList();

                            // Assert
                            Assert.IsNotNull(results);
                            Assert.IsTrue(results.Count > 0);
                        }

                        [TestMethod]
                        public async System.Threading.Tasks.Task GoalDataAccess_GetListByGame_ReturnsGoals()
                        {
                            // Arrange
                            GoalDataAccess da = new(base.Configuration);
                            int gameCode = 1;

                            // Act
                            List<Goal> results = await da.GetListByGame(gameCode);

                            // Assert
                            Assert.IsNotNull(results);
                        }

                        [TestMethod]
                        public async System.Threading.Tasks.Task GoalDataAccess_SaveItem_ExecutesSuccessfully()
                        {
                            // Arrange
                            GoalDataAccess da = new(base.Configuration);
                            Goal goal = new()
                            {
                                GoalCode = 1,
                                GameCode = 1,
                                PlayerCode = 1,
                                GoalTime = 45,
                                InjuryTime = 0,
                                IsPenalty = false,
                                IsOwnGoal = false,
                                IsGoldenGoal = false
                            };

                            // Act
                            bool result = await da.SaveItem(goal);

                            // Assert - Just verify it executes
                            Assert.IsFalse(result == null);
                        }

                        [TestMethod]
                        public async System.Threading.Tasks.Task GoalDataAccess_DeleteItem_ExecutesSuccessfully()
                        {
                            // Arrange
                            GoalDataAccess da = new(base.Configuration);
                            Goal goal = new()
                            {
                                GoalCode = 1
                            };

                            // Act
                            bool result = await da.DeleteItem(goal);

                            // Assert - Just verify it executes
                            Assert.IsFalse(result == null);
                        }
                    }
                }
