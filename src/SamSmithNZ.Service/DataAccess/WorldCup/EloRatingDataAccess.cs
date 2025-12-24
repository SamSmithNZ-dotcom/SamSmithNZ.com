using Dapper;
using Microsoft.Extensions.Configuration;
using SamSmithNZ.Service.DataAccess.Base;
using SamSmithNZ.Service.DataAccess.WorldCup.Interfaces;
using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using static SamSmithNZ.Service.DataAccess.WorldCup.EloRating;

namespace SamSmithNZ.Service.DataAccess.WorldCup
{
    public class EloRatingDataAccess : BaseDataAccess<EloRating>, IEloRatingDataAccess
    {
        private readonly IConfiguration _configuration;
        public EloRatingDataAccess(IConfiguration configuration)
        {
            _configuration = configuration;
            base.SetupConnectionString(configuration);
        }

        public async Task<bool> UpdateTournamentELORatings(int tournamentCode)
        {
            GameDataAccess da = new(_configuration);
            List<Game> games = await da.GetListByTournament(tournamentCode);

            TournamentTeamDataAccess tournamentTeamDA = new(_configuration);
            List<TournamentTeam> tournamentTeams = await tournamentTeamDA.GetQualifiedTeams(tournamentCode);
            Dictionary<int, TournamentTeam> teamCache = tournamentTeams.ToDictionary(t => t.TeamCode);

            GamePreEloRatingDataAccess preEloDA = new(_configuration);
            List<Game> gamesToUpdate = new();

            foreach (Game game in games)
            {
                if (game.Team1NormalTimeScore != null || game.Team2NormalTimeScore != null)
                {
                    bool updated = await UpdateGameELORatingOptimized(tournamentCode, game, teamCache, preEloDA);
                    if (updated)
                    {
                        gamesToUpdate.Add(game);
                    }
                }
            }

            if (gamesToUpdate.Count > 0)
            {
                await BatchUpdateGames(gamesToUpdate);
            }

            if (teamCache.Count > 0)
            {
                await BatchUpdateTournamentTeams(tournamentCode, teamCache.Values.ToList());
            }

            return true;
        }

