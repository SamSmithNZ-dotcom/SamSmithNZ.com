﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.Models.Steam
{
    //<achievementpercentages> 
    //  <achievements> 
    //      <achievement> 
    //          <name>ACHIEVEMENT_28</name> 
    //          <percent>80.6781005859375</percent> 
    //      </achievement>
    //  <achievements>
    //<achievementpercentages>
    public class SteamGlobalAchievementsForApp
    {
        public SteamGlobalAchievements achievementpercentages { get; set; }

    }

    public class SteamGlobalAchievements
    {
        public List<SteamGlobalAchievement> achievements { get; set; }
    }

    public class SteamGlobalAchievement
    {
        public string name { get; set; }
        public decimal percent { get; set; }
    }
}
