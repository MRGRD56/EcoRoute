using CsvHelper.Configuration;
using EcoRoute.Infrastructure.Models;

namespace EcoRoute.Data.Mapping
{
    public sealed class SensorDataMap : ClassMap<SensorData>
    {
        public SensorDataMap()
        {
            Map(s => s.Address);
            Map(s => s.ChangeDate)
                .Convert(s => s.Value.ChangeDate
                    .ToUniversalTime()
                    .ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"));
            Map(s => s.Temperature);
            Map(s => s.Humidity);
            Map(s => s.Co2);
            Map(s => s.Los);
            Map(s => s.DustPm1);
            Map(s => s.DustPm25);
            Map(s => s.DustPm10);
            Map(s => s.Pressure);
            Map(s => s.Aqi);
            Map(s => s.Formaldehyde);
        }
    }
}