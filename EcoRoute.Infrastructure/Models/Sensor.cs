using System.Linq;
using System.Threading.Tasks;
using EcoRoute.Common.Models;
using OpenStreetMap.Interop;

namespace EcoRoute.Infrastructure.Models
{
    public class Sensor
    {
        public string Address { get; set; }
        
        public Coordinates Coordinates { get; set; }

        public Task<Coordinates> FindCoordinatesAsync(OpenStreetMapClient osm)
        {
            return Sensor.FindCoordinatesAsync(Address, osm);
        }

        public static async Task<Coordinates> FindCoordinatesAsync(string address, OpenStreetMapClient osm)
        {
            var sensorPlaces = await osm.Search(
                country: "Russia",
                city: "Moscow",
                street: address);
            var sensorPlace = sensorPlaces.FirstOrDefault();
            var sensorCoordinates = sensorPlace == null 
                ? null 
                : new Coordinates(sensorPlace.Latitude, sensorPlace.Longitude);
            return sensorCoordinates;
        }

        public Sensor()
        {
            
        }

        public Sensor(string address, Coordinates coordinates)
        {
            Address = address;
            Coordinates = coordinates;
        }
    }
}