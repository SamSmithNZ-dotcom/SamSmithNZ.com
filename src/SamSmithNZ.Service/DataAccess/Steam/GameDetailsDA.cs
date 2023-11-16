﻿using SamSmithNZ.Service.Models.Steam;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.Steam
{
    public class GameDetailsDA
    {
        public async Task<GameDetail> GetDataAsync(string steamID, string appID, bool getStats = true, string achievementToSearch = null)
        {
            SteamGameDetailDA da = new();
            SteamGameDetail gameDetail = await da.GetDataAsync(appID);

            //Get the list of owned games for the user
            SteamOwnedGamesDA da2 = new();
            SteamOwnedGames gameOwnedGames = await da2.GetDataAsync(steamID);

            //Merge the two datasets into a clean gamedetail object
            GameDetail result = new();
            if (gameOwnedGames != null && gameOwnedGames.response != null && gameOwnedGames.response.games != null)
            {
                foreach (Message item in gameOwnedGames.response.games)
                {
                    if (item.appid == appID)
                    {
                        result.AppID = appID;
                        result.GameName = item.name;
                        if (string.IsNullOrEmpty(item.img_icon_url))
                        {
                            result.IconURL = null;
                        }
                        else
                        {
                            result.IconURL = item.img_icon_url;
                        }
                        result.LogoURL = item.img_logo_url;
                        break;
                    }
                }
            }

            //Get the achievements from another dataset
            if (gameDetail != null)
            {
                //Get the achievements
                Tuple<List<Achievement>, string> tempResults = await GetAchievementDataAsync(steamID, appID, gameDetail, achievementToSearch);
                result.Achievements = tempResults.Item1;
                result.ErrorMessage = tempResults.Item2;

                //Calculate the total achieved items
                int totalAchieved = 0;
                foreach (Achievement item in result.Achievements)
                {
                    if (item.Achieved == true)
                    {
                        totalAchieved++;
                    }
                }
                result.TotalAchieved = totalAchieved;
                if (result.Achievements.Count > 0)
                {
                    result.PercentAchieved = (decimal)totalAchieved / (decimal)result.Achievements.Count;
                }

                //Get the achievement Stats
                //Build Stats
                if (getStats == true)
                {
                    List<string> blackList = new(){ "achievement", "the", "with", "one", "and", "game", "your", "all", "every", "have", "for", "than", "any", "you" };
                    TextStats ts = new(blackList, true);
                    foreach (Achievement item in result.Achievements)
                    {
                        ts.AddItem(item.ApiName, '_');
                        ts.AddItem(item.Description, ' ');
                        ts.AddItem(item.Name, ' ');
                    }
                    //sort the final list
                    List<KeyValuePair<string, int>> sortedList = ts.SortList(false);

                    //Filter the list to remove more than 10 search items
                    int counter = 0;
                    int currentItem = 0;
                    foreach (KeyValuePair<string, int> item in sortedList)
                    {
                        counter++;
                        if (counter >= 15)
                        {
                            currentItem = item.Value;
                            break;
                        }
                    }
                    List<KeyValuePair<string, int>> finalSortedList = new();
                    foreach (KeyValuePair<string, int> item in sortedList)
                    {
                        if (item.Value >= currentItem)
                        {
                            finalSortedList.Add(item);
                        }
                        else if (item.Value < currentItem)
                        {
                            break;
                        }
                    }
                    result.AchievementsStats = finalSortedList;
                }
                else
                {
                    result.AchievementsStats = new();
                }
            }
            else
            {
                result.Achievements = new();
                result.AchievementsStats = new();
            }

            return result;
        }

        //public async Task<GameDetail> GetDataWithFriendAsync(string steamID, string appID, string friendSteamId, bool getStats = true, string achievementToSearch = null)
        //{
        //    GameDetail details = await GetDataAsync(redisService, steamID, appID, getStats, achievementToSearch);
        //    GameDetail friendDetails = await GetDataAsync(redisService, friendSteamId, appID, getStats, achievementToSearch);

        //    //if the friend details return correctly, (this can fail sometimes if the friend has a private project), then continue:
        //    if (friendDetails == null || friendDetails.Achievements == null || friendDetails.Achievements.Count == 0)
        //    {
        //        details = null;
        //    }
        //    else
        //    {
        //        details.FriendPercentAchieved = friendDetails.PercentAchieved;
        //        details.FriendTotalAchieved = friendDetails.TotalAchieved;
        //        foreach (Achievement item in details.Achievements)
        //        {
        //            //Get the friends achievements for this game
        //            Achievement friendItem = FindFriendItem(item.ApiName, friendDetails.Achievements);
        //            if (friendItem != null)
        //            {
        //                item.FriendAchieved = friendItem.Achieved;
        //            }
        //        }
        //    }

        //    return details;
        //}

        //private Achievement FindFriendItem(string apiName, List<Achievement> friendAchievements)
        //{
        //    Achievement result = null;
        //    foreach (Achievement item in friendAchievements)
        //    {
        //        if (item.ApiName == apiName)
        //        {
        //            result = item;
        //            break;
        //        }
        //    }
        //    return result;
        //}

        public async Task<Tuple<List<Achievement>, string>> GetAchievementDataAsync(string steamID, string appID, SteamGameDetail steamGameDetails, string achievementToSearch)
        {
            //Get the achievements for the app
            SteamPlayerAchievementsForAppDA da = new();
            Tuple<SteamPlayerAchievementsForApp, SteamPlayerAchievementsForAppError> playerData = await da.GetDataAsync(steamID, appID);

            List<Achievement> results = new();
            if (playerData != null && playerData.Item1 != null)
            {
                //Get the global achievement stats for the app
                SteamGlobalAchievementPercentagesForAppDA da2 = new();
                SteamGlobalAchievementsForApp globalData = await da2.GetDataAsync(appID);

                if (playerData.Item1.playerstats.achievements != null)
                {
                    foreach (SteamPlayerAchievement item in playerData.Item1.playerstats.achievements)
                    {
                        //Merge the play achievements with the global and friend achivements to one clean achievement object
                        //if (item.apiname== "DLC4_ACHIEVEMENT_01")
                        //{
                        //    Console.WriteLine("here");
                        //}
                        Achievement newItem = new();
                        newItem.ApiName = item.apiname;
                        newItem.Name = item.name;
                        newItem.Description = item.description;
                        newItem.IsVisible = true;
                        if (item.achieved == 1)
                        {
                            newItem.Achieved = true;
                        }
                        else
                        {
                            newItem.Achieved = false;
                        }
                        newItem.GlobalPercent = GetGlobalAchievement(item.apiname, globalData.achievementpercentages.achievements);
                        if (steamGameDetails != null && steamGameDetails.game != null && steamGameDetails.game.availableGameStats != null && steamGameDetails.game.availableGameStats.achievements != null)
                        {
                            newItem.IconURL = GetIconURL(newItem.ApiName, steamGameDetails.game.availableGameStats.achievements);
                            newItem.IconGrayURL = GetIconGrayURL(newItem.ApiName, steamGameDetails.game.availableGameStats.achievements);
                            newItem.IsVisible = GetIsVisible(newItem.ApiName, steamGameDetails.game.availableGameStats.achievements);
                        }
                        if (newItem.IsVisible == false && newItem.Achieved == true)
                        {
                            newItem.IsVisible = true;
                        }
                        if (newItem.IsVisible == false)
                        {
                            newItem.Description = "(Note: this is a hidden achievement that has not been released to the public yet)";
                        }
                        newItem.FriendAchieved = false;

                        if (achievementToSearch != null)
                        {
                            string[] splitApiNames = newItem.ApiName.Split('_');
                            if (Array.Exists(splitApiNames, element => element == achievementToSearch.ToUpper()) == true)
                            {
                                results.Add(newItem);
                            }
                        }
                        else
                        {
                            results.Add(newItem);
                        }

                    }
                }

                //Sort the results by global achievement %
                results.Sort(delegate (Achievement p1, Achievement p2)
                {
                    return p2.GlobalPercent.CompareTo(p1.GlobalPercent);
                });
            }

            //Handle any errors that may have been returned from the Steam API
            string error = null;
            if (playerData != null && playerData.Item2 != null && playerData.Item2.playerstats != null && playerData.Item2.playerstats.success == false)
            {
                error = playerData.Item2.playerstats.error;
            }

            return new Tuple<List<Achievement>, string>(results, error);
        }

        private static string GetIconURL(string apiName, List<GameAchievement> steamGameDetails)
        {
            string result = null;
            foreach (GameAchievement item in steamGameDetails)
            {
                if (item.name == apiName)
                {
                    result = item.icon;
                    break;
                }
            }
            return result;
        }

        private static string GetIconGrayURL(string apiName, List<GameAchievement> steamGameDetails)
        {
            string result = null;
            foreach (GameAchievement item in steamGameDetails)
            {
                if (item.name == apiName)
                {
                    result = item.icongray;
                    break;
                }
            }
            return result;
        }

        private static bool GetIsVisible(string apiName, List<GameAchievement> steamGameDetails)
        {
            bool result = false;
            foreach (GameAchievement item in steamGameDetails)
            {
                if (item.name == apiName)
                {
                    result = (item.hidden != 1);
                    break;
                }
            }
            return result;
        }

        private static decimal GetGlobalAchievement(string apiName, List<SteamGlobalAchievement> globalItems)
        {
            decimal result = 0m;
            foreach (SteamGlobalAchievement item in globalItems)
            {
                if (item.name == apiName)
                {
                    result = item.percent;
                    break;
                }
            }
            return result;
        }
    }
}
