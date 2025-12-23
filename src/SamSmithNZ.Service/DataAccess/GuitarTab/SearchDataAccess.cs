using Dapper;
using Microsoft.Extensions.Configuration;
using SamSmithNZ.Service.DataAccess.Base;
using SamSmithNZ.Service.DataAccess.GuitarTab.Interfaces;
using SamSmithNZ.Service.Models.GuitarTab;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.GuitarTab
{
    public class SearchDataAccess : BaseDataAccess<Search>, ISearchDataAccess
    {
        public SearchDataAccess(IConfiguration configuration)
        {
            SetupConnectionString(configuration);
        }

        public async Task<List<Search>> GetList(Guid? recordid)
        {
            DynamicParameters parameters = new();
            parameters.Add("@RecordId", recordid, DbType.Guid);

            return await GetList("Tab_GetSearchResults", parameters);
        }

        public async Task<Guid> SaveItem(string searchText)
        {
            DynamicParameters parameters = new();
            parameters.Add("@SearchText", searchText, DbType.String);

            return await GetScalarItem<Guid>("Tab_SaveSearchParameters", parameters);
        }

    }
}
