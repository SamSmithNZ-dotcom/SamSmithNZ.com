using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using SamSmithNZ.Service.Models.FooFighters;
using SamSmithNZ.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SamSmithNZ.Web.Services
{
    public class FooFightersServiceApiClient : BaseServiceApiClient, IFooFightersServiceApiClient
    {
        private readonly IConfiguration _configuration;

        public FooFightersServiceApiClient(IConfiguration configuration, HttpClient client)
        {
            _configuration = configuration;
            client.BaseAddress = new Uri(_configuration["AppSettings:WebServiceURL"]);
            base.SetupClient(client);
        }

        public async Task<List<Album>> GetAlbums()
        {
            Uri url = new($"api/FooFighters/Album/GetAlbums", UriKind.Relative);
            List<Album> results = await base.ReadMessageList<Album>(url);
            if (results == null)
            {
                return new List<Album>();
            }
            else
            {
                return results;
            }
        }

        public async Task<Album> GetAlbum(int albumCode)
        {
            string albumUrl = QueryHelpers.AddQueryString("api/FooFighters/Album/GetAlbum", "AlbumCode", albumCode.ToString());
            Uri url = new(albumUrl, UriKind.Relative);
            Album result = await base.ReadMessageItem<Album>(url);
            if (result == null)
            {
                return new Album();
            }
            else
            {
                return result;
            }
        }

        public async Task<List<AverageSetlist>> GetAverageSetlist(int yearCode, int minimumSongCount = 0, bool showAllSongs = false)
        {
            Dictionary<string, string> queryParams = new Dictionary<string, string>
            {
                { "YearCode", yearCode.ToString() },
                { "MinimumSongCount", minimumSongCount.ToString() },
                { "showAllSongs", showAllSongs.ToString() }
            };
            string averageSetlistUrl = QueryHelpers.AddQueryString("api/FooFighters/AverageSetlist/GetAverageSetlist", queryParams);
            Uri url = new(averageSetlistUrl, UriKind.Relative);
            List<AverageSetlist> results = await base.ReadMessageList<AverageSetlist>(url);
            if (results == null)
            {
                return new List<AverageSetlist>();
            }
            else
            {
                return results;
            }
        }

        public async Task<List<Show>> GetShowsByYear(int yearCode)
        {
            string showsByYearUrl = QueryHelpers.AddQueryString("api/FooFighters/Show/GetShowsByYear", "YearCode", yearCode.ToString());
            Uri url = new(showsByYearUrl, UriKind.Relative);
            List<Show> results = await base.ReadMessageList<Show>(url);
            if (results == null)
            {
                return new List<Show>();
            }
            else
            {
                return results;
            }
        }

        public async Task<List<Show>> GetShowsBySong(int songCode)
        {
            string showsBySongUrl = QueryHelpers.AddQueryString("api/FooFighters/Show/GetShowsBySong", "SongCode", songCode.ToString());
            Uri url = new(showsBySongUrl, UriKind.Relative);
            List<Show> results = await base.ReadMessageList<Show>(url);
            if (results == null)
            {
                return new List<Show>();
            }
            else
            {
                return results;
            }
        }

        public async Task<Show> GetShow(int showCode)
        {
            string showUrl = QueryHelpers.AddQueryString("api/FooFighters/Show/GetShow", "ShowCode", showCode.ToString());
            Uri url = new(showUrl, UriKind.Relative);
            Show result = await base.ReadMessageItem<Show>(url);
            if (result == null)
            {
                return new Show();
            }
            else
            {
                return result;
            }
        }

        public async Task<List<Song>> GetSongs()
        {
            Uri url = new($"api/FooFighters/Song/GetSongs", UriKind.Relative);
            List<Song> results = await base.ReadMessageList<Song>(url);
            if (results == null)
            {
                return new List<Song>();
            }
            else
            {
                return results;
            }
        }

        public async Task<List<Song>> GetSongsByAlbum(int albumCode)
        {
            string songsByAlbumUrl = QueryHelpers.AddQueryString("api/FooFighters/Song/GetSongsByAlbum", "AlbumCode", albumCode.ToString());
            Uri url = new(songsByAlbumUrl, UriKind.Relative);
            List<Song> results = await base.ReadMessageList<Song>(url);
            if (results == null)
            {
                return new List<Song>();
            }
            else
            {
                return results;
            }
        }

        public async Task<List<Song>> GetSongsByShow(int showCode)
        {
            string songsByShowUrl = QueryHelpers.AddQueryString("api/FooFighters/Song/GetSongsByShow", "ShowCode", showCode.ToString());
            Uri url = new(songsByShowUrl, UriKind.Relative);
            List<Song> results = await base.ReadMessageList<Song>(url);
            if (results == null)
            {
                return new List<Song>();
            }
            else
            {
                return results;
            }
        }

        public async Task<Song> GetSong(int songCode)
        {
            string songUrl = QueryHelpers.AddQueryString("api/FooFighters/Song/GetSong", "SongCode", songCode.ToString());
            Uri url = new(songUrl, UriKind.Relative);
            Song result = await base.ReadMessageItem<Song>(url);
            if (result == null)
            {
                return new Song();
            }
            else
            {
                return result;
            }
        }

        public async Task<List<Year>> GetYears()
        {
            Uri url = new($"api/FooFighters/Year/GetYears" , UriKind.Relative);
            List<Year> results = await base.ReadMessageList<Year>(url);
            if (results == null)
            {
                return new List<Year>();
            }
            else
            {
                return results;
            }
        }
       
    }
}
