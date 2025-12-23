using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Web.Services.Interfaces
{
    public interface IMandMCounterServiceApiClient
    {

        Task<float> GetMandMDataForUnit(string unit, float quantity);
        Task<float> GetMandMDataForRectangle(string unit, float height, float width, float length);
        Task<float> GetMandMDataForCylinder(string unit, float height, float radius);
        Task<float> GetPeanutMandMDataForUnit(string unit, float quantity);
        Task<float> GetPeanutMandMDataForRectangle(string unit, float height, float width, float length);
        Task<float> GetPeanutMandMDataForCylinder(string unit, float height, float radius);
        Task<float> GetSkittlesDataForUnit(string unit, float quantity);
        Task<float> GetSkittlesDataForRectangle(string unit, float height, float width, float length);
        Task<float> GetSkittlesDataForCylinder(string unit, float height, float radius);
        Task<float> GetJellyBeansDataForUnit(string unit, float quantity);
        Task<float> GetJellyBeansDataForRectangle(string unit, float height, float width, float length);
        Task<float> GetJellyBeansDataForCylinder(string unit, float height, float radius);


        Task<List<string>> GetUnitsForVolume();
        Task<List<string>> GetUnitsForContainer();
    }
}
