using Dapper;
using Microsoft.Extensions.Configuration;
using SamSmithNZ.Service.DataAccess.Base;
using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.WorldCup
{
    public class ThirdPlaceMatchDataAccess : BaseDataAccess<ThirdPlaceMatch>
    {
        public ThirdPlaceMatchDataAccess(IConfiguration configuration)
        {
            base.SetupConnectionString(configuration);
        }

        public async Task<List<ThirdPlaceMatch>> GetList(int tournamentCode, int combinationCode)
        {
            DynamicParameters parameters = new();
            parameters.Add("@TouramentCode", tournamentCode, DbType.Int32);
            parameters.Add("@GroupsInTop8", combinationCode, DbType.String);

            return await base.GetList("FB_GetTournamentThirdPlacedMatchups", parameters);
        }
        
    }
}
