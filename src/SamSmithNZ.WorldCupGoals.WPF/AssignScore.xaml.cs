using Microsoft.Extensions.Configuration;
using SamSmithNZ.Service.DataAccess.WorldCup;
using SamSmithNZ.Service.Models.WorldCup;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

            // Based on these tables, we can determine which teams go into which playoff games.
            // 1. using the wc_tournament_format_playoff_setup, find the relevant teams
            // 2. teams in wc_group_stage that finished 1st and 2nd are the easiest
            // 3. Teams in wc_group_stage_third_placed_teams are next, and need the wc_tournament_third_place_matchups data to get the right team for each game

            //Get all of the data we need
            GroupDataAccess da = new(_configuration);
            List<Group> groups = await da.GetList(tournamentCode, 1);
            List<Group> thirdPlacedTeams = await da.GetList(tournamentCode, 1, "3rd");

            PlayoffDataAccess da2 = new(_configuration);
            List<Playoff> playoffSetups = await da2.GetList(tournamentCode);

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


            return true;
        }

    }
}
