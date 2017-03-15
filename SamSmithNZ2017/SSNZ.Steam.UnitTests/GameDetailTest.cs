﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSNZ.Steam.Data;
using SSNZ.Steam.Models;

namespace SSNZ.Steam.UnitTests
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class GameDetailTest
    {

        [TestMethod]
        public void SamSmithXCOMTest()
        {
            //Arrange
            GameDetailsDA da = new GameDetailsDA();
            string steamId = "76561197971691578";
            string appId = "200510"; //Xcom

            //Act
            GameDetail result = da.GetData(steamId, appId);

            //Assert
            Assert.IsTrue(result != null);
            Assert.IsTrue(result.AppID == "200510");
            Assert.IsTrue(result.GameName == "XCOM: Enemy Unknown");
            Assert.IsTrue(result.IconURL == "48be2fee1d0d511b5c7313e1359beafd36ea92ed");
            Assert.IsTrue(result.LogoURL == "eaa298d2b0d908b2c4f5370d2c8c59a8eff887c6");
            Assert.IsTrue(result.PercentAchieved == 1m);
            Assert.IsTrue(result.TotalAchieved == 85m);
            Assert.IsTrue(result.Achievements.Count == 85);
            Assert.IsTrue(result.Achievements[0].ApiName == "ACHIEVEMENT_28");
            Assert.IsTrue(result.Achievements[0].Name == "We Happy Few ");
            Assert.IsTrue(result.Achievements[0].Description == "Complete a mission without losing a soldier.");
            Assert.IsTrue(result.Achievements[0].Achieved == true);
            Assert.IsTrue(result.Achievements[0].IconURL == "http://cdn.akamai.steamstatic.com/steamcommunity/public/images/apps/200510/9ef3538334062eceed71992328e6b1a6b577b5d7.jpg");
            Assert.IsTrue(result.Achievements[0].IconGrayURL == "http://cdn.akamai.steamstatic.com/steamcommunity/public/images/apps/200510/8cb928ad8be98984f1c739fa6f9b4f34ae0ea17e.jpg");
            Assert.IsTrue(result.Achievements[0].GlobalPercent >= 0);
            Assert.IsTrue(result.Achievements[0].FriendAchieved == false);
        }


        [TestMethod]
        public void SamSmithXCOMFriendWithStewTest()
        {
            //Arrange
            GameDetailsDA da = new GameDetailsDA();
            string steamId = "76561197971691578"; //Sam
            string friendSteamId = "76561197990013217"; //Stew
            string appId = "200510"; //Xcom

            //Act
            GameDetail result = da.GetDataWithFriend(steamId, appId, friendSteamId);

            //Assert
            Assert.IsTrue(result != null);
            Assert.IsTrue(result.AppID == "200510");
            Assert.IsTrue(result.GameName == "XCOM: Enemy Unknown");
            Assert.IsTrue(result.IconURL == "48be2fee1d0d511b5c7313e1359beafd36ea92ed");
            Assert.IsTrue(result.LogoURL == "eaa298d2b0d908b2c4f5370d2c8c59a8eff887c6");
            Assert.IsTrue(result.PercentAchieved == 1m);
            Assert.IsTrue(result.TotalAchieved == 85m);
            Assert.IsTrue(result.Achievements.Count == 85);
            bool foundAPI1 = false;
            bool foundAPI2 = false;
            foreach (Achievement item in result.Achievements)
            {
                if (item.ApiName == "ACHIEVEMENT_28")
                {
                    foundAPI1 = true;
                    Assert.IsTrue(item.ApiName == "ACHIEVEMENT_28");
                    Assert.IsTrue(item.Name == "We Happy Few ");
                    Assert.IsTrue(item.Description == "Complete a mission without losing a soldier.");
                    Assert.IsTrue(item.Achieved == true);
                    Assert.IsTrue(item.IconURL == "http://cdn.akamai.steamstatic.com/steamcommunity/public/images/apps/200510/9ef3538334062eceed71992328e6b1a6b577b5d7.jpg");
                    Assert.IsTrue(item.IconGrayURL == "http://cdn.akamai.steamstatic.com/steamcommunity/public/images/apps/200510/8cb928ad8be98984f1c739fa6f9b4f34ae0ea17e.jpg");
                    Assert.IsTrue(item.GlobalPercent >= 0);
                    Assert.IsTrue(item.FriendAchieved == true);
                }
                else if (item.ApiName == "ACHIEVEMENT_38")
                {
                    foundAPI2 = true;
                    Assert.IsTrue(item.ApiName == "ACHIEVEMENT_38");
                    Assert.IsTrue(item.Name == "A Continental Fellow");
                    Assert.IsTrue(item.Description == "Win the game from each of the 5 starting locations.");
                    Assert.IsTrue(item.Achieved == true);
                    Assert.IsTrue(item.IconURL == "http://cdn.akamai.steamstatic.com/steamcommunity/public/images/apps/200510/1efdb8b427c628de17a49a55ad5afb495dd35cf0.jpg");
                    Assert.IsTrue(item.IconGrayURL == "http://cdn.akamai.steamstatic.com/steamcommunity/public/images/apps/200510/3d74016b994db08005c2aafe05db4740f22f5876.jpg");
                    Assert.IsTrue(item.GlobalPercent >= 0);
                    Assert.IsTrue(item.FriendAchieved == false);
                }
                if (foundAPI2 == true && foundAPI1 == true)
                {
                    break;
                }
            }
            Assert.IsTrue(foundAPI1 == true);
            Assert.IsTrue(foundAPI2 == true);
        }

        [TestMethod]
        public void SamSmithCiv6FriendWithStewTest()
        {
            //Arrange
            GameDetailsDA da = new GameDetailsDA();
            string steamId = "76561197971691578"; //Sam
            string friendSteamId = "76561197990013217"; //Stew
            string appId = "289070"; //Civ 6

            //Act
            GameDetail result = da.GetDataWithFriend(steamId, appId, friendSteamId);

            //Assert
            Assert.IsTrue(result == null);
           
        }
            

        [TestMethod]
        public void SamSmithCOH2Test()
        {
            //Arrange
            GameDetailsDA da = new GameDetailsDA();
            string steamId = "76561197971691578";
            string appId = "231430"; //Company of Heroes 2

            //Act
            GameDetail result = da.GetData(steamId, appId);

            //Assert
            Assert.IsTrue(result != null);
            Assert.IsTrue(result.AppID == "231430");
            Assert.IsTrue(result.GameName == "Company of Heroes 2");
            Assert.IsTrue(result.IconURL == "5e7e1866fb4f33a17393b0441cd1c750dbd91c5f");
            Assert.IsTrue(result.LogoURL == "1be017e709b9b0638e82b399cc97de4667f44a1e");
            Assert.IsTrue(result.PercentAchieved >= 0m);
            Assert.IsTrue(result.TotalAchieved >= 12);
            Assert.IsTrue(result.Achievements.Count == 452);
            bool foundAPI1 = false;
            bool foundAPI2 = false;
            foreach (Achievement item in result.Achievements)
            {
                if (item.ApiName == "count_camp_1")
                {
                    foundAPI1 = true;
                    Assert.IsTrue(item.ApiName == "count_camp_1");
                    Assert.IsTrue(item.Name == "Campaign Conscript");
                    Assert.IsTrue(item.Description == "Play 1 Campaign mission");
                    Assert.IsTrue(item.Achieved == true);
                    Assert.IsTrue(item.IconURL == "http://cdn.akamai.steamstatic.com/steamcommunity/public/images/apps/231430/d4764605a93d376ee1f7ab92d74fc020a613390a.jpg");
                    Assert.IsTrue(item.IconGrayURL == "http://cdn.akamai.steamstatic.com/steamcommunity/public/images/apps/231430/a17ecc024d331e61dbf70a32cf3cbd9e6c38e007.jpg");
                    Assert.IsTrue(item.GlobalPercent >= 0);
                    Assert.IsTrue(item.FriendAchieved == false);
                }
                else if (item.ApiName == "count_stomp_1")
                {
                    foundAPI2 = true;
                    Assert.IsTrue(item.ApiName == "count_stomp_1");
                    Assert.IsTrue(item.Name == "Comp-Stomp Conscript");
                    Assert.IsTrue(item.Description == "Play 1 match versus AI");
                    Assert.IsTrue(item.Achieved == false);
                    Assert.IsTrue(item.IconURL == "http://cdn.akamai.steamstatic.com/steamcommunity/public/images/apps/231430/60c55f152741c29391c8bb0bf7b78c1fb62c7c60.jpg");
                    Assert.IsTrue(item.IconGrayURL == "http://cdn.akamai.steamstatic.com/steamcommunity/public/images/apps/231430/a17ecc024d331e61dbf70a32cf3cbd9e6c38e007.jpg");
                    Assert.IsTrue(item.GlobalPercent >= 0);
                    Assert.IsTrue(item.FriendAchieved == false);
                }
                if (foundAPI2 == true && foundAPI1 == true)
                {
                    break;
                }
            }
            Assert.IsTrue(foundAPI1 == true);
            Assert.IsTrue(foundAPI2 == true);
        }

        [TestMethod]
        public void SamSmithCastleStoryTest()
        {
            //Arrange
            GameDetailsDA da = new GameDetailsDA();
            string steamId = "76561197971691578";
            string appId = "227860"; //castle Story

            //Act
            GameDetail result = da.GetData(steamId, appId);

            //Assert
            Assert.IsTrue(result != null);
            Assert.IsTrue(result.AppID == "227860");
            Assert.IsTrue(result.GameName == "Castle Story");
            Assert.IsTrue(result.IconURL == "5ba78b0a0b8197fcbef037b3ad0cc526fb5da4a1");
            Assert.IsTrue(result.LogoURL == "8456e045dd5f0311b71246c0c80b21b9b58c968e");
            Assert.IsTrue(result.PercentAchieved == 0m);
            Assert.IsTrue(result.TotalAchieved == 0m);
            Assert.IsTrue(result.Achievements.Count == 0);
        }

        [TestMethod]
        public void SamSmithGodusTest()
        {
            //Arrange
            GameDetailsDA da = new GameDetailsDA();
            string steamId = "76561197971691578";
            string appId = "232810"; //Godus

            //Act
            GameDetail result = da.GetData(steamId, appId);

            //Assert
            Assert.IsTrue(result != null);
            Assert.IsTrue(result.AppID == "232810");
            Assert.IsTrue(result.GameName == "Godus");
            Assert.IsTrue(result.IconURL == "4ee4e78811f8600fa39bc4377129b124b63e42a1");
            Assert.IsTrue(result.LogoURL == "e2a7637399293a7d2406157e6e4b833d519526ec");
            Assert.IsTrue(result.PercentAchieved == 0m);
            Assert.IsTrue(result.TotalAchieved == 0m);
            Assert.IsTrue(result.Achievements.Count == 0);
        }

    }
}
