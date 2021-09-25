namespace EcoRoute.Infrastructure.Models
{
    public class Aqi
    {
        public float Value { get; set; }
        
        public AqiLevel Level => AqiLevel.GetLevel(Value);

        public Aqi(float value)
        {
            Value = value;
        }
    }
}