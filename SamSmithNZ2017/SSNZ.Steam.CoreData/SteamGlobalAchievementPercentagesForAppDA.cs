﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SSNZ.Steam.CoreModels;
using System.Net;
using System.Net.Http;

namespace SSNZ.Steam.CoreData
{
    public class SteamGlobalAchievementPercentagesForAppDA
    {

        //http://api.steampowered.com/ISteamUserStats/GetGlobalAchievementPercentagesForApp/v0002/?gameid=200510
        public async Task<SteamGlobalAchievementsForApp> GetDataAsync(string appID)
        {
            string jsonRequestString = "http://api.steampowered.com/ISteamUserStats/GetGlobalAchievementPercentagesForApp/v0002/?gameid=" + appID.ToString();
            //WebClient newClient = new WebClient();
            //newClient.Encoding = UTF8Encoding.UTF8;
            //string jsonData = await newClient.DownloadStringTaskAsync(jsonRequestString);
            string jsonData = await Utility.GetPageAsStringAsync(new Uri(jsonRequestString));

            SteamGlobalAchievementsForApp result = JsonConvert.DeserializeObject<SteamGlobalAchievementsForApp>(jsonData);
            return result;
        }

      
    }
}
