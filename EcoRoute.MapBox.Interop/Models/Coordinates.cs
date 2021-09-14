namespace EcoRoute.MapBox.Interop.Models
{
    public record Coordinates(
        string Longitude,
        string Latitude)
    {
        public override string ToString() => $"{Longitude},{Latitude}";
    }
}