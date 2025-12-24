using Dapper;
using Microsoft.Extensions.Configuration;
using SamSmithNZ.Service.DataAccess.Base;
using SamSmithNZ.Service.DataAccess.FooFighters.Interfaces;
using SamSmithNZ.Service.Models.FooFighters;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.FooFighters
{
    public class ShowDataAccess : BaseDataAccess<Show>, IShowDataAccess
    {
        public ShowDataAccess(IConfiguration configuration)
        {
            SetupConnectionString(configuration);
        }

        public async Task<List<Show>> GetListByYearAsync(int yearCode)
        {
            DynamicParameters parameters = new();
            parameters.Add("@yearCode", yearCode, DbType.Int32);

            return await GetList("FFL_GetShows", parameters);
        }

        public async Task<List<Show>> GetListBySongAsync(int songCode)
        {
            DynamicParameters parameters = new();
            parameters.Add("@songCode", songCode, DbType.Int32);

            return await GetList("FFL_GetShows", parameters);
        }

        public async Task<List<Show>> GetListByFFLCode()
        {
            DynamicParameters parameters = new();
            parameters.Add("@GetFFLCodes", true, DbType.Int32);

            return await GetList("FFL_GetShows", parameters);
        }

        public async Task<Show> GetItem(int showCode)
        {
            DynamicParameters parameters = new();
            parameters.Add("@showCode", showCode, DbType.Int32);

            return await GetItem("FFL_GetShows", parameters);
        }

        public async Task<bool> SaveItem(Show show)
        {
            DynamicParameters parameters = new();
            parameters.Add("@ShowCode", show.ShowCode, DbType.Int32);
            parameters.Add("@ShowDate", show.ShowDate, DbType.DateTime);
            parameters.Add("@ShowLocation", show.ShowLocation, DbType.String);
            parameters.Add("@ShowCity", show.ShowCity, DbType.String);
            parameters.Add("@ShowCountry", show.ShowCountry, DbType.String);

            return await SaveItem("FFL_SaveShow", parameters);
        }
    }
}


