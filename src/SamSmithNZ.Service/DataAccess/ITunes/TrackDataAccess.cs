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
    public class TrackDataAccess : BaseDataAccess<Track>, ITrackDataAccess
    {
        public TrackDataAccess(IConfiguration configuration)
        {
            SetupConnectionString(configuration);
        }

        public async Task<List<Track>> GetList(int playlistCode, bool showJustSummary)
        {
            DynamicParameters parameters = new();
            parameters.Add("@PlaylistCode", playlistCode, DbType.Int32);
            parameters.Add("@ShowJustSummary", showJustSummary, DbType.Boolean);

            return await GetList("ITunes_GetTracks", parameters);
        }

        //public async Task<Track> GetItem(int playlistCode, bool showJustSummary, String trackName)
        //{
        //    DynamicParameters parameters = new();
        //    parameters.Add("@PlaylistCode", playlistCode, DbType.Int32);
        //    parameters.Add("@ShowJustSummary", showJustSummary, DbType.Boolean);
        //    parameters.Add("@TrackName", trackName, DbType.String);

        //    return await GetItem("ITunes_GetTracks", parameters);
        //}

        //public async Task<bool> SaveItem(Track track)
        //{
        //    DynamicParameters parameters = new();
        //    parameters.Add("@PlaylistCode", track.PlaylistCode, DbType.Int32);
        //    parameters.Add("@TrackName", track.TrackName, DbType.String);
        //    parameters.Add("@AlbumName", track.AlbumName, DbType.String);
        //    parameters.Add("@ArtistName", track.ArtistName, DbType.String);
        //    parameters.Add("@PlayCount", track.PlayCount, DbType.Int32);
        //    parameters.Add("@Ranking", track.Ranking, DbType.Int32);
        //    parameters.Add("@Rating", track.Rating, DbType.Int32);

        //    return await SaveItem("ITunes_ImportInsertTrack", parameters);
        //}

        //public async Task<List<Track>> ValidateTracksForPlaylist(int playlistCode)
        //{
        //    DynamicParameters parameters = new();
        //    parameters.Add("@PlaylistCode", playlistCode, DbType.Int32);
        //    int timeOut = 3600; //One hour

        //    return await GetList("ITunes_ImportValidateTracksForDuplicates", parameters, timeOut);
        //}

        //public async Task<bool> SetTrackRanksForPlaylist(int playlistCode)
        //{
        //    DynamicParameters parameters = new();
        //    parameters.Add("@PlaylistCode", playlistCode, DbType.Int32);
        //    int timeOut = 3600; //One hour

        //    return await SaveItem("ITunes_ImportSetTrackRanks", parameters, timeOut);
        //}

    }
}