using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using EcoRoute.MapBox.Models;
using Newtonsoft.Json.Linq;

namespace EcoRoute.MapBox.Interop
{
    public interface IMapBoxClient
    {
        Task<T> InvokeAsync<T>(string endpoint, HttpMethod httpMethod = null,
            string queryString = null, object requestBody = null);

        Task<JObject> GetDirectionsAsync(RoutingProfile profile, IEnumerable<Coordinates> coordinates);
        Task<JObject> GetDirectionsAsync(string profile, string coordinates);
    }
}