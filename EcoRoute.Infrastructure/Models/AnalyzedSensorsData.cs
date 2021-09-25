using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcoRoute.Common.Models;
using OpenStreetMap.Interop;

namespace EcoRoute.Infrastructure.Models
{
    public class AnalyzedSensorsData
    {
        public string Street { get; set; }
        
        public List<SensorGeoData> GeoData { get; set; }

        public AnalyzedSensorsData()
        {
            
        }

        public AnalyzedSensorsData(string street, List<SensorGeoData> geoData)
        {
            Street = street;
            GeoData = geoData;
        }

        public static List<AnalyzedSensorsData> FromSensorsData(IEnumerable<SensorData> sensorData)
        {
            var openStreetMapClient = new OpenStreetMapClient();
            
            var sensorDataList = sensorData.ToList();
            var sensorDataByStreets = sensorDataList
                .GroupBy(s => s.GetStreet())
                .Select(g => new
                {
                    Street = g.Key,
                    SensorData = g.ToList()
                })
                .ToList();

            var data = sensorDataByStreets.Select(s =>
            {
                var sensorDataByAddresses = s.SensorData
                    .GroupBy(d => d.GetAddress())
                    .Select(g =>
                    {
                        var address = g.Key;
                        var lastAverageAqi = g
                            .OrderBy(sd => sd.ChangeDate)
                            .TakeLast(50)
                            .Average(sd => sd.Aqi);

                        var sensorCoordinates = Sensor.FindCoordinatesAsync(address, openStreetMapClient)
                            .GetAwaiter().GetResult();

                        if (sensorCoordinates == null)
                        {
                            Console.WriteLine("Coordinates null - " + address);
                        }
                        var sensor = new Sensor(address, sensorCoordinates);
                        
                        return new SensorGeoData
                        {
                            Sensor = sensor,
                            Aqi = new Aqi(lastAverageAqi)
                        };
                    })
                    .ToList();

                return new AnalyzedSensorsData
                {
                    Street = s.Street,
                    GeoData = sensorDataByAddresses
                };
            }).ToList();

            return data;
        }
    }
}