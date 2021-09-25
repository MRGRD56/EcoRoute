using EcoRoute.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcoRoute.Controllers
{
    [Route("api/analyzed_data")]
    public class AnalyzedDataController : ControllerBase
    {
        private readonly SensorsDataRepository _sensorsDataRepository;

        public AnalyzedDataController(SensorsDataRepository sensorsDataRepository)
        {
            _sensorsDataRepository = sensorsDataRepository;
        }
        
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_sensorsDataRepository.AnalyzedSensorsData);
        }
    }
}