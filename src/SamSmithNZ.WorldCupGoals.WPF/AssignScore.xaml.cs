using Microsoft.Extensions.Configuration;
using SamSmithNZ.Service.DataAccess.WorldCup;
using SamSmithNZ.Service.Models.WorldCup;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Group = SamSmithNZ.Service.Models.WorldCup.Group;

namespace SamSmithNZ.WorldCupGoals.WPF
{
    /// <summary>
    /// Interaction logic for AssignScore.xaml
    /// </summary>
    public partial class AssignScore : Window
    {
        private bool _bResult;
        private Service.Models.WorldCup.Game _game;
        private readonly IConfigurationRoot _configuration;

        public AssignScore()
        {
            InitializeComponent();

            IConfigurationBuilder config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .AddUserSecrets<Main>(true);
            _configuration = config.Build();
        }

        public async Task<bool> ShowForm(int gameCode)
        {
            GameDataAccess da = new(_configuration);
            _game = await da.GetItem(gameCode);

            lblGameHeader.Content = "#" + _game.GameNumber + ": " + _game.GameTime.ToString("d-MMM-yyyy hh:mm:sstt");
            lblGame.Content = _game.Team1Name + " vs " + _game.Team2Name;
            lblStatus.Content = "";

            txtTeam1NormalTime.Text = _game.Team1NormalTimeScore.ToString();
            txtTeam1ExtraTime.Text = _game.Team1ExtraTimeScore.ToString();
            txtTeam1Penalties.Text = _game.Team1PenaltiesScore.ToString();
            txtTeam2NormalTime.Text = _game.Team2NormalTimeScore.ToString();
            txtTeam2ExtraTime.Text = _game.Team2ExtraTimeScore.ToString();
            txtTeam2Penalties.Text = _game.Team2PenaltiesScore.ToString();

            txtTeam1NormalTime.Focus();

            //dsWorldCup.GameListForGoalAssigningDataTable dtGame = clsWCDataAccess.GameListForGoalAssigning(tournamentCode);
            //lstGames.DataContext = dtGame.DefaultView;

            ShowDialog();
            return _bResult;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            _bResult = false;
            Close();
        }

