using Microsoft.AspNetCore.Mvc;
using SamSmithNZ.Service.DataAccess.GuitarTab.Interfaces;
using SamSmithNZ.Service.Models.GuitarTab;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.Controllers.GuitarTab
{
    [Route("api/guitartab/[controller]")]
    [ApiController]
    public class TabController : ControllerBase
    {
        private readonly ITabDataAccess _repo;

        public TabController(ITabDataAccess repo)
        {
            _repo = repo;
        }

        [HttpGet("GetTabs")]
        public async Task<List<Tab>> GetTabs(int albumCode, int sortOrder = 0)
        {
            return await _repo.GetList(albumCode, sortOrder);
        }

        [HttpGet("GetTab")]
        public async Task<Tab> GetTab(int tabCode)
        {
            return await _repo.GetItem(tabCode);
        }

        [HttpPost("SaveTab")]
        public async Task<bool> SaveTab(Tab item)
        {
            return await _repo.SaveItem(item);
        }

        [HttpGet("DeleteTab")]
        public async Task<bool> DeleteTab(int tabCode)
        {
            return await _repo.DeleteItem(tabCode);
        }
    }
}
