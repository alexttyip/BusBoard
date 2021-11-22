using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace TramBoard
{
    public class Coordinate
    {
        public Coordinate(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public static async Task<Coordinate> CreateFromPostcode(string postcode)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"https://api.postcodes.io/postcodes/{postcode}");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var wrapper = await response.Content.ReadFromJsonAsync<CoordinateWrapper>();
                return wrapper.Result;
            }

            Console.Out.WriteLine(response.StatusCode);
            throw new Exception();
        }

        public double DistFromOther(Coordinate other)
        {
            // Account for differences in lengths of latitude and longitude
            var latMiles = (Latitude - other.Latitude) * 69;
            var lonMiles = (Longitude - other.Longitude) * 54.6;

            return Math.Sqrt(Math.Pow(latMiles, 2) + Math.Pow(lonMiles, 2));
        }
    }
}