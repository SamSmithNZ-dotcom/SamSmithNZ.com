using Microsoft.Extensions.Configuration;
using SamSmithNZ.Service.DataAccess.Base;
using SamSmithNZ.Service.DataAccess.GuitarTab.Interfaces;
using SamSmithNZ.Service.Models.GuitarTab;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.GuitarTab
{
    public class RatingDataAccess : BaseDataAccess<Rating>, IRatingDataAccess
    {
        public RatingDataAccess(IConfiguration configuration)
        {
            SetupConnectionString(configuration);
        }

        public async Task<List<Rating>> GetList()
        {
            return await GetList("Tab_GetRatings");
        }

    }
}


