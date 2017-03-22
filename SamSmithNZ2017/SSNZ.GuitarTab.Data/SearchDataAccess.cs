﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using SSNZ.GuitarTab.Models;
using Dapper;
using System.Data;
using System.Threading.Tasks;

namespace SSNZ.GuitarTab.Data
{
    public class SearchDataAccess : GenericDataAccess<Search>
    {
        public async Task<List<Search>> GetDataAsync(Guid recordid)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@record_id", recordid, DbType.Guid);

            return await base.GetListAsync("spKS_Tab_GetSearchResults", parameters);
        }

        public async Task<Guid> SaveItemAsync(String searchText)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@search_text", searchText, DbType.String);

            return await base.GetScalarAsync<Guid>("spKS_Tab_SaveSearchParameters", parameters);
        }

    }
}
