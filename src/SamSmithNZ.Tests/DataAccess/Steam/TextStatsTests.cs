using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithNZ.Service.DataAccess.Steam;
using System.Collections.Generic;
using System.Linq;

namespace SamSmithNZ.Tests.DataAccess.Steam
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class TextStatsTests
    {
        [TestMethod]
        public void TextStats_Constructor_InitializesEmptyDictionary()
        {
            // Arrange
            List<string> blackList = new List<string>();
            bool filterNumbers = true;

            // Act
            TextStats stats = new TextStats(blackList, filterNumbers);

            // Assert
            Assert.IsNotNull(stats);
            Assert.IsNotNull(stats.TextStatistics);
            Assert.AreEqual(0, stats.TextStatistics.Count);
        }

        [TestMethod]
        public void AddItem_SimpleText_AddsWordsToDictionary()
        {
            // Arrange
            TextStats stats = new TextStats(new List<string>(), false);
            string text = "hello world test";
            char splitChar = ' ';

            // Act
            bool result = stats.AddItem(text, splitChar);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(3, stats.TextStatistics.Count);
            Assert.IsTrue(stats.TextStatistics.ContainsKey("hello"));
            Assert.IsTrue(stats.TextStatistics.ContainsKey("world"));
            Assert.IsTrue(stats.TextStatistics.ContainsKey("test"));
        }

        [TestMethod]
        public void AddItem_DuplicateWords_IncrementsCount()
        {
            // Arrange
            TextStats stats = new TextStats(new List<string>(), false);
            string text = "test test test";
            char splitChar = ' ';

            // Act
            stats.AddItem(text, splitChar);

            // Assert
            Assert.AreEqual(1, stats.TextStatistics.Count);
            Assert.AreEqual(3, stats.TextStatistics["test"]);
        }

        [TestMethod]
        public void AddItem_TextWithPunctuation_RemovesPunctuation()
        {
            // Arrange
            TextStats stats = new TextStats(new List<string>(), false);
            string text = "hello, world. test";
            char splitChar = ' ';

            // Act
            stats.AddItem(text, splitChar);

            // Assert
            Assert.IsTrue(stats.TextStatistics.ContainsKey("hello"));
            Assert.IsTrue(stats.TextStatistics.ContainsKey("world"));
            Assert.IsTrue(stats.TextStatistics.ContainsKey("test"));
        }

        [TestMethod]
        public void AddItem_MixedCase_ConvertsToLowerCase()
        {
            // Arrange
            TextStats stats = new TextStats(new List<string>(), false);
            string text = "Hello WORLD Test";
            char splitChar = ' ';

            // Act
            stats.AddItem(text, splitChar);

            // Assert
            Assert.IsTrue(stats.TextStatistics.ContainsKey("hello"));
            Assert.IsTrue(stats.TextStatistics.ContainsKey("world"));
            Assert.IsTrue(stats.TextStatistics.ContainsKey("test"));
            Assert.IsFalse(stats.TextStatistics.ContainsKey("Hello"));
            Assert.IsFalse(stats.TextStatistics.ContainsKey("WORLD"));
        }

        [TestMethod]
        public void AddItem_BlacklistedWords_FiltersOutBlacklistedWords()
        {
            // Arrange
            List<string> blackList = new List<string> { "achievement", "the" };
            TextStats stats = new TextStats(blackList, false);
            string text = "achievement the test";
            char splitChar = ' ';

            // Act
            stats.AddItem(text, splitChar);

            // Assert
            Assert.AreEqual(1, stats.TextStatistics.Count);
            Assert.IsTrue(stats.TextStatistics.ContainsKey("test"));
            Assert.IsFalse(stats.TextStatistics.ContainsKey("achievement"));
            Assert.IsFalse(stats.TextStatistics.ContainsKey("the"));
        }

        [TestMethod]
        public void AddItem_FilterNumbersTrue_FiltersOutNumbers()
        {
            // Arrange
            TextStats stats = new TextStats(new List<string>(), true);
            string text = "test 123 hello 456";
            char splitChar = ' ';

            // Act
            stats.AddItem(text, splitChar);

            // Assert
            Assert.AreEqual(2, stats.TextStatistics.Count);
            Assert.IsTrue(stats.TextStatistics.ContainsKey("test"));
            Assert.IsTrue(stats.TextStatistics.ContainsKey("hello"));
            Assert.IsFalse(stats.TextStatistics.ContainsKey("123"));
            Assert.IsFalse(stats.TextStatistics.ContainsKey("456"));
        }

        [TestMethod]
        public void AddItem_FilterNumbersFalse_IncludesNumbers()
        {
            // Arrange
            TextStats stats = new TextStats(new List<string>(), false);
            string text = "test 123 hello";
            char splitChar = ' ';

            // Act
            stats.AddItem(text, splitChar);

            // Assert
            Assert.AreEqual(3, stats.TextStatistics.Count);
            Assert.IsTrue(stats.TextStatistics.ContainsKey("test"));
            Assert.IsTrue(stats.TextStatistics.ContainsKey("hello"));
            Assert.IsTrue(stats.TextStatistics.ContainsKey("123"));
        }

        [TestMethod]
        public void AddItem_WordsLessThanThreeCharacters_FiltersOut()
        {
            // Arrange
            TextStats stats = new TextStats(new List<string>(), false);
            string text = "a to be test hello";
            char splitChar = ' ';

            // Act
            stats.AddItem(text, splitChar);

            // Assert
            Assert.AreEqual(2, stats.TextStatistics.Count);
            Assert.IsTrue(stats.TextStatistics.ContainsKey("test"));
            Assert.IsTrue(stats.TextStatistics.ContainsKey("hello"));
            Assert.IsFalse(stats.TextStatistics.ContainsKey("a"));
            Assert.IsFalse(stats.TextStatistics.ContainsKey("to"));
            Assert.IsFalse(stats.TextStatistics.ContainsKey("be"));
        }

        [TestMethod]
        public void AddItem_EmptyString_ReturnsTrue()
        {
            // Arrange
            TextStats stats = new TextStats(new List<string>(), false);
            string text = "";
            char splitChar = ' ';

            // Act
            bool result = stats.AddItem(text, splitChar);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(0, stats.TextStatistics.Count);
        }

        [TestMethod]
        public void AddItem_DifferentSplitChar_SplitsCorrectly()
        {
            // Arrange
            TextStats stats = new TextStats(new List<string>(), false);
            string text = "hello;world;test";
            char splitChar = ';';

            // Act
            stats.AddItem(text, splitChar);

            // Assert
            Assert.AreEqual(3, stats.TextStatistics.Count);
            Assert.IsTrue(stats.TextStatistics.ContainsKey("hello"));
            Assert.IsTrue(stats.TextStatistics.ContainsKey("world"));
            Assert.IsTrue(stats.TextStatistics.ContainsKey("test"));
        }

        [TestMethod]
        public void AddItem_MultipleCallsSameWord_AccumulatesCount()
        {
            // Arrange
            TextStats stats = new TextStats(new List<string>(), false);

            // Act
            stats.AddItem("test hello", ' ');
            stats.AddItem("test world", ' ');
            stats.AddItem("test again", ' ');

            // Assert
            Assert.AreEqual(4, stats.TextStatistics.Count);
            Assert.AreEqual(3, stats.TextStatistics["test"]);
            Assert.AreEqual(1, stats.TextStatistics["hello"]);
            Assert.AreEqual(1, stats.TextStatistics["world"]);
            Assert.AreEqual(1, stats.TextStatistics["again"]);
        }

        [TestMethod]
        public void SortList_DescendingOrder_ReturnsSortedList()
        {
            // Arrange
            TextStats stats = new TextStats(new List<string>(), false);
            stats.AddItem("apple apple apple banana banana cherry", ' ');

            // Act
            List<KeyValuePair<string, int>> sortedList = stats.SortList(false);

            // Assert
            Assert.AreEqual(3, sortedList.Count);
            Assert.AreEqual("apple", sortedList[0].Key);
            Assert.AreEqual(3, sortedList[0].Value);
            Assert.AreEqual("banana", sortedList[1].Key);
            Assert.AreEqual(2, sortedList[1].Value);
            Assert.AreEqual("cherry", sortedList[2].Key);
            Assert.AreEqual(1, sortedList[2].Value);
        }

        [TestMethod]
        public void SortList_EmptyDictionary_ReturnsEmptyList()
        {
            // Arrange
            TextStats stats = new TextStats(new List<string>(), false);

            // Act
            List<KeyValuePair<string, int>> sortedList = stats.SortList(false);

            // Assert
            Assert.AreEqual(0, sortedList.Count);
        }

        [TestMethod]
        public void SortList_SingleItem_ReturnsSingleItemList()
        {
            // Arrange
            TextStats stats = new TextStats(new List<string>(), false);
            stats.AddItem("test test test", ' ');

            // Act
            List<KeyValuePair<string, int>> sortedList = stats.SortList(false);

            // Assert
            Assert.AreEqual(1, sortedList.Count);
            Assert.AreEqual("test", sortedList[0].Key);
            Assert.AreEqual(3, sortedList[0].Value);
        }

        [TestMethod]
        public void SortList_ComplexScenario_SortsCorrectly()
        {
            // Arrange
            TextStats stats = new TextStats(new List<string>(), false);
            stats.AddItem("achievement unlock complete victory master supreme elite legendary heroic", ' ');
            stats.AddItem("unlock complete victory", ' ');
            stats.AddItem("complete victory", ' ');
            stats.AddItem("victory", ' ');

            // Act
            List<KeyValuePair<string, int>> sortedList = stats.SortList(false);

            // Assert
            Assert.AreEqual(9, sortedList.Count);
            Assert.AreEqual("victory", sortedList[0].Key);
            Assert.AreEqual(4, sortedList[0].Value);
            Assert.AreEqual("complete", sortedList[1].Key);
            Assert.AreEqual(3, sortedList[1].Value);
            Assert.AreEqual("unlock", sortedList[2].Key);
            Assert.AreEqual(2, sortedList[2].Value);
        }

        [TestMethod]
        public void IntegrationTest_SteamAchievementScenario()
        {
            // Arrange
            List<string> blackList = new List<string> { "achievement", "the", "and", "for" };
            TextStats stats = new TextStats(blackList, true);

            // Act - Simulating Steam achievement descriptions
            stats.AddItem("Complete the tutorial", ' ');
            stats.AddItem("Complete 10 levels", ' ');
            stats.AddItem("Complete all challenges", ' ');
            stats.AddItem("Master difficulty complete", ' ');
            stats.AddItem("Win 100 matches", ' ');

            // Assert
            stats.SortList(false);
            int completeCount;
            Assert.IsTrue(stats.TextStatistics.TryGetValue("complete", out completeCount));
            Assert.AreEqual(4, completeCount);
            Assert.IsFalse(stats.TextStatistics.ContainsKey("the"));
            Assert.IsFalse(stats.TextStatistics.ContainsKey("10"));
            Assert.IsFalse(stats.TextStatistics.ContainsKey("100"));
        }
    }
}
