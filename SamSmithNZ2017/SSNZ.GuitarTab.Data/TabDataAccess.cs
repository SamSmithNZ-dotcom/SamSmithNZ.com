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
    public class TabDataAccess : GenericDataAccess<Tab>
    {
        public async Task<List<Tab>> GetDataAsync(short albumCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@album_code", albumCode, DbType.Int16);

            List<Tab> result = await base.GetListAsync("spKS_Tab_GetTracks", parameters);

            return result.ToList<Tab>();
        }

        public async Task<Tab> GetItemAsync(short trackCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@track_code", trackCode, DbType.Int16);

            return await base.GetItemAsync("spKS_Tab_GetArtists", parameters);
        }

        public async Task<bool> SaveItemAsync(Tab item)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@track_code", item.TrackCode, DbType.Int32);
            parameters.Add("@album_code", item.AlbumCode, DbType.Int32);
            parameters.Add("@track_name", item.TrackName, DbType.String);
            parameters.Add("@track_text", item.TrackText, DbType.String);
            parameters.Add("@track_order", item.TrackOrder, DbType.Int32);
            parameters.Add("@rating", item.Rating, DbType.Int32);
            parameters.Add("@tuning_code", item.TuningCode, DbType.Int32);

            return await base.PostItemAsync("spKS_Tab_SaveTrack", parameters);
        }

        public async Task<bool> DeleteItem(short trackCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@track_code", trackCode, DbType.Int32);

            return await base.PostItemAsync("spKS_Tab_DeleteTrack", parameters);
        }

    }
}


