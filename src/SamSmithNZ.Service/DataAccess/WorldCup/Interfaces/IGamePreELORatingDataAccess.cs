﻿using SamSmithNZ.Service.Models.WorldCup;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.WorldCup.Interfaces
{
    public interface IGamePreELORatingDataAccess
    {
        Task<GamePreEloRating> GetGamePreELORatings(int tournamentCode, int gameCode);
    }
}