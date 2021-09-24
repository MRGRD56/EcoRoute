using System;

namespace EcoRoute.Infrastructure.Models
{
    public record SensorData(
        string Address,
        DateTime ChangeDate,
        float Temperature,
        float Humidity,
        float Co2,
        float Los,
        float DustPm1,
        float DustPm25,
        float DustPm10,
        float Pressure,
        float Aqi,
        float Formaldehyde);
}