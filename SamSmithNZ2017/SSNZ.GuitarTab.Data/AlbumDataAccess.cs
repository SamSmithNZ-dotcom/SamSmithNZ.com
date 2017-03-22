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
    public class AlbumDataAccess : GenericDataAccess<Album>
    {
        public async Task<List<Album>> GetDataAsync(bool isAdmin)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@is_admin", isAdmin, DbType.Boolean);

            return await base.GetListAsync("spKS_Tab_GetAlbums", parameters);
        }

        public async Task<Album> GetItemAsync(int albumCode, bool isAdmin)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@album_code", albumCode, DbType.Int32);
            parameters.Add("@is_admin", isAdmin, DbType.Boolean);

            return await base.GetItemAsync("spKS_Tab_GetAlbums", parameters);
        }

        public async Task<Album> SaveItemAsync(Album item)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@album_code", item.AlbumCode, DbType.Int32);
            parameters.Add("@artist_name", item.ArtistName, DbType.String);
            parameters.Add("@album_name", item.AlbumName, DbType.String);
            parameters.Add("@album_year", item.AlbumYear, DbType.Int32);
            parameters.Add("@is_bass_tab", item.IsBassTab, DbType.Boolean);
            parameters.Add("@is_new_album", item.IsNewAlbum, DbType.Boolean);
            parameters.Add("@is_misc_collection_album", item.IsMiscCollectionAlbum, DbType.Boolean);
            parameters.Add("@include_in_index", item.IncludeInIndex, DbType.Boolean);
            parameters.Add("@include_on_website", item.IncludeOnWebsite, DbType.Boolean);

            item.AlbumCode = await base.GetScalarAsync<short>("spKS_Tab_SaveAlbum", parameters);
            return item;
        }

    }
}