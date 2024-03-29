﻿using Microsoft.AspNetCore.Mvc;
using SamSmithNZ.Service.DataAccess.WorldCup.Interfaces;
using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.Controllers.WorldCup
{
    [Route("api/WorldCup/[controller]")]
    [ApiController]
    public class StatsAverageTournamentGoalsController : ControllerBase
    {
        private readonly IStatsAverageTournamentGoalsDataAccess _repo;

        public StatsAverageTournamentGoalsController(IStatsAverageTournamentGoalsDataAccess repo)
        {
            _repo = repo;
        }

        [HttpGet("GetStatsAverageTournamentGoalsList")]
        public async Task<List<StatsAverageTournamentGoals>> GetStatsAverageTournamentGoalsList(int? competitionCode = null)
        {
            return await _repo.GetList(competitionCode);
        }

        [HttpGet("GetStatsAverageTournamentGoals")]
        public async Task<StatsAverageTournamentGoals> GetStatsAverageTournamentGoals(int tournamentCode)
        {
            return await _repo.GetItem(tournamentCode);
        }

    }
}
