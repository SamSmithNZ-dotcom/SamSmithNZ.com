using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithNZ.Service.Models.WorldCup;

namespace SamSmithNZ.Tests.Models.WorldCup
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class ThirdPlaceMatchModelTests
    {
        [TestMethod]
        public void ThirdPlaceMatch_AllProperties_CanBeSetAndRead()
        {
            // Arrange & Act
            ThirdPlaceMatch match = new ThirdPlaceMatch
            {
                TournamentCode = 40,
                CombinationCode = 1,
                WinnerMatchCode = "W61",
                ThirdPlaceGroup = "A"
            };

            // Assert
            Assert.AreEqual(40, match.TournamentCode);
            Assert.AreEqual(1, match.CombinationCode);
            Assert.AreEqual("W61", match.WinnerMatchCode);
            Assert.AreEqual("A", match.ThirdPlaceGroup);
        }

        [TestMethod]
        public void ThirdPlaceMatch_DefaultConstructor_CreatesInstance()
        {
            // Arrange & Act
            ThirdPlaceMatch match = new ThirdPlaceMatch();

            // Assert
            Assert.IsNotNull(match);
            Assert.AreEqual(0, match.TournamentCode);
            Assert.AreEqual(0, match.CombinationCode);
            Assert.IsNull(match.WinnerMatchCode);
            Assert.IsNull(match.ThirdPlaceGroup);
        }

        [TestMethod]
        public void ThirdPlaceMatch_DifferentGroups_CanBeSet()
        {
            // Arrange & Act
            ThirdPlaceMatch matchA = new ThirdPlaceMatch { ThirdPlaceGroup = "A" };
            ThirdPlaceMatch matchB = new ThirdPlaceMatch { ThirdPlaceGroup = "B" };
            ThirdPlaceMatch matchC = new ThirdPlaceMatch { ThirdPlaceGroup = "C" };
            ThirdPlaceMatch matchD = new ThirdPlaceMatch { ThirdPlaceGroup = "D" };

            // Assert
            Assert.AreEqual("A", matchA.ThirdPlaceGroup);
            Assert.AreEqual("B", matchB.ThirdPlaceGroup);
            Assert.AreEqual("C", matchC.ThirdPlaceGroup);
            Assert.AreEqual("D", matchD.ThirdPlaceGroup);
        }
    }
}
