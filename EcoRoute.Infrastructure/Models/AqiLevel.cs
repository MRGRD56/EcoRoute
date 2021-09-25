using System.Collections.Generic;
using System.Linq;

namespace EcoRoute.Infrastructure.Models
{
    public record AqiLevel(
        string Name,
        float MinValue,
        string Color)
    {
        public static AqiLevel GetLevel(float value)
        {
            var levels = Levels.OrderByDescending(level => level.MinValue).ToList();
            foreach (var aqiLevel in levels)
            {
                if (value > aqiLevel.MinValue)
                {
                    return aqiLevel;
                }
            }

            return null;
        }
        
        public static readonly List<AqiLevel> Levels = new()
        {
            new AqiLevel("Good", 0, "#46e346"),
            new AqiLevel("Moderate", 51, "#f5f647"),
            new AqiLevel("Unhealthy for Sensetive Groups", 101, "#f69c47"),
            new AqiLevel("Unhealthy", 151, "#f64747"),
            new AqiLevel("Very Unhealthy", 201, "#ad4478"),
            new AqiLevel("Hazardous", 301, "#99435b")
        };
    }
}