        public async Task<bool> UpdateGameELORating(int tournamentCode,
            int gameCode)
        {
            double diff = 400;

            //1. If updating a game, pass in the game code and tournament code to get the pre-elo-ratings
            GameDataAccess da = new(_configuration);
            Game game = await da.GetItem(gameCode);
            bool gamePreELORatingIsDirty = false;
            if (game.Team1PreGameEloRating == 0 || game.Team2PreGameEloRating == 0)
            {
                gamePreELORatingIsDirty = true;
            }

            //2. Retrieve the pre-ELO rating, either from the tournament or the previous game
            GamePreEloRatingDataAccess da2 = new(_configuration);
            GamePreEloRating gamePreELORating = await da2.GetGamePreELORatings(tournamentCode, game.GameCode);
            game.Team1PreGameEloRating = gamePreELORating.Team1PreELORating;
            game.Team2PreGameEloRating = gamePreELORating.Team2PreELORating;

            //3. Calculate the new ELO ratings, and save them back to the game
            List<TeamELORating> teamRatingList = new();
            TeamELORating team1 = GetTeamELORating(tournamentCode, game.Team1Code, game.Team1Name, game.Team1PreGameEloRating, teamRatingList);
            TeamELORating team2 = GetTeamELORating(tournamentCode, game.Team2Code, game.Team2Name, game.Team2PreGameEloRating, teamRatingList);
            EloRating eloRating = new();
            WhoWonEnum? result = eloRating.WhoWon(game);
            double kFactor = eloRating.CalculateKFactor(game);
            if (result != null)
            {
                (int, int) eloResult;
                //the game has started yet, we can process the game
                if (result == WhoWonEnum.Team1)
                {
                    eloResult = eloRating.GetEloRatingScoresForMatchUp((int)game.Team1PreGameEloRating, (int)game.Team2PreGameEloRating, true, false, kFactor, diff);
                    team1.Wins++;
                    team2.Losses++;
                }
                else if (result == WhoWonEnum.Team2)
                {
                    eloResult = eloRating.GetEloRatingScoresForMatchUp((int)game.Team1PreGameEloRating, (int)game.Team2PreGameEloRating, false, true, kFactor, diff);
                    team1.Losses++;
                    team2.Wins++;
                }
                else
                {
                    eloResult = eloRating.GetEloRatingScoresForMatchUp((int)game.Team1PreGameEloRating, (int)game.Team2PreGameEloRating, false, false, kFactor, diff);
                    team1.Draws++;
                    team2.Draws++;
                }
                team1.ELORating = eloResult.Item1;
                team1.GameCount++;
                team2.ELORating = eloResult.Item2;
                team2.GameCount++;
                if (gamePreELORatingIsDirty == true || game.Team1PostGameEloRating == null || game.Team2PostGameEloRating == null || game.Team1PostGameEloRating != team1.ELORating || game.Team2PostGameEloRating != team2.ELORating)
                {
                    game.Team1PostGameEloRating = team1.ELORating;
                    game.Team2PostGameEloRating = team2.ELORating;
                    await da.SaveItem(game);
                }
            }
            
            //3. Save the new ELO ratings to the database
            TournamentTeamDataAccess da3 = new(_configuration);
            TournamentTeam tournamentTeam1 = await da3.GetTournamentTeamAsync(tournamentCode, game.Team1Code);
            if (tournamentTeam1 != null && tournamentTeam1.CurrentEloRating != (int)game.Team1PostGameEloRating)
            {
                tournamentTeam1.CurrentEloRating = (int)game.Team1PostGameEloRating;
                await da3.SaveELOItem(tournamentTeam1);
            }
            TournamentTeam tournamentTeam2 = await da3.GetTournamentTeamAsync(tournamentCode, game.Team2Code);
            if (tournamentTeam2 != null && tournamentTeam2.CurrentEloRating != (int)game.Team2PostGameEloRating)
            {
                tournamentTeam2.CurrentEloRating = (int)game.Team2PostGameEloRating;
                await da3.SaveELOItem(tournamentTeam2);
            }

            //4. Push the ELO ratings to the next game
            Game? nextGameTeam1 = await da.GetNextGame(tournamentCode, gameCode, game.Team1Code);
            Game? nextGameTeam2 = await da.GetNextGame(tournamentCode, gameCode, game.Team2Code);
            if (nextGameTeam1 != null)
            {
                bool isDirty = false;
                if (nextGameTeam1.Team1Code == game.Team1Code &&
                    nextGameTeam1.Team1PreGameEloRating != team1.ELORating)
                {
                    nextGameTeam1.Team1PreGameEloRating = team1.ELORating;
                    isDirty = true;
                }
                else if (nextGameTeam1.Team2Code == game.Team1Code &&
                    nextGameTeam1.Team2PreGameEloRating != team1.ELORating)
                {
                    nextGameTeam1.Team2PreGameEloRating = team1.ELORating;
                    isDirty = true;
                }
                if (isDirty == true)
                {
                    await da.SaveItem(nextGameTeam1);
                }
            }
            if (nextGameTeam2 != null)
            {
                bool isDirty = false;
                if (nextGameTeam2.Team1Code == game.Team2Code &&
                    nextGameTeam2.Team1PreGameEloRating != team2.ELORating)
                {
                    nextGameTeam2.Team1PreGameEloRating = team2.ELORating;
                    isDirty = true;
                }
                else if (nextGameTeam2.Team2Code == game.Team2Code &&
                    nextGameTeam2.Team2PreGameEloRating != team2.ELORating)
                {
                    nextGameTeam2.Team2PreGameEloRating = team2.ELORating;
                    isDirty = true;
                }
                if (isDirty == true)
                {
                    await da.SaveItem(nextGameTeam2);
                }
            }

            return true;
        }

        //public async Task<List<TeamELORating>> CalculateEloForTournamentAsync(int tournamentCode)
        //{
        //    double diff = 400;
        //    double kFactor = 32;

        //    GameDataAccess da = new(_configuration);
        //    List<Game> gameList = await da.GetListByTournament(tournamentCode);

        //    //TeamDataAccess da2 = new(_configuration);
        //    //List<Team> teamList = await da2.GetList();

        //    TournamentTeamDataAccess da3 = new(_configuration);
        //    List<TournamentTeam> tournamentTeams = await da3.GetQualifiedTeams(tournamentCode);

        //    //Update all of the tournament team ELO ratings with starting ratings
        //    foreach (TournamentTeam tournamentTeam in tournamentTeams)
        //    {
        //        await SaveTeamELORatingAsync(tournamentCode, tournamentTeam.TeamCode, tournamentTeam.StartingEloRating);
        //    }
        //    //refresh tournament ELO ratings
        //    tournamentTeams = await da3.GetQualifiedTeams(tournamentCode);

        //    List<TeamELORating> teamRatingList = new();
        //    foreach (Game game in gameList)
        //    {
        //        int? team1StartingEloRating = GetTournamentTeamCurrentEloRanking(game.Team1Code, tournamentTeams);
        //        int? team2StartingEloRating = GetTournamentTeamCurrentEloRanking(game.Team2Code, tournamentTeams);
        //        bool gameIsDirty = false;

