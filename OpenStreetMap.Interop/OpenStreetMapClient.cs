using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using EcoRoute.Common.Http;
using Mrgrd56.JsonHttpClient;
using Newtonsoft.Json.Linq;
using OpenStreetMap.Interop.Models;

namespace OpenStreetMap.Interop
{
    public class OpenStreetMapClient
    {
        private const string BaseUri = "https://nominatim.openstreetmap.org/";

        private HttpClient HttpClient
        {
            get
            {
                var httpClient = new HttpClient
                {
                    BaseAddress = new Uri(BaseUri, UriKind.Absolute)
                };
                
                httpClient.DefaultRequestHeaders.Add("User-Agent", "PostmanRuntime/7.28.4");
                httpClient.DefaultRequestHeaders.Add("Accept", "*/*");

                return httpClient;
            }
        }

        public async Task<List<Place>> Search(
            string query = null,
            string street = null,
            string city = null,
            string country = null,
            string state = null,
            string postalCode = null)
        {
            var queryString = new QueryStringBuilder()
                .AddParameter("q", query)
                .AddParameter("street", street)
                .AddParameter("city", city)
                .AddParameter("country", country)
                .AddParameter("state", state)
                .AddParameter("postalcode", postalCode)
                .AddParameter("format", "jsonv2")
                .Build();

            var requestUri = new Uri("search?" + queryString, UriKind.Relative);
            var response = await HttpClient.GetAsync<List<Place>>(requestUri);
            return response;
        }
    }
}