namespace SamSmithNZ.Service.Models.WorldCup
{
    /// <summary>
    /// Represents a single group in a third place combination
    /// </summary>
    public class ThirdPlaceMatch
    {
        public int TournamentCode { get; set; }
        public int CombinationCode { get; set; }
        public string WinnerMatchCode { get; set; }
        public string ThirdPlaceGroup { get; set; }
    }
}
