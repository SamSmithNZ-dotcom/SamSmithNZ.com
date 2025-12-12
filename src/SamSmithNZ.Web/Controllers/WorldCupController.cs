using Microsoft.AspNetCore.Mvc;
using SamSmithNZ.Service.Models.WorldCup;
using SamSmithNZ.Web.Models.WorldCup;
using SamSmithNZ.Web.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Web.Controllers
{
    public class WorldCupController : Controller
    {
        private readonly IWorldCupServiceApiClient _ServiceApiClient;

        public WorldCupController(IWorldCupServiceApiClient ServiceApiClient)
        {
            _ServiceApiClient = ServiceApiClient;
        }

        public async Task<IActionResult> Index(int? competitionCode = null)
        {
            //competitionCode = 3;
            List<Tournament> tournaments = await _ServiceApiClient.GetTournaments(competitionCode);
            List<TournamentImportStatus> tournamentsImportStatus = await _ServiceApiClient.GetTournamentsImportStatus(competitionCode);

            IndexViewModel indexViewModel = new()
            {
                Tournaments = tournaments,
                TournamentsImportStatus = tournamentsImportStatus
            };

            return View(indexViewModel);
        }

        public async Task<IActionResult> Tournament(int tournamentCode)
        {
            Tournament tournament = await _ServiceApiClient.GetTournament(tournamentCode);
            bool isPlacingTeams = true;
            List<TournamentTeam> teams = await _ServiceApiClient.GetTournamentPlacingTeams(tournamentCode);
            if (teams.Count == 0)
            {
                isPlacingTeams = false;
                teams = await _ServiceApiClient.GetTournamentQualifyingTeams(tournamentCode);
            }
            List<TournamentTopGoalScorer> goals = await _ServiceApiClient.GetTournamentTopGoalScorers(tournamentCode, false);
            List<TournamentTopGoalScorer> ownGoals = await _ServiceApiClient.GetTournamentTopGoalScorers(tournamentCode, true);
            TournamentViewModel tournamentViewModel = new()
            {
                Tournament = tournament,
                IsPlacingTeams = isPlacingTeams,
                Teams = teams,
                Goals = goals,
                OwnGoals = ownGoals
            };

            if (teams.Count > 0 && teams[0].ChanceToWin > 0)
            {
                tournamentViewModel.ChanceToWin = true;
            }

            return View(tournamentViewModel);
        }

        //public async Task<IActionResult> GroupOld(int tournamentCode, int roundNumber, string roundCode)
        //{
        //    List<GroupCode> groupCodes = await _ServiceApiClient.GetGroupCodes(tournamentCode, roundNumber);
        //    if (string.IsNullOrEmpty(roundCode) == true && groupCodes.Count > 0)
        //    {
        //        roundCode = groupCodes[0].RoundCode;
        //    }
        //    List<Group> groups = await _ServiceApiClient.GetGroups(tournamentCode, roundNumber, roundCode);
        //    bool teamWithDrew = false;
        //    foreach (Group item in groups)
        //    {
        //        if (item.TeamWithdrew == true)
        //        {
        //            teamWithDrew = true;
        //            break;
        //        }
        //    }
        //    List<Game> games = await _ServiceApiClient.GetGames(tournamentCode, roundNumber, roundCode);

        //    GroupViewModel groupViewModel = new(groupCodes)
        //    {
        //        TournamentCode = tournamentCode,
        //        RoundNumber = roundNumber,
        //        RoundCode = roundCode,
        //        Groups = groups,
        //        TeamWithdrew = teamWithDrew,
        //        Games = games
        //    };

        //    return View(groupViewModel);
        //}

        public async Task<IActionResult> Group(int tournamentCode, int roundNumber, string roundCode)
        {
            List<GroupCode> groupCodes = await _ServiceApiClient.GetGroupCodes(tournamentCode, roundNumber);
            if (string.IsNullOrEmpty(roundCode) == true && groupCodes.Count > 0)
            {
                roundCode = groupCodes[0].RoundCode;
            }
            List<Group> groups = await _ServiceApiClient.GetGroups(tournamentCode, roundNumber, null);
            bool teamWithDrew = false;
            foreach (Group item in groups)
            {
                if (item.TeamWithdrew == true)
                {
                    teamWithDrew = true;
                    break;
                }
            }
            List<Game> games = await _ServiceApiClient.GetGames(tournamentCode, roundNumber, null, true);

            GroupViewModel groupViewModel = new(groupCodes)
            {
                TournamentCode = tournamentCode,
                RoundNumber = roundNumber,
                RoundCode = roundCode,
                Groups = groups,
                TeamWithdrew = teamWithDrew,
                Games = games
            };

            return View(groupViewModel);
        }

        [HttpPost]
        public IActionResult GroupPost(int tournamentCode, int roundNumber, string roundCode)
        {
            return RedirectToAction("Group",
                new
                {
                    tournamentCode = tournamentCode,
                    roundNumber = roundNumber,
                    roundCode = roundCode
                });
        }

        public async Task<IActionResult> PlayoffsOld(int tournamentCode, int roundNumber)
        {
            List<Game> games = await _ServiceApiClient.GetPlayoffGames(tournamentCode, roundNumber, true);

            PlayoffsViewModel playoffsViewModel = new()
            {
                Games = games
            };

            return View(playoffsViewModel);
        }

        public async Task<IActionResult> Playoffs(int tournamentCode, int roundNumber)
        {
            List<Playoff> playoffs = await _ServiceApiClient.GetPlayoffSetup(tournamentCode);
            List<Game> games = await _ServiceApiClient.GetPlayoffGames(tournamentCode, roundNumber, true);

            PlayoffsViewModel playoffsViewModel = new()
            {
                Playoffs = playoffs,
                Games = games
            };

            if (games.Count == 32)
            {
                // Redirect to another action (same controller)
                return RedirectToAction("Playoffs32Teams", "WorldCup", new { tournamentCode = tournamentCode, roundNumber = roundNumber });
            }
            else
            {
                return View(playoffsViewModel);
            }
        }

        public async Task<IActionResult> Playoffs32Teams(int tournamentCode, int roundNumber)
        {
            List<Playoff> playoffs = await _ServiceApiClient.GetPlayoffSetup(tournamentCode);
            List<Game> games = await _ServiceApiClient.GetPlayoffGames(tournamentCode, roundNumber, true);

            PlayoffsViewModel playoffsViewModel = new()
            {
                Playoffs = playoffs,
                Games = games
            };

            return View(playoffsViewModel);
        }

        public async Task<IActionResult> Team(int teamCode)
        {
            //Team team = await _ServiceApiClient.GetTeam(teamCode);
            //List<Game> games = await _ServiceApiClient.GetGamesByTeam(teamCode);
            TeamStatistics teamStatistics = await _ServiceApiClient.GetTeamStatistics(teamCode);

            TeamViewModel teamViewModel = new(teamStatistics);

            return View(teamViewModel);
        }

        public async Task<IActionResult> Matchup(int team1Code, int team2Code)
        {
            TeamMatchup teamMatchup = await _ServiceApiClient.GetTeamMatchup(team1Code, team2Code);
            return View(teamMatchup);
        }

        public async Task<IActionResult> Stats(int tournamentCode)
        {
            int competitionCode = 1;
            List<StatsAverageTournamentGoals> goalsPerGame = await _ServiceApiClient.GetStatsAverageTournamentGoalsList(competitionCode);

            StatsViewModel statsViewModel = new()
            {
                TournamentCode = tournamentCode,
                StatsAverageTournamentGoalsList = goalsPerGame
            };

            return View(statsViewModel);
        }

        public IActionResult ELORating() //int tournamentCode)
        {
            //SamSmithNZ2015.Core.WorldCup.DataAccess.GameDataAccess da = new SamSmithNZ2015.Core.WorldCup.DataAccess.GameDataAccess();
            //List<Game> gameList = da.GetItems(tournamentCode);

            //ELORatingManager api = new ELORatingManager();
            //List<TeamRating> teamRatingList = api.ProcessTeams(gameList);
            //return View(teamRatingList);

            return View();
        }

        [HttpPost]
        public IActionResult WCOddsPost(string maxRange = "", bool chkShowActive = true, bool chkShowEliminated = false)
        {
            return View();
            //return Redirect("https://samsmithnz2015.azurewebsites.net/WorldCup/WCOddsPost?maxRange=" + maxRange + "&chkShowActive=" + chkShowActive + "&chkShowEliminated=" + chkShowEliminated);


            ////return View(WCOdds(maxRange, chkShowActive, chkShowEliminated));
            //return RedirectToAction("Index", "FootballPool", new { maxRange = maxRange, chkShowActive = chkShowActive, chkShowEliminated = chkShowEliminated });
        }

        public IActionResult WCOdds(string maxRange = "", bool showActive = true, bool showEliminated = false, int tournamentCode = 20)
        {
            return View();
            //return Redirect("https://samsmithnz2015.azurewebsites.net/WorldCup/WCOdds?maxRange=" + maxRange + "&showActive=" + showActive + "&showEliminated=" + showEliminated + "&tournamentCode=" + tournamentCode);

            ////Scrap the odds
            //SamSmithNZ2015.Core.WorldCup.DataAccess.ImportGameOddsDataAccess da = new SamSmithNZ2015.Core.WorldCup.DataAccess.ImportGameOddsDataAccess();
            //List<ImportGameOdds> apiGameResults = new List<ImportGameOdds>();
            ////if (tournamentCode == 20)
            ////{
            ////    WCAPIManager apiManager = new WCAPIManager();
            ////    apiGameResults = apiManager.GetAPIWCOdds();
            ////    //Save the days odds extraction
            ////    if (DateTime.Now.TimeOfDay.Hours < 12)
            ////    {
            ////        foreach (ImportGameOdds game in apiGameResults)
            ////        {
            ////            //make sure we pass the tournament code too (fixes a bug that saves data to a 0 tournament code)
            ////            game.TournamentCode = tournamentCode;
            ////            Console.WriteLine(tournamentCode);
            ////            da.Commit(game);
            ////        }
            ////        da.NormalizeOdds(DateTime.Now);
            ////    }            
            ////}
            //da.AddMissingOdds(DateTime.Now, tournamentCode);
            //apiGameResults = new List<ImportGameOdds>();
            //apiGameResults.AddRange(da.GetItems(tournamentCode, DateTime.Now));

            ////Get odds difference for each team from database
            //SamSmithNZ2015.Core.WorldCup.DataAccess.ImportGameOddsMinMaxDataAccess da2 = new SamSmithNZ2015.Core.WorldCup.DataAccess.ImportGameOddsMinMaxDataAccess();
            //List<ImportGameOddsMinMax> result2 = da2.GetItems(tournamentCode);
            //foreach (ImportGameOdds game in apiGameResults)
            //{
            //    foreach (ImportGameOddsMinMax game2 in result2)
            //    {
            //        if (game.TeamName == game2.TeamName)
            //        {
            //            game.OddsDifference = game2.OddsDifference;
            //            break;
            //        }
            //    }
            //    if (game.OddsMin > 0)
            //    {
            //        game.OddsMin = 1 / game.OddsMin;
            //    }
            //    if (game.OddsMax > 0)
            //    {
            //        game.OddsMax = 1 / game.OddsMax;
            //    }
            //    if (game.OddsMean > 0)
            //    {
            //        game.OddsMean = 1 / game.OddsMean;
            //    }
            //    game.OddsStandardDeviation = game.OddsStandardDeviation;
            //    game.TournamentCode = tournamentCode;
            //}

            ////Sort List by Probability
            //apiGameResults.Sort(delegate (ImportGameOdds p1, ImportGameOdds p2)
            //{
            //    return p2.OddsProbability.CompareTo(p1.OddsProbability);
            //});

            //if (maxRange == "")
            //{
            //    maxRange = "100";
            //}

            ////HtmlString Chance100 = CreateOddsGraphData(1);
            ////HtmlString Chance6 = CreateOddsGraphData(0.06);
            ////HtmlString Chance2 = CreateOddsGraphData(0.02);
            //HtmlString chance = CreateOddsGraphData(Convert.ToDouble(maxRange) / 100, showActive, showEliminated, tournamentCode);

            //return View(new SamSmithNZ2015.Models.WorldCup.OddsViewModel(apiGameResults, chance, maxRange, showActive, showEliminated));
        }

        //From https://github.com/sghall/d3-multi-series-charts       
        public IActionResult WCOddsGraph(int tournamentCode = 20)
        {
            return View();
            //return Redirect("https://samsmithnz2015.azurewebsites.net/WorldCup/WCOddsGraph?tournamentCode=" + tournamentCode);


            //return View(CreateOddsGraphData(0.02, true, true, tournamentCode));
        }

        //private System.Web.HtmlString CreateOddsGraphData(double oddsLimit, bool chkShowActive, bool chkShowEliminated, int tournamentCode)
        //{
        //    return Redirect("https://samsmithnz2015.azurewebsites.net/WorldCup/Index");

        //    //SamSmithNZ2015.Core.WorldCup.DataAccess.ImportGameOddsDataAccess da = new SamSmithNZ2015.Core.WorldCup.DataAccess.ImportGameOddsDataAccess();
        //    //List<ImportGameOdds> games;
        //    //if (oddsLimit > 0)
        //    //{
        //    //    games = da.GetItems(oddsLimit, chkShowActive, chkShowEliminated, tournamentCode);
        //    //}
        //    //else
        //    //{
        //    //    games = da.GetItems(tournamentCode);
        //    //}
        //    ////Build the header
        //    //StringBuilder headerRow = new();
        //    //headerRow.Append("\"quarter\",");

        //    ////for (int i = 0; i < 32; i++)
        //    ////{
        //    ////    ImportGameOdds item = games[i];
        //    ////}
        //    //foreach (string item in games.Select(x => x.TeamName).Distinct()) //Get a distinct list of team names
        //    //{
        //    //    headerRow.Append("\"");
        //    //    headerRow.Append(item);
        //    //    headerRow.Append("\"");
        //    //    headerRow.Append(",");
        //    //}

        //    ////Build the body
        //    //List<string> rows = new();
        //    //DateTime currentDate = DateTime.MinValue;
        //    //StringBuilder row = new();

        //    //foreach (ImportGameOdds item in games)
        //    //{
        //    //    if (currentDate != item.Date)
        //    //    {
        //    //        if (currentDate != DateTime.MinValue)
        //    //        {
        //    //            rows.Add(row.ToString().TrimEnd(','));
        //    //            row = new();
        //    //        }
        //    //        row.Append("\"");
        //    //        row.Append(item.Date.ToString("dd-MMM"));
        //    //        row.Append("\"");
        //    //        row.Append(",");
        //    //        currentDate = item.Date;
        //    //    }
        //    //    row.Append("\"");
        //    //    row.Append((item.OddsProbability * 100).ToString("0.00"));
        //    //    row.Append("\"");
        //    //    row.Append(",");
        //    //}
        //    //rows.Add(row.ToString().TrimEnd(','));

        //    ////Join it all together
        //    //List<string> finalCSVRows = new();
        //    //finalCSVRows.Add(headerRow.ToString().TrimEnd(','));
        //    //finalCSVRows.AddRange(rows);

        //    ////Create the CSV/JS Array
        //    //StringBuilder sbJSArray = new();
        //    //for (int i = 0; i <= finalCSVRows.Count - 1; i++)
        //    //{
        //    //    //sbJSArray.Append("[");
        //    //    sbJSArray.Append(finalCSVRows[i]);
        //    //    //sbJSArray.Append("]");
        //    //    if (i < finalCSVRows.Count - 1)
        //    //    {
        //    //        sbJSArray.Append(",");
        //    //    }
        //    //    else
        //    //    {
        //    //        sbJSArray.Append(Environment.NewLine);
        //    //    }
        //    //}

        //    ////Write out the CSV rows
        //    //StringBuilder csvAnother = new();
        //    //System.Diagnostics.Debug.WriteLine("Starting " + oddsLimit + " max chance");
        //    //foreach (string item in finalCSVRows)
        //    //{
        //    //    System.Diagnostics.Debug.WriteLine(item.Replace("\"", ""));
        //    //    csvAnother.Append(item.Replace("\"", ""));
        //    //    csvAnother.Append(Environment.NewLine);
        //    //}

        //    //string fileName = "Data_" + tournamentCode.ToString("000") + "_" + DateTime.Now.ToString("ddMMM") + "_" + oddsLimit.ToString("0.00").Replace(".", "") + "_" + chkShowActive + "_" + chkShowEliminated + ".csv";
        //    //string path = System.IO.Path.Combine(Server.MapPath("~/D3Content/"), fileName);

        //    //System.IO.StreamWriter objWriter;
        //    //objWriter = new System.IO.StreamWriter(path);
        //    //objWriter.Write(csvAnother.ToString());
        //    //objWriter.Close();

        //    //return new System.Web.HtmlString(fileName);
        //}

        public IActionResult About()
        {
            return RedirectToAction("About", "Home");
        }

        public async Task<IActionResult> Insights()
        {
            List<GoalInsight> regularTimeGoalInsights = await _ServiceApiClient.GetGoalInsights(false);
            List<GoalInsight> extraTimeGoalInsights = await _ServiceApiClient.GetGoalInsights(true);

            InsightsViewModel insightsViewModel = new()
            {
                RegularTimeGoalInsights = regularTimeGoalInsights,
                ExtraTimeGoalInsights = extraTimeGoalInsights
            };

            return View(insightsViewModel);
        }

    }
}