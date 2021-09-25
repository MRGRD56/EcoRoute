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
                ContaminationLevel = data.GeoData.Average(g => g.Values.Aqi),
                Sensors = data.GeoData.Select(g => g.Sensor.Coordinates.AsArray())
            };
        }

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
                return new JsonResult(null);
            }

            var result = GetResultFromData(sensorsDataById);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult Street([FromRoute] string id)
        {
            var sensorsDataById = _sensorsDataRepository.FindDataById(id);
            if (sensorsDataById == null)
            {
                return new JsonResult(null);
            }
            var result = new
            {
                sensorsDataById.Street,
                Values = new SensorValues
                {
                    Temperature = sensorsDataById.GeoData.Average(sd => sd.Values.Temperature),
                    Humidity = sensorsDataById.GeoData.Average(sd => sd.Values.Humidity),
                    Co2 = sensorsDataById.GeoData.Average(sd => sd.Values.Co2),
                    Los = sensorsDataById.GeoData.Average(sd => sd.Values.Los),
                    DustPm1 = sensorsDataById.GeoData.Average(sd => sd.Values.DustPm1),
                    DustPm25 = sensorsDataById.GeoData.Average(sd => sd.Values.DustPm25),
                    DustPm10 = sensorsDataById.GeoData.Average(sd => sd.Values.DustPm10),
                    Pressure = sensorsDataById.GeoData.Average(sd => sd.Values.Pressure),
                    Aqi = sensorsDataById.GeoData.Average(sd => sd.Values.Aqi),
                    Formaldehyde = sensorsDataById.GeoData.Average(sd => sd.Values.Formaldehyde)
                }
            };
            return Ok(result);
        }
    }
}