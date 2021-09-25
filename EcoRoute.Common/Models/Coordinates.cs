using System;
using System.Text.RegularExpressions;

namespace EcoRoute.Common.Models
{
    public record Coordinates(
        string Latitude,
        string Longitude)
    {
        public string[] AsArray() => new[] { Latitude, Longitude };

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

            return decimal.TryParse(split[0], out _) && decimal.TryParse(split[1], out _);
        }

        public override string ToString() => $"{Latitude},{Longitude}";
    }
}