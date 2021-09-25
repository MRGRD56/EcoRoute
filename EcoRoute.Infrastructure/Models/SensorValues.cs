namespace EcoRoute.Infrastructure.Models
{
    public class SensorValues
    {
        public float Temperature { get; set; }
        public float Humidity { get; set; }
        public float Co2 { get; set; }
        public float Los { get; set; }
        public float? DustPm1 { get; set; }
        public float? DustPm25 { get; set; }
        public float? DustPm10 { get; set; }
        public float Pressure { get; set; }
        public float Aqi { get; set; }
        public float Formaldehyde { get; set; }
    }
}