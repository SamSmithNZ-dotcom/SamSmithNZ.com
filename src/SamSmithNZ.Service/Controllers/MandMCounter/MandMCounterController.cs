using MandMCounter.Core;
using Microsoft.AspNetCore.Mvc;

namespace SamSmithNZ.Service.Controllers.MandMCounter
{
    [Produces("application/json")]
    [Route("api/MandMCounter")]
    public class MandMCounterController : Controller
    {

        [HttpGet("GetDataForUnit")]
        public float GetDataForUnit(string unit, float quantity)
        {
            return Calculator.CountMandMs(unit, quantity);
        }

        [HttpGet("GetDataForRectangle")]
        public float GetDataForRectangle(string unit, float height, float width, float length)
        {
            return Calculator.CountMandMs(unit, height, width, length);
        }

        [HttpGet("GetDataForCylinder")]
        public float GetDataForCylinder(string unit, float height, float radius)
        {
            return Calculator.CountMandMs(unit, height, radius);
        }

    }
}
