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
    public class PlaylistDataAccess : BaseDataAccess<Playlist>, IPlaylistDataAccess
    {
        public PlaylistDataAccess(IConfiguration configuration)
        {
            SetupConnectionString(configuration);
        }

        public async Task<List<Playlist>> GetList()
        {
            DynamicParameters parameters = new();

            return await GetList("ITunes_GetPlaylists", parameters);
        }

        public async Task<Playlist> GetItem(int playlistCode)
        {
            DynamicParameters parameters = new();
            parameters.Add("@PlaylistCode", playlistCode, DbType.Int32);

            return await GetItem("ITunes_GetPlaylists", parameters);
        }

        //public async Task<int> SaveItem(DateTime dteDate)
        //{
        //    DynamicParameters parameters = new();
        //    parameters.Add("@PlaylistDate", dteDate, DbType.DateTime);

        //    return await GetScalarItem<int>("ITunes_ImportCreateNewPlayList", parameters);
        //}

        //public async Task<bool> DeleteItem(int playlistCode)
        //{
        //    DynamicParameters parameters = new();
        //    parameters.Add("@PlaylistCode", playlistCode, DbType.Int32);

        //    return await SaveItem("ITunes_ImportDeletePlaylistTracks", parameters);
        //}

    }
}