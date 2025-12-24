using Dapper;
using Microsoft.Extensions.Configuration;
using SamSmithNZ.Service.DataAccess.Base;
using SamSmithNZ.Service.DataAccess.ITunes.Interfaces;
using SamSmithNZ.Service.Models.ITunes;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.ITunes
{
    public class TopArtistsDataAccess : BaseDataAccess<TopArtists>, ITopArtistsDataAccess
    {
        public TopArtistsDataAccess(IConfiguration configuration)
        {
            SetupConnectionString(configuration);
        }

        public async Task<List<TopArtists>> GetList(int playlistCode, bool showJustSummary)
        {
            DynamicParameters parameters = new();
            parameters.Add("@PlaylistCode", playlistCode, DbType.Int32);
            parameters.Add("@ShowJustSummary", showJustSummary, DbType.Boolean);

            return await GetList("ITunes_GetTopArtists", parameters);
        }
     
        public async Task<List<TopArtists>> GetList(bool showJustSummary)
        {
            DynamicParameters parameters = new();
            parameters.Add("@ShowJustSummary", showJustSummary, DbType.Boolean);

            return await GetList("ITunes_GetTopArtists", parameters);
        }  

    }
}