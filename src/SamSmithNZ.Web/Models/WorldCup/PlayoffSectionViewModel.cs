using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;

namespace SamSmithNZ.Web.Models.WorldCup
{
    public class PlayoffSectionViewModel
    {
        public PlayoffSectionViewModel(List<Game> games, List<Playoff> playoffs, List<string> rounds, 
            bool showDebugElements, bool top32, bool top16, bool quarterFinals, bool semiFinals, bool finals, bool thirdPlace)
        {
            List<Game> filteredGames = new();
            foreach (Game game in games)
            {
                if (rounds.Contains(game.RoundCode) == true && game.RowType == 1)
                {
                    filteredGames.Add(game);
                }
            }

            List<Playoff> filteredPlayoffs = new();
            foreach (Playoff playoff in playoffs)
            {
                if (rounds.Contains(playoff.RoundCode) == true)
                {
                    filteredPlayoffs.Add(playoff);
                }
            }

            Games = filteredGames;
            Playoffs = filteredPlayoffs;
            this.ShowDebugElements = showDebugElements;
            this.Top32 = top32;
            this.Top16 = top16;
            this.QuarterFinals = quarterFinals;
            this.SemiFinals = semiFinals;
            this.Finals = finals;
            this.ThirdPlace = thirdPlace;
        }

        public bool ShowDebugElements { get; private set; }
        public bool Top32 { get; private set; }
        public bool Top16 { get; private set; }
        public bool QuarterFinals { get; private set; }
        public bool SemiFinals { get; private set; }
        public bool Finals { get; private set; }
        public bool ThirdPlace { get; private set; }

        public List<Game> Games { get; set; }
        public List<Playoff> Playoffs { get; set; }

    }
}
