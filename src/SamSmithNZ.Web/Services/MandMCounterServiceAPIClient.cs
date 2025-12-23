using Microsoft.Extensions.Configuration;
using SamSmithNZ.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SamSmithNZ.Web.Services
{
    public class MandMCounterServiceApiClient : BaseServiceApiClient, IMandMCounterServiceApiClient
    {
        private readonly IConfiguration _configuration;

        public MandMCounterServiceApiClient(IConfiguration configuration)
        {
            _configuration = configuration;
            HttpClient client = new()
            {
                BaseAddress = new(_configuration["AppSettings:MandMWebServiceURL"])
            };
            base.SetupClient(client);
        }

        public async Task<float> GetMandMDataForUnit(string unit, float quantity)
        {
            Uri url = new($"api/MandMCounter/GetDataForUnit?unit=" + unit + "&quantity=" + quantity, UriKind.Relative);
            return await base.GetMessageScalar<float>(url);
        }

        public async Task<float> GetMandMDataForRectangle(string unit, float height, float width, float length)
        {
            Uri url = new($"api/MandMCounter/GetDataForRectangle?unit=" + unit + "&height=" + height + "&width=" + width + "&length=" + length, UriKind.Relative);
            return await base.GetMessageScalar<float>(url);
        }

        public async Task<float> GetMandMDataForCylinder(string unit, float height, float radius)
        {
            Uri url = new($"api/MandMCounter/GetDataForCylinder?unit=" + unit + "&height=" + height + "&radius=" + radius, UriKind.Relative);
            return await base.GetMessageScalar<float>(url);
        }

        public async Task<float> GetPeanutMandMDataForUnit(string unit, float quantity)
        {
            Uri url = new($"api/PeanutMandMCounter/GetDataForUnit?unit=" + unit + "&quantity=" + quantity, UriKind.Relative);
            return await base.GetMessageScalar<float>(url);
        }

        public async Task<float> GetPeanutMandMDataForRectangle(string unit, float height, float width, float length)
        {
            Uri url = new($"api/PeanutMandMCounter/GetDataForRectangle?unit=" + unit + "&height=" + height + "&width=" + width + "&length=" + length, UriKind.Relative);
            return await base.GetMessageScalar<float>(url);
        }

        public async Task<float> GetPeanutMandMDataForCylinder(string unit, float height, float radius)
        {
            Uri url = new($"api/PeanutMandMCounter/GetDataForCylinder?unit=" + unit + "&height=" + height + "&radius=" + radius, UriKind.Relative);
            return await base.GetMessageScalar<float>(url);
        }


        public async Task<float> GetSkittlesDataForUnit(string unit, float quantity)
        {
            Uri url = new($"api/SkittleCounter/GetDataForUnit?unit=" + unit + "&quantity=" + quantity, UriKind.Relative);
            return await base.GetMessageScalar<float>(url);
        }

        public async Task<float> GetSkittlesDataForRectangle(string unit, float height, float width, float length)
        {
            Uri url = new($"api/SkittleCounter/GetDataForRectangle?unit=" + unit + "&height=" + height + "&width=" + width + "&length=" + length, UriKind.Relative);
            return await base.GetMessageScalar<float>(url);
        }

        public async Task<float> GetSkittlesDataForCylinder(string unit, float height, float radius)
        {
            Uri url = new($"api/SkittleCounter/GetDataForCylinder?unit=" + unit + "&height=" + height + "&radius=" + radius, UriKind.Relative);
            return await base.GetMessageScalar<float>(url);
        }

        public async Task<float> GetJellybeansDataForUnit(string unit, float quantity)
        {
            Uri url = new($"api/SkittleCounter/GetDataForUnit?unit=" + unit + "&quantity=" + quantity, UriKind.Relative);
            return await base.GetMessageScalar<float>(url);
        }

        public async Task<float> GetJellybeansDataForRectangle(string unit, float height, float width, float length)
        {
            Uri url = new($"api/SkittleCounter/GetDataForRectangle?unit=" + unit + "&height=" + height + "&width=" + width + "&length=" + length, UriKind.Relative);
            return await base.GetMessageScalar<float>(url);
        }

        public async Task<float> GetJellybeansDataForCylinder(string unit, float height, float radius)
        {
            Uri url = new($"api/SkittleCounter/GetDataForCylinder?unit=" + unit + "&height=" + height + "&radius=" + radius, UriKind.Relative);
            return await base.GetMessageScalar<float>(url);
        }

        public async Task<List<string>> GetUnitsForVolume()
        {
            Uri url = new($"api/Units/GetUnitsForVolume", UriKind.Relative);
            List<string> results = await base.ReadMessageList<string>(url);
            if (results == null)
            {
                return new();
            }
            else
            {
                return results;
            }
        }

        public async Task<List<string>> GetUnitsForContainer()
        {
            Uri url = new($"api/Units/GetUnitsForContainer", UriKind.Relative);
            List<string> results = await base.ReadMessageList<string>(url);
            if (results == null)
            {
                return new();
            }
            else
            {
                return results;
            }
        }

    }
}
