using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.Steam
{
    public static class Utility
    {
        //https://steamcommunity.com/dev
        //https://developer.valvesoftware.com/wiki/Steam_Web_API#GetPlayerSummaries_.2v0001.29
        //https://portablesteamwebapi.codeplex.com/documentation
        public const string MySteamWebAPIKey = "35D42236AAC777BEDB12CDEB625EF289";

        private readonly static HttpClient _httpClient = new();

        public static string ConvertMinutesToFriendlyTime(long minutes)
        {
            long hh = minutes / (long)60;
            string result = hh + " hrs";

            return result;
        }

        //A generic object
        public async static Task<string> GetPageAsStringAsync(Uri address)
        {
            string result;
            try
            {
                Debug.WriteLine("Getting item from external API: " + address.ToString());
                result = await _httpClient.GetStringAsync(address);
            }
            catch (HttpRequestException e)
            {
                //An error occured, capture the error response
                System.Diagnostics.Debug.WriteLine("Error: {0}", e.Message);
                Console.WriteLine(e.Message);
                result = e.Message;
            }
            return result;
        }
    }
}
