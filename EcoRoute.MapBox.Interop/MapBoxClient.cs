using System;
using System.Net.Http;
using System.Threading.Tasks;
using Mrgrd56.JsonHttpClient;

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

        private static HttpClient HttpClient => new()
        {
            BaseAddress = new Uri(BaseUrl)
        };

        public async Task<T> InvokeAsync<T>(string endpoint, HttpMethod httpMethod = null, string queryString = null, object requestBody = null)
        {
            httpMethod ??= HttpMethod.Get;
            if (!string.IsNullOrWhiteSpace(queryString) && !queryString.StartsWith("&"))
            {
                queryString = "&" + queryString;
            }

            return await HttpClient.SendAsync<T>($"{endpoint}?access_token={_accessToken}{queryString}", httpMethod, requestBody);
        }
    }
}