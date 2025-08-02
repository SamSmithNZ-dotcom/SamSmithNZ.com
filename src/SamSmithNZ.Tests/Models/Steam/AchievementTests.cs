using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithNZ.Service.Models.Steam;

namespace SamSmithNZ.Tests.Models.Steam
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class AchievementTests
    {
        [TestMethod]
        public void Achievement_DefaultConstructor_CreatesInstance()
        {
            // Act
            var achievement = new Achievement();

            // Assert
            Assert.IsNotNull(achievement);
            Assert.IsNull(achievement.ApiName);
            Assert.IsFalse(achievement.Achieved);
            Assert.IsNull(achievement.Name);
            Assert.IsNull(achievement.Description);
            Assert.AreEqual(0m, achievement.GlobalPercent);
            Assert.IsNull(achievement.IconURL);
            Assert.IsNull(achievement.IconGrayURL);
            Assert.IsFalse(achievement.FriendAchieved);
            Assert.IsFalse(achievement.IsVisible);
        }

        [TestMethod]
        public void Achievement_SetAllProperties_ReturnsAllValues()
        {
            // Arrange
            var achievement = new Achievement();
            var expectedApiName = "ACHIEVEMENT_1";
            var expectedAchieved = true;
            var expectedName = "First Blood";
            var expectedDescription = "Get your first kill";
            var expectedGlobalPercent = 85.6m;
            var expectedIconURL = "http://example.com/icon.jpg";
            var expectedIconGrayURL = "http://example.com/icon_gray.jpg";
            var expectedFriendAchieved = true;
            var expectedIsVisible = true;

            // Act
            achievement.ApiName = expectedApiName;
            achievement.Achieved = expectedAchieved;
            achievement.Name = expectedName;
            achievement.Description = expectedDescription;
            achievement.GlobalPercent = expectedGlobalPercent;
            achievement.IconURL = expectedIconURL;
            achievement.IconGrayURL = expectedIconGrayURL;
            achievement.FriendAchieved = expectedFriendAchieved;
            achievement.IsVisible = expectedIsVisible;

            // Assert
            Assert.AreEqual(expectedApiName, achievement.ApiName);
            Assert.AreEqual(expectedAchieved, achievement.Achieved);
            Assert.AreEqual(expectedName, achievement.Name);
            Assert.AreEqual(expectedDescription, achievement.Description);
            Assert.AreEqual(expectedGlobalPercent, achievement.GlobalPercent);
            Assert.AreEqual(expectedIconURL, achievement.IconURL);
            Assert.AreEqual(expectedIconGrayURL, achievement.IconGrayURL);
            Assert.AreEqual(expectedFriendAchieved, achievement.FriendAchieved);
            Assert.AreEqual(expectedIsVisible, achievement.IsVisible);
        }

        [TestMethod]
        public void Achievement_InitializeWithObjectInitializer_ReturnsAllValues()
        {
            // Arrange
            var expectedApiName = "ACHIEVEMENT_BOSS";
            var expectedAchieved = false;
            var expectedName = "Boss Slayer";
            var expectedDescription = "Defeat the final boss";
            var expectedGlobalPercent = 12.3m;
            var expectedIconURL = "http://example.com/boss_icon.jpg";
            var expectedIconGrayURL = "http://example.com/boss_icon_gray.jpg";
            var expectedFriendAchieved = false;
            var expectedIsVisible = true;

            // Act
            var achievement = new Achievement
            {
                ApiName = expectedApiName,
                Achieved = expectedAchieved,
                Name = expectedName,
                Description = expectedDescription,
                GlobalPercent = expectedGlobalPercent,
                IconURL = expectedIconURL,
                IconGrayURL = expectedIconGrayURL,
                FriendAchieved = expectedFriendAchieved,
                IsVisible = expectedIsVisible
            };

            // Assert
            Assert.AreEqual(expectedApiName, achievement.ApiName);
            Assert.AreEqual(expectedAchieved, achievement.Achieved);
            Assert.AreEqual(expectedName, achievement.Name);
            Assert.AreEqual(expectedDescription, achievement.Description);
            Assert.AreEqual(expectedGlobalPercent, achievement.GlobalPercent);
            Assert.AreEqual(expectedIconURL, achievement.IconURL);
            Assert.AreEqual(expectedIconGrayURL, achievement.IconGrayURL);
            Assert.AreEqual(expectedFriendAchieved, achievement.FriendAchieved);
            Assert.AreEqual(expectedIsVisible, achievement.IsVisible);
        }
    }
}