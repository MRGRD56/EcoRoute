using System;

namespace EcoRoute.Infrastructure.Models
{
    public class SensorData
    {
        public string Address { get; set; }
        public DateTime ChangeDate { get; set; }
        public float Temperature { get; set; }
        public float Humidity { get; set; }
        public float Co2 { get; set; }
        public float Los { get; set; }
        public float DustPm1 { get; set; }
        public float DustPm25 { get; set; }
        public float DustPm10 { get; set; }
        public float Pressure { get; set; }
        public float Aqi { get; set; }
        public float Formaldehyde { get; set; }

        public SensorData()
        {
            
        }
        
        public SensorData(string address, DateTime changeDate, float temperature, float humidity, float co2, float los, 
            float dustPm1, float dustPm25, float dustPm10, float pressure, float aqi, float formaldehyde)
        {
            Address = address;
            ChangeDate = changeDate;
            Temperature = temperature;
            Humidity = humidity;
            Co2 = co2;
            Los = los;
            DustPm1 = dustPm1;
            DustPm25 = dustPm25;
            DustPm10 = dustPm10;
            Pressure = pressure;
            Aqi = aqi;
            Formaldehyde = formaldehyde;
        }
    }
}