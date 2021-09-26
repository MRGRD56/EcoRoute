using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace EcoRoute.Common.Models
{
    public record Coordinates(
        string Latitude,
        string Longitude)
    {
        public string[] AsArray() => new[] { Latitude, Longitude };
        public decimal[] AsNumbersArray() => new[] 
        { 
            decimal.Parse(Latitude, CultureInfo.InvariantCulture), 
            decimal.Parse(Longitude, CultureInfo.InvariantCulture) 
        };

        public static Coordinates Parse(string coordinatesString)
        {
            var split = coordinatesString.Split(",");
            if (split.Length != 2)
            {
                throw new FormatException();
            }

            return new Coordinates(split[0], split[1]);
        }

        public static bool TryParse(string coordinatesString, out Coordinates coordinates)
        {
            try
            {
                coordinates = Coordinates.Parse(coordinatesString);
                return true;
            }
            catch (FormatException)
            {
                coordinates = null;
                return false;
            }
        }

        public static bool IsCoordinates(string coordinatesString)
        {
            var split = coordinatesString.Split(",");
            if (split.Length != 2)
            {
                return false;
            }

            return decimal.TryParse(split[0], NumberStyles.Any, CultureInfo.InvariantCulture, out _) && 
                   decimal.TryParse(split[1], NumberStyles.Any, CultureInfo.InvariantCulture, out _);
        }

        public override string ToString() => $"{Latitude},{Longitude}";
    }
}