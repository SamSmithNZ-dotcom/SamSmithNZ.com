using Dapper;
using Microsoft.Extensions.Configuration;
using SamSmithNZ.Service.DataAccess.Base;
using SamSmithNZ.Service.DataAccess.WorldCup.Interfaces;
using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.WorldCup
{
    public class PlayoffDataAccess : BaseDataAccess<Playoff>, IPlayoffDataAccess
    {
        public PlayoffDataAccess(IConfiguration configuration)
        {
            base.SetupConnectionString(configuration);
        }

        public async Task<List<Playoff>> GetList(int tournamentCode)
        {
            DynamicParameters parameters = new();
            parameters.Add("@TournamentCode", tournamentCode, DbType.Int32);

            return await base.GetList("FB_GetMigratePlayoffs", parameters);
        }

        public async Task<bool> SaveItem(Playoff setup)
        {
            DynamicParameters parameters = new();
            parameters.Add("@TournamentCode", setup.TournamentCode, DbType.Int32);
            parameters.Add("@RoundCode", setup.RoundCode, DbType.String);
            parameters.Add("@GameNumber", setup.GameNumber, DbType.Int32);
            parameters.Add("@Team1Prereq", setup.Team1Prereq, DbType.String);
            parameters.Add("@Team2Prereq", setup.Team2Prereq, DbType.String);
            parameters.Add("@SortOrder", setup.SortOrder, DbType.Int32);           

            await base.SaveItem("FB_SaveMigratePlayoffsGames", parameters);
            return true;
        }
    }
}