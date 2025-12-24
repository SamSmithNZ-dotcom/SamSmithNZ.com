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
    public class TabDataAccess : BaseDataAccess<Tab>, ITabDataAccess
    {
        public TabDataAccess(IConfiguration configuration)
        {
            SetupConnectionString(configuration);
        }

        public async Task<List<Tab>> GetList(int albumCode, int sortOrder)
        {
            DynamicParameters parameters = new();
            parameters.Add("@AlbumCode", albumCode, DbType.Int32);
            parameters.Add("@SortOrder", sortOrder, DbType.Int32);

            return await GetList("Tab_GetTabs", parameters);
        }

        public async Task<Tab> GetItem(int tabCode)
        {
            DynamicParameters parameters = new();
            parameters.Add("@TabCode", tabCode, DbType.Int32);

            List<Tab> results = await GetList("Tab_GetTabs", parameters);

            //Sometimes a tab can be associated with multiple albums, so we need to get a list and return the first record in those cases
            if (results.Count > 0)
            {
                return results[0];
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> SaveItem(Tab item)
        {
            DynamicParameters parameters = new();
            parameters.Add("@TabCode", item.TabCode, DbType.Int32);
            parameters.Add("@AlbumCode", item.AlbumCode, DbType.Int32);
            parameters.Add("@TabName", item.TabName, DbType.String);
            parameters.Add("@TabText", item.TabText, DbType.String);
            parameters.Add("@TabOrder", item.TabOrder, DbType.Int32);
            parameters.Add("@Rating", item.Rating, DbType.Int32);
            parameters.Add("@TuningCode", item.TuningCode, DbType.Int32);

            return await SaveItem("Tab_SaveTab", parameters);
        }

        public async Task<bool> DeleteItem(int tabCode)
        {
            DynamicParameters parameters = new();
            parameters.Add("@TabCode", tabCode, DbType.Int32);

            return await SaveItem("Tab_DeleteTab", parameters);
        }

    }
}


