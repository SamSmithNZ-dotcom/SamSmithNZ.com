using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithnNZ.Tests;
using SamSmithNZ.Service.DataAccess.WorldCup;
using SamSmithNZ.Service.Models.WorldCup;
using static SamSmithNZ.Service.DataAccess.WorldCup.EloRating;

namespace SamSmithNZ.Tests.WorldCup
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class ELORatingTests : BaseIntegrationTest
    {

        [TestMethod()]
        public void EvenTeamsELORatingsTest()
        {
            //arrange
            EloRating eloRating = new();
            int team1ELORating = 1000;
            int team2ELORating = 1000;
            Service.Models.WorldCup.Game game = new()
            {
                Team1NormalTimeScore = 1,
                Team2NormalTimeScore = 0
            };

            //act
            WhoWonEnum? whoWonGame = eloRating.WhoWon(game);
            double kFactor = eloRating.CalculateKFactor(game);
            (int, int) result = eloRating.GetEloRatingScoresForMatchUp(team1ELORating, team2ELORating,
                whoWonGame == WhoWonEnum.Team1,
                whoWonGame == WhoWonEnum.Team2,
                kFactor);

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1050, result.Item1);
            Assert.AreEqual(950, result.Item2);
        }

        [TestMethod()]
        public void GermanyJapan2022ELORatingsTest()
        {
            //arrange
            EloRating eloRating = new();
            int team1ELORating = 1963;
            int team2ELORating = 1798;
            Service.Models.WorldCup.Game game = new()
            {
                Team1NormalTimeScore = 1,
                Team2NormalTimeScore = 2
            };

            //act
            WhoWonEnum? whoWonGame = eloRating.WhoWon(game);
            double kFactor = eloRating.CalculateKFactor(game);
            (int, int) result = eloRating.GetEloRatingScoresForMatchUp(team1ELORating, team2ELORating,
                whoWonGame == WhoWonEnum.Team1,
                whoWonGame == WhoWonEnum.Team2,
                kFactor);

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1891, result.Item1);
            Assert.AreEqual(1870, result.Item2);
        }


        [TestMethod()]
        public void SemiFinal2022ELORatingsAndOddsTest()
        {
            //arrange
            EloRating eloRating = new();
            int argentina = 2072; // 63.47%
            int croatia = 1976; // 36.53%
            int france = 2120; // 59.52%
            int morrocco = 2053; // 40.48%
            Service.Models.WorldCup.Game game = new()
            {
                Team1NormalTimeScore = 1,
                Team2NormalTimeScore = 0
            };

            //act
            Service.Models.WorldCup.Game game1 = new();
            game1.Team1PreGameEloRating = argentina;
            game1.Team2PreGameEloRating = croatia;
            game1.GameCanEndInADraw = false;
            double argentinaChanceToWin1 = game1.Team1ChanceToWin;
            Assert.AreEqual(57.21, argentinaChanceToWin1);

            Service.Models.WorldCup.Game game2 = new();
            game2.Team1PreGameEloRating = argentina;
            game2.Team2PreGameEloRating = france;
            game2.GameCanEndInADraw = false;
            double argentinaChanceToWin2 = game2.Team1ChanceToWin;
            Assert.AreEqual(38.19, argentinaChanceToWin2);

            Service.Models.WorldCup.Game game3 = new();
            game3.Team1PreGameEloRating = argentina;
            game3.Team2PreGameEloRating = morrocco;
            game3.GameCanEndInADraw = false;
            double argentinaChanceToWin3 = game3.Team1ChanceToWin;
            Assert.AreEqual(46.18, argentinaChanceToWin3);

            Service.Models.WorldCup.Game game4 = new();
            game4.Team1PreGameEloRating = france;
            game4.Team2PreGameEloRating = croatia;
            game4.GameCanEndInADraw = false;
            double franceChanceToWin4 = game4.Team1ChanceToWin;
            Assert.AreEqual(63.8, franceChanceToWin4);

            Service.Models.WorldCup.Game game5 = new();
            game5.Team1PreGameEloRating = france;
            game5.Team2PreGameEloRating = morrocco;
            game5.GameCanEndInADraw = false;
            double franceChanceToWin5 = game5.Team1ChanceToWin;
            Assert.AreEqual(53.08, franceChanceToWin5);

            Service.Models.WorldCup.Game game6 = new();
            game6.Team1PreGameEloRating = croatia;
            game6.Team2PreGameEloRating = morrocco;
            game6.GameCanEndInADraw = false;
            double croatiaChanceToWin6 = game6.Team1ChanceToWin;
            Assert.AreEqual(34.99, croatiaChanceToWin6);


            //SF1 (2 paths)
            //SF2 (2 paths)
            //Final (2 paths)

                        //(int, int) result = eloRating.GetEloRatingScoresForMatchUp(argentina, croatia,
                        //WhoWonEnum? whoWonGame = eloRating.WhoWon(game);
                        //double kFactor = eloRating.CalculateKFactor(game);
                        //(int, int) result = eloRating.GetEloRatingScoresForMatchUp(team1ELORating, team2ELORating,
                        //    whoWonGame == WhoWonEnum.Team1,
                        //    whoWonGame == WhoWonEnum.Team2,
                        //    kFactor);

                        ////assert
                        //Assert.IsNotNull(result);
                        //Assert.AreEqual(1891, result.Item1);
                        //Assert.AreEqual(1870, result.Item2);
                    }

                    #region CalculateGoalDifference Tests

                    [TestMethod()]
                    public void CalculateGoalDifference_NormalTimeOnly_ReturnsCorrectDifference()
                    {
                        // Arrange
                        EloRating eloRating = new();
                        Game game = new()
                        {
                            Team1NormalTimeScore = 3,
                            Team2NormalTimeScore = 1
                        };

                        // Act
                        int? result = eloRating.CalculateGoalDifference(game);

                        // Assert
                        Assert.AreEqual(2, result);
                    }

                    [TestMethod()]
                    public void CalculateGoalDifference_WithExtraTime_IncludesExtraTimeGoals()
                    {
                        // Arrange
                        EloRating eloRating = new();
                        Game game = new()
                        {
                            Team1NormalTimeScore = 2,
                            Team2NormalTimeScore = 2,
                            Team1ExtraTimeScore = 1,
                            Team2ExtraTimeScore = 0
                        };

                        // Act
                        int? result = eloRating.CalculateGoalDifference(game);

                        // Assert
                        Assert.AreEqual(1, result);
                    }

                    [TestMethod()]
                    public void CalculateGoalDifference_PenaltiesTeam1Wins_ReturnsOne()
                    {
                        // Arrange
                        EloRating eloRating = new();
                        Game game = new()
                        {
                            Team1NormalTimeScore = 1,
                            Team2NormalTimeScore = 1,
                            Team1PenaltiesScore = 5,
                            Team2PenaltiesScore = 3
                        };

                        // Act
                        int? result = eloRating.CalculateGoalDifference(game);

                        // Assert
                        Assert.AreEqual(1, result, "Penalties should always return 1 for winner");
                    }

                    [TestMethod()]
                    public void CalculateGoalDifference_PenaltiesTeam2Wins_ReturnsNegativeOne()
                    {
                        // Arrange
                        EloRating eloRating = new();
                        Game game = new()
                        {
                            Team1NormalTimeScore = 2,
                            Team2NormalTimeScore = 2,
                            Team1PenaltiesScore = 2,
                            Team2PenaltiesScore = 4
                        };

                        // Act
                        int? result = eloRating.CalculateGoalDifference(game);

                        // Assert
                        Assert.AreEqual(-1, result, "Penalties should always return -1 for loser");
                    }

                    [TestMethod()]
                    public void CalculateGoalDifference_GameNotStarted_ReturnsNull()
                    {
                        // Arrange
                        EloRating eloRating = new();
                        Game game = new()
                        {
                            Team1NormalTimeScore = null,
                            Team2NormalTimeScore = null
                        };

                        // Act
                        int? result = eloRating.CalculateGoalDifference(game);

                        // Assert
                        Assert.IsNull(result, "Game not started should return null");
                    }

                    [TestMethod()]
                    public void CalculateGoalDifference_Team2Wins_ReturnsNegativeNumber()
                    {
                        // Arrange
                        EloRating eloRating = new();
                        Game game = new()
                        {
                            Team1NormalTimeScore = 0,
                            Team2NormalTimeScore = 3
                        };

                        // Act
                        int? result = eloRating.CalculateGoalDifference(game);

                        // Assert
                        Assert.AreEqual(-3, result);
                    }

                    [TestMethod()]
                    public void CalculateGoalDifference_Draw_ReturnsZero()
                    {
                        // Arrange
                        EloRating eloRating = new();
                        Game game = new()
                        {
                            Team1NormalTimeScore = 2,
                            Team2NormalTimeScore = 2
                        };

                        // Act
                        int? result = eloRating.CalculateGoalDifference(game);

                        // Assert
                        Assert.AreEqual(0, result);
                    }

                    #endregion

                    #region WhoWon Tests

                    [TestMethod()]
                    public void WhoWon_Team1Wins_ReturnsTeam1()
                    {
                        // Arrange
                        EloRating eloRating = new();
                        Game game = new()
                        {
                            Team1NormalTimeScore = 3,
                            Team2NormalTimeScore = 1
                        };

                        // Act
                        WhoWonEnum? result = eloRating.WhoWon(game);

                        // Assert
                        Assert.AreEqual(WhoWonEnum.Team1, result);
                    }

                    [TestMethod()]
                    public void WhoWon_Team2Wins_ReturnsTeam2()
                    {
                        // Arrange
                        EloRating eloRating = new();
                        Game game = new()
                        {
                            Team1NormalTimeScore = 1,
                            Team2NormalTimeScore = 4
                        };

                        // Act
                        WhoWonEnum? result = eloRating.WhoWon(game);

                        // Assert
                        Assert.AreEqual(WhoWonEnum.Team2, result);
                    }

                    [TestMethod()]
                    public void WhoWon_Draw_ReturnsDraw()
                    {
                        // Arrange
                        EloRating eloRating = new();
                        Game game = new()
                        {
                            Team1NormalTimeScore = 2,
                            Team2NormalTimeScore = 2
                        };

                        // Act
                        WhoWonEnum? result = eloRating.WhoWon(game);

                        // Assert
                        Assert.AreEqual(WhoWonEnum.Draw, result);
                    }

                    [TestMethod()]
                    public void WhoWon_Team1Withdrew_ReturnsTeam2()
                    {
                        // Arrange
                        EloRating eloRating = new();
                        Game game = new()
                        {
                            Team1Withdrew = true,
                            Team2Withdrew = false
                        };

                        // Act
                        WhoWonEnum? result = eloRating.WhoWon(game);

                        // Assert
                        Assert.AreEqual(WhoWonEnum.Team2, result);
                    }

                    [TestMethod()]
                    public void WhoWon_Team2Withdrew_ReturnsTeam1()
                    {
                        // Arrange
                        EloRating eloRating = new();
                        Game game = new()
                        {
                            Team1Withdrew = false,
                            Team2Withdrew = true
                        };

                        // Act
                        WhoWonEnum? result = eloRating.WhoWon(game);

                        // Assert
                        Assert.AreEqual(WhoWonEnum.Team1, result);
                    }

                    [TestMethod()]
                    public void WhoWon_GameNotStarted_ReturnsNull()
                    {
                        // Arrange
                        EloRating eloRating = new();
                        Game game = new()
                        {
                            Team1NormalTimeScore = null,
                            Team2NormalTimeScore = null,
                            Team1Withdrew = false,
                            Team2Withdrew = false
                        };

                        // Act
                        WhoWonEnum? result = eloRating.WhoWon(game);

                        // Assert
                        Assert.IsNull(result);
                    }

                    #endregion

                    #region CalculateKFactor Tests

                    [TestMethod()]
                    public void CalculateKFactor_OneGoalDifference_ReturnsBaseKFactor()
                    {
                        // Arrange
                        EloRating eloRating = new();
                        Game game = new()
                        {
                            Team1NormalTimeScore = 2,
                            Team2NormalTimeScore = 1
                        };

                        // Act
                        double result = eloRating.CalculateKFactor(game);

                        // Assert
                        Assert.AreEqual(100.0, result, "1-goal difference should return base K factor");
                    }

                    [TestMethod()]
                    public void CalculateKFactor_TwoGoalDifference_ReturnsOnePointFiveTimes()
                    {
                        // Arrange
                        EloRating eloRating = new();
                        Game game = new()
                        {
                            Team1NormalTimeScore = 3,
                            Team2NormalTimeScore = 1
                        };

                        // Act
                        double result = eloRating.CalculateKFactor(game);

                        // Assert
                        Assert.AreEqual(150.0, result, "2-goal difference should return 1.5x K factor");
                    }

                    [TestMethod()]
                    public void CalculateKFactor_ThreeGoalDifference_ReturnsTwoTimes()
                    {
                        // Arrange
                        EloRating eloRating = new();
                        Game game = new()
                        {
                            Team1NormalTimeScore = 5,
                            Team2NormalTimeScore = 2
                        };

                        // Act
                        double result = eloRating.CalculateKFactor(game);

                        // Assert
                        Assert.AreEqual(200.0, result, "3-goal difference should return 2x K factor");
                    }

                    [TestMethod()]
                    public void CalculateKFactor_FourGoalDifference_ReturnsThreePointFiveTimes()
                    {
                        // Arrange
                        EloRating eloRating = new();
                        Game game = new()
                        {
                            Team1NormalTimeScore = 6,
                            Team2NormalTimeScore = 2
                        };

                        // Act
                        double result = eloRating.CalculateKFactor(game);

                        // Assert
                        Assert.AreEqual(350.0, result, "4-goal difference should return 3.5x K factor");
                    }

                    [TestMethod()]
                    public void CalculateKFactor_FiveGoalDifference_ReturnsThreePointFiveTimes()
                    {
                        // Arrange
                        EloRating eloRating = new();
                        Game game = new()
                        {
                            Team1NormalTimeScore = 7,
                            Team2NormalTimeScore = 2
                        };

                        // Act
                        double result = eloRating.CalculateKFactor(game);

                        // Assert
                        Assert.AreEqual(350.0, result, "5+ goal difference should return 3.5x K factor");
                    }

                    [TestMethod()]
                    public void CalculateKFactor_Team2WinsByTwo_ReturnsOnePointFiveTimes()
                    {
                        // Arrange
                        EloRating eloRating = new();
                        Game game = new()
                        {
                            Team1NormalTimeScore = 1,
                            Team2NormalTimeScore = 3
                        };

                        // Act
                        double result = eloRating.CalculateKFactor(game);

                        // Assert
                        Assert.AreEqual(150.0, result, "Negative goal difference should be treated as positive for K factor");
                    }

                    #endregion

                }
            }
