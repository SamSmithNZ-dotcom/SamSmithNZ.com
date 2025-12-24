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

        public FooFightersServiceApiClient(IConfiguration configuration)
        {
            _configuration = configuration;
            HttpClient client = new()
            {
                BaseAddress = new(_configuration["AppSettings:WebServiceURL"])
            };
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
            Uri url = new($"api/FooFighters/Album/GetAlbum?AlbumCode=" + albumCode, UriKind.Relative);
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
            Uri url = new($"api/FooFighters/AverageSetlist/GetAverageSetlist?YearCode=" + yearCode + "&MinimumSongCount=" + minimumSongCount + "&showAllSongs=" + showAllSongs, UriKind.Relative);
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
            Uri url = new($"api/FooFighters/Show/GetShowsByYear?YearCode=" + yearCode, UriKind.Relative);
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
            Uri url = new($"api/FooFighters/Show/GetShowsBySong?SongCode=" + songCode, UriKind.Relative);
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
            Uri url = new($"api/FooFighters/Show/GetShow?ShowCode=" + showCode, UriKind.Relative);
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
            Uri url = new($"api/FooFighters/Song/GetSongsByAlbum?AlbumCode=" + albumCode, UriKind.Relative);
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
            Uri url = new($"api/FooFighters/Song/GetSongsByShow?ShowCode=" + showCode, UriKind.Relative);
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
            Uri url = new($"api/FooFighters/Song/GetSong?SongCode=" + songCode, UriKind.Relative);
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
