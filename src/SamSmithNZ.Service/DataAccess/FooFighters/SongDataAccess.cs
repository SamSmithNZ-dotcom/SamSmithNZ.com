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
    public class SongDataAccess : BaseDataAccess<Song>, ISongDataAccess
    {
        public SongDataAccess(IConfiguration configuration)
        {
            SetupConnectionString(configuration);
        }

        public async Task<List<Song>> GetList()
        {
            return await GetList("FFL_GetSongs");
        }

        public async Task<List<Song>> GetListForAlbumAsync(int albumCode)
        {
            DynamicParameters parameters = new();
            parameters.Add("@AlbumCode", albumCode, DbType.Int32);

            return await GetList("FFL_GetSongs", parameters);
        }

        public async Task<List<Song>> GetListForShowAsync(int showCode)
        {
            DynamicParameters parameters = new();
            parameters.Add("@ShowCode", showCode, DbType.Int32);

            return await GetList("FFL_GetSongs", parameters);
        }

        public async Task<Song> GetItem(int songCode)
        {
            DynamicParameters parameters = new();
            parameters.Add("@SongCode", songCode, DbType.Int32);

            return await GetItem("FFL_GetSongs", parameters);
        }

        public async Task<bool> SaveItem(int songCode, int showCode, int showSongOrder)
        {
            DynamicParameters parameters = new();
            parameters.Add("@SongCode", songCode, DbType.Int32);
            parameters.Add("@ShowCode", showCode, DbType.Int32);
            parameters.Add("@ShowSongOrder", showSongOrder, DbType.Int32);

            return await SaveItem("FFL_SaveShowSong", parameters);
        }
    }
}


