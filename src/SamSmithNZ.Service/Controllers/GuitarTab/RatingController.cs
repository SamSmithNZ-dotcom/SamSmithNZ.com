﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using SamSmithNZ.Service.DataAccess.GuitarTab;
using SamSmithNZ.Service.DataAccess.GuitarTab.Interfaces;
using SamSmithNZ.Service.Models.GuitarTab;

namespace SamSmithNZ.Service.Controllers.GuitarTab
{
    [Route("api/guitartab/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRatingDataAccess _repo;

        public RatingController(IRatingDataAccess repo)
        {
            _repo = repo;
        }

        [HttpGet("GetRatings")]
        public async Task<List<Rating>> GetRatings()
        {
            return await _repo.GetList();
        }
    }
}
