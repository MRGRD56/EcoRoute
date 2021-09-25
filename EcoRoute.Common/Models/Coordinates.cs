namespace EcoRoute.Common.Models
{
    public record Coordinates(
        string Latitude,
        string Longitude)
    {
        public string[] AsArray() => new[] { Latitude, Longitude };
        
        public override string ToString() => $"{Latitude},{Longitude}";
    }
}