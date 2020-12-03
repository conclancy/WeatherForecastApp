using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WeatherForecastLibrary;

namespace WeatherForecastApp
{
    public class Program
    {
        

        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");
            var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();

            // Get secrets from secrets.json 
            var secretProvider = config.Providers.First();
            if (!secretProvider.TryGet("ApiKey", out var secretPass)) return;

            Location l = new Location();
            l.LocationAsync(secretPass).Wait();

            Console.WriteLine($"Coordinates: {l.Cor}");
        }
    }

    public class Location 
    {
        private IHttpClientFactory _clientFactory;

        public float[] Cor { get; private set; }

        public async Task LocationAsync(string secrets)
        {
            LocationModel location;
            string errorString;

            string locationUrl = $"http://dev.virtualearth.net/REST/v1/Locations?CountryRegion=US&postalCode=02155&key={secrets}";
            var locationRequest = new HttpRequestMessage(HttpMethod.Get, locationUrl);
            var locationClient = _clientFactory.CreateClient();

            HttpResponseMessage locationResponse = await locationClient.SendAsync(locationRequest);

            location = await locationResponse.Content.ReadFromJsonAsync<LocationModel>();

            foreach (Resourceset rs in location.resourceSets)
            {
                foreach (Resource r in rs.resources)
                {
                    Cor = r.bbox;
                }
            }
        }
    }
}