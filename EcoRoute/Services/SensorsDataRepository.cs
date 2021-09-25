using System.Collections.Generic;
using EcoRoute.Data;
using EcoRoute.Infrastructure.Models;

namespace EcoRoute.Services
{
    public class SensorsDataRepository
    {
        private static List<AnalyzedSensorsData> _analyzedSensorsData;

        static SensorsDataRepository()
        {
            _analyzedSensorsData = SensorsDataParser.ParseAnalyzedJson(@"Data\analyzed_data.json");
        }
        
        public List<AnalyzedSensorsData> AnalyzedSensorsData => _analyzedSensorsData;
    }
}