        //        //if (game.Team1Code == 10 || game.Team2Code == 10)
        //        //{
        //        //    System.Diagnostics.Debug.WriteLine("Game: " + game.GameNumber + ", Team1: " + game.Team1Name + ", Team1Elo: " + game.Team1PreGameEloRating + ", Team2: " + game.Team2Name + ", Team2Elo: " + game.Team2PreGameEloRating);
        //        //}

        //        if (game.Team1PreGameEloRating == team1StartingEloRating || game.Team2PreGameEloRating != team2StartingEloRating)
        //        {
        //            game.Team1PreGameEloRating = team1StartingEloRating;
        //            game.Team2PreGameEloRating = team2StartingEloRating;
        //            gameIsDirty = true;
        //        }
        //        //Calculate the ELO rating for each team, adding it to the teamRatingList object if it's not already in there
        //        //TODO: Change this so that it saves ELO updates PER game, instead of just the final ELO rating
        //        TeamELORating team1 = GetTeamELORating(tournamentCode, game.Team1Code, game.Team1Name, team1StartingEloRating, teamRatingList);
        //        TeamELORating team2 = GetTeamELORating(tournamentCode, game.Team2Code, game.Team2Name, team2StartingEloRating, teamRatingList);
        //        EloRating eloRating = new();
        //        WhoWonEnum? result = eloRating.WhoWon(game);
        //        kFactor = eloRating.CalculateKFactor(game);
        //        if (result != null)
        //        {
        //            (int, int) eloResult;
        //            //the game has started yet, we can process the game
        //            if (result == WhoWonEnum.Team1)
        //            {
        //                eloResult = eloRating.GetEloRatingScoresForMatchUp(team1.ELORating, team2.ELORating, true, false, kFactor, diff);
        //                team1.Wins++;
        //                team2.Losses++;
        //            }
        //            else if (result == WhoWonEnum.Team2)
        //            {
        //                eloResult = eloRating.GetEloRatingScoresForMatchUp(team1.ELORating, team2.ELORating, false, true, kFactor, diff);
        //                team1.Losses++;
        //                team2.Wins++;
        //            }
        //            else
        //            {
        //                eloResult = eloRating.GetEloRatingScoresForMatchUp(team1.ELORating, team2.ELORating, false, false, kFactor, diff);
        //                team1.Draws++;
        //                team2.Draws++;
        //            }
        //            team1.ELORating = eloResult.Item1;
        //            team1.GameCount++;
        //            team2.ELORating = eloResult.Item2;
        //            team2.GameCount++;
        //            if (gameIsDirty == true || game.Team1PostGameEloRating != team1.ELORating || game.Team2PostGameEloRating != team2.ELORating)
        //            {
        //                game.Team1PostGameEloRating = team1.ELORating;
        //                game.Team2PostGameEloRating = team2.ELORating;
        //                SetTournamentTeamCurrentEloRanking(game.Team1Code, tournamentTeams, team1.ELORating);
        //                SetTournamentTeamCurrentEloRanking(game.Team2Code, tournamentTeams, team2.ELORating);
        //                await da.SaveItem(game);
        //            }
        //        }
        //    }

        //    //Sort the teams
        //    teamRatingList.Sort((x, y) => y.ELORating.CompareTo(x.ELORating));

        //    //Update the team ratings 
        //    //TournamentTeamDataAccess da3 = new TournamentTeamDataAccess();
        //    //da3.SaveItem();

        //    return teamRatingList;
        //}

        //private static int GetTournamentTeamCurrentEloRanking(int teamCode, List<TournamentTeam> tournamentTeams)
        //{
        //    int result = 0;
        //    foreach (TournamentTeam item in tournamentTeams)
        //    {
        //        if (item.TeamCode == teamCode)
        //        {
        //            result = item.CurrentEloRating;
        //            break;
        //        }
        //    }
        //    return result;
        //}

        //private static bool SetTournamentTeamCurrentEloRanking(int teamCode, List<TournamentTeam> tournamentTeams, int eloRating)
        //{
        //    foreach (TournamentTeam item in tournamentTeams)
        //    {
        //        if (item.TeamCode == teamCode)
        //        {
        //            item.CurrentEloRating = eloRating;
        //            break;
        //        }
        //    }
        //    return true;
        //}

        public async Task<bool> SaveTeamELORatingAsync(int tournamentCode, int teamCode, int eloRating)
        {
            DynamicParameters parameters = new();
            parameters.Add("@TournamentCode", tournamentCode, DbType.Int32);
            parameters.Add("@TeamCode", teamCode, DbType.Int32);
            parameters.Add("@ELORating", eloRating, DbType.Int32);

            return await base.SaveItem("FB_SaveTournamentTeamELORating", parameters);
        }

