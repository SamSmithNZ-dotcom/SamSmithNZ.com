using Microsoft.AspNetCore.Mvc;
using SamSmithNZ.Service.DataAccess.GuitarTab.Interfaces;
using SamSmithNZ.Service.Models.GuitarTab;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.Controllers.GuitarTab
{
    [Route("api/guitartab/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchDataAccess _repo;

        public SearchController(ISearchDataAccess repo)
        {
            _repo = repo;
        }

        [HttpGet("GetSearchResults")]
        public async Task<List<Search>> GetSearchResults(string searchText, Guid? recordId = null)
        {
            if (recordId == null)
            {
                recordId = await _repo.SaveItem(searchText);
            }
            return await _repo.GetList(recordId);
        }

    }
}
