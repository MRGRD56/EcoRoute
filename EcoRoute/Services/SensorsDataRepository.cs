using System.Collections.Generic;
using System.Linq;
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

        public AnalyzedSensorsData FindDataById(string id)
        {
            if (id == null)
            {
                return null;
            }
            
            if (int.TryParse(id, out var numericId))
            {
                return AnalyzedSensorsData.ElementAtOrDefault(numericId);
            }

            return AnalyzedSensorsData.FirstOrDefault(d => d.Street.ToLower() == id.ToLower());
        }
    }
}