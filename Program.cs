using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

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

            Console.WriteLine($"Secret passcode: {secretPass}");
        }
    }
}


http://dev.virtualearth.net/REST/v1/Locations?CountryRegion=US&postalCode=02155&key={AoFEY3O5nYf9OveRWHxUwZGsiA29KH9pKiCjILV0RdVbecU_qtjws1hDYZ_wSdvT}