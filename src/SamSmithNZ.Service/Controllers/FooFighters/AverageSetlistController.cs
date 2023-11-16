﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using SamSmithNZ.Service.DataAccess.FooFighters;
using SamSmithNZ.Service.DataAccess.FooFighters.Interfaces;
using SamSmithNZ.Service.Models.FooFighters;

namespace SamSmithNZ.Service.Controllers.FooFighters
{
    [Route("api/foofighters/[controller]")]
    [ApiController]
    public class AverageSetlistController : ControllerBase
    {
        private readonly IAverageSetlistDataAccess _repo;

        public AverageSetlistController(IAverageSetlistDataAccess repo)
        {
            _repo = repo;
        }

        [HttpGet("GetAverageSetlist")]
        public async Task<List<AverageSetlist>> GetAverageSetlist(int yearCode, int minimumSongCount = 0, bool showAllSongs = false)
        {
            return await _repo.GetList(yearCode, minimumSongCount, showAllSongs);
        }

    }
}
