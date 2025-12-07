using Microsoft.Extensions.Configuration;
using SamSmithNZ.Service.DataAccess.WorldCup;
using SamSmithNZ.Service.Models.WorldCup;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace SamSmithNZ.WorldCupGoals.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Main : Window
    {
        private readonly IConfigurationRoot _configuration;

        public Main()
        {
            InitializeComponent();

            IConfigurationBuilder config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .AddUserSecrets<Main>(true);
            _configuration = config.Build();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                btnTournamentGames.IsEnabled = false;
                //btnSetupTournamentGroups.IsEnabled = false;
                btnSetupTournamentTeams.IsEnabled = false;
                btnMigrateTournamentPlayoffs.IsEnabled = false;
                btnMigrateGames.IsEnabled = false;
                btnMigratePlayers.IsEnabled = false;

                TournamentDataAccess da = new(_configuration);
                List<Tournament> tournaments = await da.GetList(null);
                cboTournament.DataContext = tournaments;
                cboTournament.DisplayMemberPath = "TournamentName";
                cboTournament.SelectedValuePath = "TournamentCode";
                cboTournament.SelectedIndex = 0;

                btnTournamentGames.IsEnabled = true;
                //btnSetupTournamentGroups.IsEnabled = true;
                btnSetupTournamentTeams.IsEnabled = true;
                btnMigrateTournamentPlayoffs.IsEnabled = true;
                btnMigrateGames.IsEnabled = true;
                btnMigratePlayers.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void AssignGoals_Click(object sender, RoutedEventArgs e)
        {
            AssignGoals AGi = new();
            AGi.ShowDialog();
        }

        private void TournamentDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Console.WriteLine(cboTournament.SelectedValue);
        }

        private async void TournamentGames_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cboTournament.SelectedValue != null)
                {
                    Games games = new();
                    await games.ShowForm(Convert.ToInt32(cboTournament.SelectedValue));
                }
                else
                {
                    MessageBox.Show("Tournament not selected");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private async void TournamentTeams_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cboTournament.SelectedValue != null)
                {
                    Teams teams = new();
                    await teams.ShowForm(Convert.ToInt32(cboTournament.SelectedValue));
                }
                else
                {
                    MessageBox.Show("Tournament not selected");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private async void TournamentGroups_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cboTournament.SelectedValue != null)
                {
                    Groups groups = new();
                    await groups.ShowForm(Convert.ToInt32(cboTournament.SelectedValue));
                }
                else
                {
                    MessageBox.Show("Tournament not selected");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private async void MigrateTournamentPlayoffs_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cboTournament.SelectedValue != null)
                {
                    PlayoffMigration migration = new();
                    await migration.ShowForm(Convert.ToInt32(cboTournament.SelectedValue));
                }
                else
                {
                    MessageBox.Show("Tournament not selected");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private async void MigrateGames_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cboTournament.SelectedValue != null)
                {
                    GamesMigration migration = new();
                    await migration.ShowForm(Convert.ToInt32(cboTournament.SelectedValue));
                }
                else
                {
                    MessageBox.Show("Tournament not selected");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private async void MigratePlayers_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cboTournament.SelectedValue != null)
                {
                    TeamSquadsMigration migration = new();
                    await migration.ShowForm(Convert.ToInt32(cboTournament.SelectedValue));
                }
                else
                {
                    MessageBox.Show("Tournament not selected");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private async void ResetTournament_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cboTournament.SelectedValue != null)
                {
                    TeamSquadsMigration migration = new();
                    if (MessageBox.Show("Are you sure you want to delete players, games, goals, etc?", "Are you sure?", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                    {
                        TournamentDataAccess da = new(_configuration);
                        await da.ResetTournament(Convert.ToInt32(cboTournament.SelectedValue));
                    }
                }
                else
                {
                    MessageBox.Show("Tournament not selected");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

    }
}