                private static TeamELORating GetTeamELORating(int tournamentCode, int teamCode, string teamName, int? currentELORanking, List<TeamELORating> teamList)
                {
                    TeamELORating existingTeam = teamList.FirstOrDefault(item => item.TeamCode == teamCode);
                    if (existingTeam != null)
                    {
                        return existingTeam;
                    }

                    if (currentELORanking < 500 || currentELORanking == null)
                    {
                        currentELORanking = 1000;
                    }
                    TeamELORating newTeam = new(tournamentCode, teamCode, teamName, (int)currentELORanking);
                    teamList.Add(newTeam);
                    return newTeam;
                }

                private async Task<bool> UpdateGameELORatingOptimized(int tournamentCode, Game game, 
                    Dictionary<int, TournamentTeam> teamCache, GamePreEloRatingDataAccess preEloDA)
                {
                    double diff = 400;

                    bool gamePreELORatingIsDirty = false;
                    if (game.Team1PreGameEloRating == 0 || game.Team2PreGameEloRating == 0)
                    {
                        gamePreELORatingIsDirty = true;
                    }

                    GamePreEloRating gamePreELORating = await preEloDA.GetGamePreELORatings(tournamentCode, game.GameCode);
                    game.Team1PreGameEloRating = gamePreELORating.Team1PreELORating;
                    game.Team2PreGameEloRating = gamePreELORating.Team2PreELORating;

                    List<TeamELORating> teamRatingList = new();
                    TeamELORating team1 = GetTeamELORating(tournamentCode, game.Team1Code, game.Team1Name, game.Team1PreGameEloRating, teamRatingList);
                    TeamELORating team2 = GetTeamELORating(tournamentCode, game.Team2Code, game.Team2Name, game.Team2PreGameEloRating, teamRatingList);
                    EloRating eloRating = new();
                    WhoWonEnum? result = eloRating.WhoWon(game);
                    double kFactor = eloRating.CalculateKFactor(game);
                    if (result != null)
                    {
                        (int, int) eloResult;
                        if (result == WhoWonEnum.Team1)
                        {
                            eloResult = eloRating.GetEloRatingScoresForMatchUp((int)game.Team1PreGameEloRating, (int)game.Team2PreGameEloRating, true, false, kFactor, diff);
                            team1.Wins++;
                            team2.Losses++;
                        }
                        else if (result == WhoWonEnum.Team2)
                        {
                            eloResult = eloRating.GetEloRatingScoresForMatchUp((int)game.Team1PreGameEloRating, (int)game.Team2PreGameEloRating, false, true, kFactor, diff);
                            team1.Losses++;
                            team2.Wins++;
                        }
                        else
                        {
                            eloResult = eloRating.GetEloRatingScoresForMatchUp((int)game.Team1PreGameEloRating, (int)game.Team2PreGameEloRating, false, false, kFactor, diff);
                            team1.Draws++;
                            team2.Draws++;
                        }
                        team1.ELORating = eloResult.Item1;
                        team1.GameCount++;
                        team2.ELORating = eloResult.Item2;
                        team2.GameCount++;

                        bool needsUpdate = gamePreELORatingIsDirty || game.Team1PostGameEloRating == null || 
                            game.Team2PostGameEloRating == null || game.Team1PostGameEloRating != team1.ELORating || 
                            game.Team2PostGameEloRating != team2.ELORating;

                        if (needsUpdate)
                        {
                            game.Team1PostGameEloRating = team1.ELORating;
                            game.Team2PostGameEloRating = team2.ELORating;

                            if (teamCache.TryGetValue(game.Team1Code, out TournamentTeam tournamentTeam1))
                            {
                                tournamentTeam1.CurrentEloRating = team1.ELORating;
                            }
                            if (teamCache.TryGetValue(game.Team2Code, out TournamentTeam tournamentTeam2))
                            {
                                tournamentTeam2.CurrentEloRating = team2.ELORating;
                            }

                            return true;
                        }
                    }

                    return false;
                }

                private async Task<bool> BatchUpdateGames(List<Game> games)
                {
                    GameDataAccess da = new(_configuration);
                    foreach (Game game in games)
                    {
                        await da.SaveItem(game);
                    }
                    return true;
                }

                private async Task<bool> BatchUpdateTournamentTeams(int tournamentCode, List<TournamentTeam> teams)
                {
                    TournamentTeamDataAccess da = new(_configuration);
                    foreach (TournamentTeam team in teams)
                    {
                        await da.SaveELOItem(team);
                    }
                    return true;
                }

            }
        }
