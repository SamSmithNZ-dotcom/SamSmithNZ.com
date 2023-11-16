﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using SamSmithNZ.Service.DataAccess.ITunes;
using SamSmithNZ.Service.DataAccess.ITunes.Interfaces;
using SamSmithNZ.Service.Models.ITunes;

namespace SamSmithNZ.Service.Controllers.ITunes
{
    [Route("api/itunes/[controller]")]
    [ApiController]
    public class MovementController : ControllerBase
    {
        private readonly IMovementDataAccess _repo;

        public MovementController(IMovementDataAccess repo)
        {
            _repo = repo;
        }

        [HttpGet("GetMovementsByPlaylist")]
        public async Task<List<Movement>> GetMovementsByPlaylist(int playListCode, bool showJustSummary)
        {
            return await _repo.GetList(playListCode, showJustSummary);
        }

        [HttpGet("GetMovementsSummary")]
        public async Task<List<Movement>> GetMovementsSummary(bool showJustSummary)
        {
            return await _repo.GetList(showJustSummary);
        }

    }
}
