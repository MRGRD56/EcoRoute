using System;
using System.Net.Http;

namespace EcoRoute.MapBox.Interop
{
    public class MapBoxClient
    {
        private const string BaseUrl = "https://api.mapbox.com";
        
        private readonly string _accessToken;

        public MapBoxClient(string accessToken)
        {
            _accessToken = accessToken;
        }

        private HttpClient HttpClient => new()
        {
            BaseAddress = new Uri(BaseUrl)
        };
    }
}