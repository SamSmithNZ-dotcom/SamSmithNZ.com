using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithNZ.Service.Models.Steam;

namespace SamSmithNZ.Tests.Models.Steam
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class FriendTests
    {
        [TestMethod]
        public void Friend_DefaultConstructor_CreatesInstance()
        {
            // Act
            Friend friend = new Friend();

            // Assert
            Assert.IsNotNull(friend);
            Assert.IsNull(friend.SteamId);
            Assert.IsNull(friend.Name);
            Assert.AreEqual(0L, friend.LastLogoff);
            Assert.IsNull(friend.ProfileURL);
            Assert.IsNull(friend.Avatar);
            Assert.IsNull(friend.AvatarMedium);
            Assert.IsNull(friend.AvatarFull);
            Assert.AreEqual(0L, friend.TimeCreated);
            Assert.AreEqual(0L, friend.FriendSince);
        }

        [TestMethod]
        public void Friend_SetAllProperties_ReturnsAllValues()
        {
            // Arrange
            Friend friend = new Friend();
            var expectedSteamId = "76561198059077520";
            var expectedName = "TestFriend";
            var expectedLastLogoff = 1234567890L;
            var expectedProfileURL = "https://steamcommunity.com/id/testfriend";
            var expectedAvatar = "avatar_hash.jpg";
            var expectedAvatarMedium = "avatar_medium_hash.jpg";
            var expectedAvatarFull = "avatar_full_hash.jpg";
            var expectedTimeCreated = 987654321L;
            var expectedFriendSince = 555666777L;

            // Act
            friend.SteamId = expectedSteamId;
            friend.Name = expectedName;
            friend.LastLogoff = expectedLastLogoff;
            friend.ProfileURL = expectedProfileURL;
            friend.Avatar = expectedAvatar;
            friend.AvatarMedium = expectedAvatarMedium;
            friend.AvatarFull = expectedAvatarFull;
            friend.TimeCreated = expectedTimeCreated;
            friend.FriendSince = expectedFriendSince;

            // Assert
            Assert.AreEqual(expectedSteamId, friend.SteamId);
            Assert.AreEqual(expectedName, friend.Name);
            Assert.AreEqual(expectedLastLogoff, friend.LastLogoff);
            Assert.AreEqual(expectedProfileURL, friend.ProfileURL);
            Assert.AreEqual(expectedAvatar, friend.Avatar);
            Assert.AreEqual(expectedAvatarMedium, friend.AvatarMedium);
            Assert.AreEqual(expectedAvatarFull, friend.AvatarFull);
            Assert.AreEqual(expectedTimeCreated, friend.TimeCreated);
            Assert.AreEqual(expectedFriendSince, friend.FriendSince);
        }

        [TestMethod]
        public void Friend_InitializeWithObjectInitializer_ReturnsAllValues()
        {
            // Arrange
            var expectedSteamId = "76561198121979762";
            var expectedName = "AnotherFriend";
            var expectedLastLogoff = 2222222222L;
            var expectedProfileURL = "https://steamcommunity.com/id/anotherfriend";
            var expectedAvatar = "another_avatar.jpg";
            var expectedAvatarMedium = "another_avatar_medium.jpg";
            var expectedAvatarFull = "another_avatar_full.jpg";
            var expectedTimeCreated = 1111111111L;
            var expectedFriendSince = 3333333333L;

            // Act
            Friend friend = new Friend
            {
                SteamId = expectedSteamId,
                Name = expectedName,
                LastLogoff = expectedLastLogoff,
                ProfileURL = expectedProfileURL,
                Avatar = expectedAvatar,
                AvatarMedium = expectedAvatarMedium,
                AvatarFull = expectedAvatarFull,
                TimeCreated = expectedTimeCreated,
                FriendSince = expectedFriendSince
            };

            // Assert
            Assert.AreEqual(expectedSteamId, friend.SteamId);
            Assert.AreEqual(expectedName, friend.Name);
            Assert.AreEqual(expectedLastLogoff, friend.LastLogoff);
            Assert.AreEqual(expectedProfileURL, friend.ProfileURL);
            Assert.AreEqual(expectedAvatar, friend.Avatar);
            Assert.AreEqual(expectedAvatarMedium, friend.AvatarMedium);
            Assert.AreEqual(expectedAvatarFull, friend.AvatarFull);
            Assert.AreEqual(expectedTimeCreated, friend.TimeCreated);
            Assert.AreEqual(expectedFriendSince, friend.FriendSince);
        }
    }
}