        private void TextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (e.KeyboardDevice.IsKeyDown(Key.Tab))
            {
                ((TextBox)sender).SelectAll();
            }
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ValidateSave() == true)
                {
                    btnSave.IsEnabled = false;
                    Cancel.IsEnabled = false;
                    lblStatus.Content = "Saving game...";
                    if (txtTeam1NormalTime.Text.Length == 0 || txtTeam2NormalTime.Text.Length == 0)
                    {
                        _game.Team1NormalTimeScore = null;
                        _game.Team2NormalTimeScore = null;
                        _game.Team1ExtraTimeScore = null;
                        _game.Team2ExtraTimeScore = null;
                        _game.Team1PenaltiesScore = null;
                        _game.Team2PenaltiesScore = null;
                    }
                    else
                    {
                        _game.Team1NormalTimeScore = Convert.ToInt32(txtTeam1NormalTime.Text);
                        _game.Team2NormalTimeScore = Convert.ToInt32(txtTeam2NormalTime.Text);
                        if (txtTeam1ExtraTime.Text.Length > 0 || txtTeam2ExtraTime.Text.Length > 0)
                        {
                            _game.Team1ExtraTimeScore = Convert.ToInt32(txtTeam1ExtraTime.Text);
                            _game.Team2ExtraTimeScore = Convert.ToInt32(txtTeam2ExtraTime.Text);
                        }
                        else
                        {
                            _game.Team1ExtraTimeScore = null;
                            _game.Team2ExtraTimeScore = null;
                        }
                        if (txtTeam1Penalties.Text.Length > 0 || txtTeam2Penalties.Text.Length > 0)
                        {
                            _game.Team1PenaltiesScore = Convert.ToInt32(txtTeam1Penalties.Text);
                            _game.Team2PenaltiesScore = Convert.ToInt32(txtTeam2Penalties.Text);
                        }
                        else
                        {
                            _game.Team1PenaltiesScore = null;
                            _game.Team2PenaltiesScore = null;
                        }
                    }

                    GameDataAccess da = new(_configuration);
                    ////Get the ELO rating updates
                    //EloRating eloRating = new();
                    //WhoWonEnum? whoWonGame = eloRating.WhoWon(_game);
                    //double kFactor = eloRating.CalculateKFactor(_game);
                    //(int, int) newEloRatings = eloRating.GetEloRatingScoresForMatchUp((int)_game.Team1PreGameEloRating,
                    //    (int)_game.Team2PreGameEloRating,
                    //    whoWonGame == WhoWonEnum.Team1,
                    //    whoWonGame == WhoWonEnum.Team2,
                    //    kFactor);
                    //_game.Team1PostGameEloRating = newEloRatings.Item1;
                    //_game.Team2PostGameEloRating = newEloRatings.Item2;
                    //Save the game
                    await da.SaveItem(_game);

                    if (_game.RoundNumber == 1)
                    {

                        //Now update the playoff games, if needed.
                        //Pull from wc_group_stage_third_placed_teams
                        //Use the match combinations in [FB_GetTournamentThirdPlacedMatchups]
                        lblStatus.Content = "Updating playoff matchups...";
                        await SetupPlayoffs(_game.TournamentCode);
                    }

                    lblStatus.Content = "Updating ELO ratings...";
                    EloRatingDataAccess daELO = new(_configuration);
                    //await daELO.UpdateTournamentELORatings(_game.TournamentCode);
                    await daELO.UpdateGameELORating(_game.TournamentCode, _game.GameCode);

                    lblStatus.Content = "Game updated...";
                    _bResult = true;
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                btnSave.IsEnabled = true;
                Cancel.IsEnabled = true;
            }
        }

        private bool ValidateSave()
        {
            int result;
            if (txtTeam1NormalTime.Text.Length == 0 && txtTeam2NormalTime.Text.Length == 0)
            {
                if (MessageBox.Show("Are you sure you want to remove this result?", "Remove result?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if (int.TryParse(txtTeam1NormalTime.Text, out result) == false)
            {
                MessageBox.Show("Enter a valid team 1 normal time score");
                Debug.WriteLine(result);
                return false;
            }
            if (int.TryParse(txtTeam2NormalTime.Text, out result) == false)
            {
                MessageBox.Show("Enter a valid team 2 normal time score");
                return false;
            }
            if (txtTeam1ExtraTime.Text.Length > 0 && int.TryParse(txtTeam1ExtraTime.Text, out result) == false)
            {
                MessageBox.Show("Enter a valid team 1 extra time score");
                return false;
            }
            if (txtTeam2ExtraTime.Text.Length > 0 && int.TryParse(txtTeam2ExtraTime.Text, out result) == false)
            {
                MessageBox.Show("Enter a valid team 2 extra time score");
                return false;
            }
            if (txtTeam1Penalties.Text.Length > 0 && int.TryParse(txtTeam1Penalties.Text, out result) == false)
            {
                MessageBox.Show("Enter a valid team 1 penalties score");
                return false;
            }
            if (txtTeam2Penalties.Text.Length > 0 && int.TryParse(txtTeam2Penalties.Text, out result) == false)
            {
                MessageBox.Show("Enter a valid team 2 penalties score");
                return false;
            }
            Debug.WriteLine(result);

            return true;
        }
        private async Task<bool> SetupPlayoffs(int tournamentCode)
        {
            //We need to pull information from a couple tables:
            // - wc_group_stage: The groups with the teams and their finishing positions
            // - wc_tournament_format_playoff_setup: The playoff setup info, which teams are coming from which groups
            // - wc_group_stage_third_placed_teams: The position of the 3rd placed teams from the groups.
            // - wc_tournament_third_place_matchups: has the 3rd place playoff info, what 8 teams who finished 3rd in groups go into which playoff game

            //Get all of the data we need
            GroupDataAccess da = new(_configuration);
            List<Group> groups = await da.GetList(tournamentCode, 1);
            List<Group> thirdPlacedTeams = await da.GetList(tournamentCode, 1, "3rd");

            PlayoffDataAccess da2 = new(_configuration);
            List<Playoff> playoffs = await da2.GetList(tournamentCode);

            ThirdPlaceMatchDataAccess da3 = new(_configuration);
            StringBuilder groupsInTop8 = new();
            int i = 0;
            foreach (Group group in thirdPlacedTeams)
            {
                i++;
                groupsInTop8.Append(group.RoundCode);
                if (i < 8)
                {
                    groupsInTop8.Append(',');
                }
                if (i >= 8)
                {
                    break;
                }
            }
            List<ThirdPlaceMatch> thirdPlacedMatches = await da3.GetList(tournamentCode, groupsInTop8.ToString());

            GameDataAccess da4 = new(_configuration);
            List<Game> playoffGames = await da4.GetListByPlayoff(tournamentCode, 2, false);

            // Based on this data, we can determine which teams go into which playoff games.
            // 1. using the playoffSetups, find the teams we need from each group, using the "groups" list to find teams that finished 1st and 2nd first
            // 2. Teams in thirdPlacedMatches are next, to get the last 8 team for each playoffs
            foreach (Playoff playoff in playoffs)
            {
                if (playoff.RoundCode == "32")
                {
                    //Get the playoff game
                    Game playoffGame = GetPlayoffGame(playoffGames, playoff.GameNumber);

                    //Get the ancestors
                    string team1Prereq = playoff.Team1Prereq.Replace("Group ", "").Replace(" place finisher", "");
                    string[] team1Ancestor = team1Prereq.Split(' ');
                    string team2Prereq = playoff.Team2Prereq.Replace("Group ", "").Replace(" place finisher", "");
                    string[] team2Ancestor = team2Prereq.Split(' ');

                    //Find the team codes
                    int team1Code = 0;
                    if (team1Ancestor[1] == "3")
                    {
                        //Note that we are using the OTHER teams group in the first parameter - that isn't a typo
                        team1Code = GetThirdPlacedTeamCode(team2Ancestor[0], team2Ancestor[1], team1Ancestor[1], groups, thirdPlacedMatches);
                    }
                    else
                    {
                        team1Code = GetGroupTeamCode(team1Ancestor[0], team1Ancestor[1], groups);
                    }
                    int team2Code = 0;
                    if (team2Ancestor[1] == "3")
                    {
                        //Note that we are using the OTHER teams group in the first parameter - that isn't a typo
                        team2Code = GetThirdPlacedTeamCode(team1Ancestor[0], team1Ancestor[1], team2Ancestor[1], groups, thirdPlacedMatches);
                    }
                    else
                    {
                        team2Code = GetGroupTeamCode(team2Ancestor[0], team2Ancestor[1], groups);
                    }
                    bool updateGame = false;
                    if (team1Code > 0 && playoffGame.Team1Code != team1Code)
                    {
                        playoffGame.Team1Code = team1Code;
                        updateGame = true;
                    }
                    if (team2Code > 0 && playoffGame.Team2Code != team2Code)
                    {
                        playoffGame.Team2Code = team2Code;
                        updateGame = true;
                    }
                    if (updateGame)
                    {
                        //save the game
                        await da4.SaveItem(playoffGame);
                    }
                }
            }

            return true;
        }

        private Game GetPlayoffGame(List<Game> playoffGames, int gameNumber)
        {
            foreach (Game game in playoffGames)
            {
                if (game.GameNumber == gameNumber)
                {
                    return game;
                }
            }
            return null;
        }

        private int GetGroupTeamCode(string groupCode, string groupOrder, List<Group> groups)
        {
            int groupRanking = int.Parse(groupOrder);
            foreach (Group group in groups)
            {
                if (group.RoundCode == groupCode && group.GroupRanking == groupRanking)
                {
                    return group.TeamCode;
                }
            }
            return 0;
        }

        private int GetThirdPlacedTeamCode(string groupCode, string groupOrder, string thirdGroupOrder, List<Group> groups, List<ThirdPlaceMatch> thirdPlacedMatches)
        {
            foreach (ThirdPlaceMatch thirdPlaceMatch in thirdPlacedMatches)
            {
                // the format is typically 1E, reversed here from normal
                if (thirdPlaceMatch.WinnerMatchCode == groupOrder + groupCode)
                {
                    string thirdPlaceGroup = thirdPlaceMatch.ThirdPlaceGroup;
                    return GetGroupTeamCode(thirdPlaceGroup, thirdGroupOrder, groups);
                }
            }
            return 0;
        }

    }
}
