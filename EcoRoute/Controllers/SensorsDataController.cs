using System;
using System.Linq;
using EcoRoute.Infrastructure.Models;
using EcoRoute.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcoRoute.Controllers
{
    [Route("api/[controller]/[action]")]
    public class SensorsDataController : ControllerBase
    {
        private readonly SensorsDataRepository _sensorsDataRepository;

        public SensorsDataController(SensorsDataRepository sensorsDataRepository)
        {
            _sensorsDataRepository = sensorsDataRepository;
        }

        [HttpGet]
        public IActionResult AnalyzedData()
        {
            return Ok(_sensorsDataRepository.AnalyzedSensorsData);
        }

        private dynamic GetResultFromData(AnalyzedSensorsData data)
        {
            return new
            {
                data.Street,
                ContaminationLevel = data.GeoData.Average(g => g.Aqi.Value),
                Sensors = data.GeoData.Select(g => g.Sensor.Coordinates.AsArray())
            };
        }
        
        // [HttpGet]
        // public IActionResult AllData()
        // {
        //     var sensorsDataList = _sensorsDataRepository.AnalyzedSensorsData;
        //     
        //     return Ok(sensorsDataList.Select(data => GetResultFromData(data)));
        // }
        
        [HttpGet("{page:int}")]
        public IActionResult AllData([FromRoute] int page)
        {
            const int pageLimit = 10;
            
            var sensorsDataList = _sensorsDataRepository.AnalyzedSensorsData;

            var sensorsData = page <= 0
                ? sensorsDataList
                : sensorsDataList.Skip((page - 1) * pageLimit).Take(pageLimit);
            
            return Ok(sensorsData.Select(data => GetResultFromData(data)));
        }

        [HttpGet]
        public IActionResult AllData([FromQuery] string street)
        {
            if (string.IsNullOrWhiteSpace(street))
            {
                return Ok(_sensorsDataRepository.AnalyzedSensorsData
                    .Select(data => GetResultFromData(data)));
            }
            
            var sensorsDataById = _sensorsDataRepository.FindDataById(street);
            if (sensorsDataById == null)
            {
                return BadRequest(null);
            }

            var result = GetResultFromData(sensorsDataById);
            return Ok(result);
        }
    }
}