using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BusBoard
{
    public class Coordinate
    {
        public double Latitude;
        public double Longitude;

        public Coordinate()
        {
        }

        public Coordinate(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public static async Task<Coordinate> CreateFromPostcode(string postcode)
        {
            var httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync($"https://api.postcodes.io/postcodes/{postcode}");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                var wrapper = JsonConvert.DeserializeObject<CoordinateWrapper>(content);
                return wrapper.result;
            }

            Console.Out.WriteLine(response.StatusCode);
            throw new Exception();
        }

        public double DistFromOther(Coordinate other)
        {
            return Math.Sqrt(Math.Pow(Latitude - other.Latitude, 2) + Math.Pow(Longitude - other.Longitude, 2));
        }
    }
}