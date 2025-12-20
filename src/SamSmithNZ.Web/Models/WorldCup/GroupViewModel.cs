using Microsoft.AspNetCore.Mvc.Rendering;
using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;

namespace SamSmithNZ.Web.Models.WorldCup
{
    public class GroupViewModel
    {
        public GroupViewModel(List<GroupCode> groupCodes)
        {
            GroupCodes = new List<SelectListItem>();
            foreach (GroupCode item in groupCodes)
            {
                GroupCodes.Add(new SelectListItem(item.RoundCode, item.RoundCode));
            }
            if (groupCodes.Count > 0)
            {
                IsLastRound = groupCodes[0].IsLastRound;
            }
        }

        public int TournamentCode { get; set; }
        public int RoundNumber { get; set; }
        public string RoundCode { get; set; }
        public List<SelectListItem> GroupCodes { get; set; }
        public List<Group> Groups { get; set; }
        public bool IsLastRound { get; set; }
        public bool TeamWithdrew { get; set; }
        public List<Game> Games { get; set; }

        public string GetGroupRowStyle(bool hasQualifiedForNextRound, int groupRanking, bool teamWithdrew, bool isLastRound)
        {
            string trStyle = "white";
            if (isLastRound == true)
            {
                switch (groupRanking)
                {
                    case 1:
                        trStyle = "gold";
                        break;
                    case 2:
                        trStyle = "silver";
                        break;
                    case 3:
                        trStyle = "#A67D3D"; //Bronze
                        break;
                }
            }
            else
            {
                if (hasQualifiedForNextRound == true)
                {
                    trStyle = "#CCFF99"; // (light green) = Qualified/Advanced
                }
                else if (teamWithdrew == true)
                {
                    trStyle = "#ffcccc"; // (light red/pink) for teams that withdrew - indicating a negative status
                }
            }
            return trStyle;
        }

        public string GetGameRowStyle(int gameCode)
        {
            string trStyle = "white";
            if ((gameCode % 2) == 1)
            {
                trStyle = "#f9f9f9"; //very light gray
            }
            return trStyle;
        }

    }
}
