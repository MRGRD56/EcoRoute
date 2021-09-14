using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using EcoRoute.Common.Extensions;
using EcoRoute.MapBox.Interop.Models;
using Mrgrd56.JsonHttpClient;
using Newtonsoft.Json.Linq;

namespace EcoRoute.MapBox.Interop
{
    public class MapBoxClient : IMapBoxClient
    {
        private const string BaseUrl = "https://api.mapbox.com/";
        
        private readonly string _accessToken;

        public MapBoxClient(string accessToken)
        {
            _accessToken = accessToken;
        }

        private static HttpClient HttpClient => new()
        {
            BaseAddress = new Uri(BaseUrl, UriKind.Absolute)
        };

        public async Task<T> InvokeAsync<T>(string endpoint, HttpMethod httpMethod = null, 
            string queryString = null, object requestBody = null)
        {
            httpMethod ??= HttpMethod.Get;
            if (endpoint.StartsWith("/"))
            {
                endpoint = endpoint.Remove(0, 1);
            }
            if (!string.IsNullOrWhiteSpace(queryString))
            {
                if (queryString.StartsWith("?"))
                {
                    queryString = queryString.Remove(0, 1);
                }
                if (!queryString.StartsWith("&"))
                {
                    queryString = "&" + queryString;
                }
            }

            var requestUri = new Uri($"{endpoint}?access_token={_accessToken}{queryString}", UriKind.Relative);
            return await HttpClient.SendAsync<T>(requestUri, httpMethod, requestBody);
        }

        public async Task<JObject> GetDirectionsAsync(RoutingProfile profile, IEnumerable<Coordinates> coordinates)
        {
            var profileString = profile.GetStringValue();
            var coordinatesString = string.Join(";", coordinates);

            return await GetDirectionsAsync(profileString, coordinatesString);
        }

        public async Task<JObject> GetDirectionsAsync(string profile, string coordinates)
        {
            return await InvokeAsync<JObject>($"/directions/v5/{profile}/{coordinates}", queryString: "geometries=geojson");
        }
    }
}