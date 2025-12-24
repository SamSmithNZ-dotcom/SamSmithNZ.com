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
    public class AlbumDataAccess : BaseDataAccess<Album>, IAlbumDataAccess
    {
        public AlbumDataAccess(IConfiguration configuration)
        {
            SetupConnectionString(configuration);
        }

        public async Task<List<Album>> GetList()
        {
            return await GetList("FFL_GetAlbums");
        }

        public async Task<Album> GetItem(int albumCode)
        {
            DynamicParameters parameters = new();
            parameters.Add("@albumCode", albumCode, DbType.Int32);

            return await GetItem("FFL_GetAlbums", parameters);
        }
    }
}


