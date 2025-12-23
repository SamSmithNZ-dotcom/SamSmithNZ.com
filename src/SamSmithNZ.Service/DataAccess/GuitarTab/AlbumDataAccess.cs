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
    public class AlbumDataAccess : BaseDataAccess<Album>, IAlbumDataAccess
    {
        public AlbumDataAccess(IConfiguration configuration)
        {
            SetupConnectionString(configuration);
        }

        public async Task<List<Album>> GetList(bool isAdmin)
        {
            DynamicParameters parameters = new();
            parameters.Add("@IsAdmin", isAdmin, DbType.Boolean);

            return await GetList("Tab_GetAlbums", parameters);
        }

        public async Task<Album> GetItem(int albumCode, bool isAdmin)
        {
            DynamicParameters parameters = new();
            parameters.Add("@AlbumCode", albumCode, DbType.Int32);
            parameters.Add("@IsAdmin", isAdmin, DbType.Boolean);

            return await GetItem("Tab_GetAlbums", parameters);
        }

        public async Task<Album> SaveItem(Album item)
        {
            DynamicParameters parameters = new();
            parameters.Add("@AlbumCode", item.AlbumCode, DbType.Int32);
            parameters.Add("@ArtistName", item.ArtistName, DbType.String);
            parameters.Add("@AlbumName", item.AlbumName, DbType.String);
            parameters.Add("@AlbumYear", item.AlbumYear, DbType.Int32);
            parameters.Add("@IsBassTab", item.IsBassTab, DbType.Boolean);
            parameters.Add("@IsNewAlbum", item.IsNewAlbum, DbType.Boolean);
            parameters.Add("@IsMiscCollectionAlbum", item.IsMiscCollectionAlbum, DbType.Boolean);
            parameters.Add("@IncludeInIndex", item.IncludeInIndex, DbType.Boolean);
            parameters.Add("@IncludeOnWebsite", item.IncludeOnWebsite, DbType.Boolean);

            item.AlbumCode = await GetScalarItem<int>("Tab_SaveAlbum", parameters);
            return item;
        }

    }
}