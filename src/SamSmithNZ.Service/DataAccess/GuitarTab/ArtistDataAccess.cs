using Dapper;
using Microsoft.Extensions.Configuration;
using SamSmithNZ.Service.DataAccess.Base;
using SamSmithNZ.Service.DataAccess.GuitarTab.Interfaces;
using SamSmithNZ.Service.Models.GuitarTab;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.GuitarTab
{
    public class ArtistDataAccess : BaseDataAccess<Artist>, IArtistDataAccess
    {
        public ArtistDataAccess(IConfiguration configuration)
        {
            SetupConnectionString(configuration);
        }

        public async Task<List<Artist>> GetList(bool? includeAllItems, bool isAdmin)
        {
            DynamicParameters parameters = new();
            if (includeAllItems != null)
            {
                parameters.Add("@IncludeInIndex", includeAllItems, DbType.Boolean);
            }
            parameters.Add("@IsAdmin", isAdmin, DbType.Boolean);

            return await GetList("Tab_GetArtists", parameters);
        }

    }
}


