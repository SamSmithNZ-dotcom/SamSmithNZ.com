using Microsoft.Extensions.Configuration;
using SamSmithNZ.Service.DataAccess.WorldCup;
using SamSmithNZ.Service.Models.WorldCup;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace SamSmithNZ.WorldCupGoals.WPF
{
    /// <summary>
    /// Interaction logic for AssignShootoutGoals.xaml
    /// </summary>
    public partial class AssignShootoutGoals : Window
    {
        private bool _bResult;
        private int _gameCode;
        private int _penaltyCode;
        private int _order;
        private readonly IConfigurationRoot _configuration;

        public AssignShootoutGoals()
        {
            InitializeComponent();

            IConfigurationBuilder config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .AddUserSecrets<Main>(true);
            _configuration = config.Build();
        }

        public async Task<bool> ShowForm(int tournamentCode, int gameCode, int penaltyCode, int iOrder)
        {
            _gameCode = gameCode;
            _penaltyCode = penaltyCode;
            _order = iOrder;

            GameDataAccess da = new(_configuration);
            List<Game> games = await da.GetListByTournament(tournamentCode);
            Game game = null;
            foreach (Game item in games)
            {
                if (item.GameCode == gameCode)
                {
                    game = item;
                    break;
                }
            }
            PlayerDataAccess da2 = new(_configuration);
            List<Player> players = await da2.GetList(gameCode);

            lblGame.Content = "#" + game.GameNumber + ": " + game.GameTime.ToString("d-MMM-yyyy hh:mm:sstt") + "   " + game.Team1Name + " vs " + game.Team2Name + ": " + Utility.GetGameScore(game);

            cboPlayer.DataContext = players;
            cboPlayer.DisplayMemberPath = "PlayerName";
            cboPlayer.SelectedValuePath = "PlayerCode";

            //We are adding a new goal to the game
            if (penaltyCode == 0)
            {
                // No action required when adding a new shootout goal; form fields are already initialized above.
            }
            else //it's an existing goal, load and populate the form
            {
                PenaltyShootoutGoalDataAccess da3 = new(_configuration);
                List<PenaltyShootoutGoal> goals = await da3.GetList(gameCode);
                PenaltyShootoutGoal goal = null;
                foreach (PenaltyShootoutGoal item in goals)
                {
                    if (item.PenaltyCode == penaltyCode)
                    {
                        goal = item;
                        break;
                    }
                }
                cboPlayer.SelectedValue = goal.PlayerCode;
                chkScored.IsChecked = goal.Scored;
            }

            //dsWorldCup.GameListForGoalAssigningDataTable dtGame = clsWCDataAccess.GameListForGoalAssigning(tournamentCode);
            //lstGames.DataContext = dtGame.DefaultView;

            cboPlayer.Focus();

            ShowDialog();
            return _bResult;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            _bResult = false;
            Close();
        }

        //private static void UpdateGoalAppearance(int playerCode, int goalTime, int extraTime, bool? isPenalty, bool? isOwnGoal)
        //{
        //    string goalText;

        //    if (playerCode > 0 && goalTime > 0)
        //    {
        //        //goalText = cboPlayer.SelectedItem.player_name;

        //        // goalText = goalText + " - " + goalTime.ToString();
        //        goalText = goalTime.ToString();

        //        if (extraTime > 0)
        //        {
        //            goalText += "+" + extraTime.ToString() + "'";
        //        }
        //        else
        //        {
        //            goalText += "'";
        //        }
        //        if (isPenalty == true)
        //        {
        //            goalText += " (pen)";
        //        }
        //        if (isOwnGoal == true)
        //        {
        //            goalText += " (og)";
        //        }
        //    }
        //}

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateSave() == true)
            {
                bool bScored = Convert.ToBoolean(chkScored.IsChecked);

                PenaltyShootoutGoal goal = new();
                goal.PenaltyCode = _penaltyCode;
                goal.GameCode = _gameCode;
                goal.PlayerCode = Convert.ToInt32(cboPlayer.SelectedValue);
                goal.PenaltyOrder = _order;
                goal.Scored = bScored;

                PenaltyShootoutGoalDataAccess da = new(_configuration);
                await da.SaveItem(goal);

                _bResult = bScored;
                Close();
            }
        }

        private bool ValidateSave()
        {
            if (Convert.ToInt32(cboPlayer.SelectedValue) == 0)
            {
                MessageBox.Show("Select player");
                return false;
            }

            if (chkScored.IsChecked == null)
            {
                MessageBox.Show("The Scored checkbox cannot be null");
                return false;
            }

            return true;
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            PenaltyShootoutGoal goal = new();
            goal.PenaltyCode = _penaltyCode;

            PenaltyShootoutGoalDataAccess da = new(_configuration);
            await da.DeleteItem(goal);

            _bResult = true;
            Close();
        }
    }
}
