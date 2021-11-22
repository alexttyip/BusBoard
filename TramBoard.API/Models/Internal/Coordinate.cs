using System;
using System.Threading.Tasks;
using TramBoard.API.Clients;

namespace TramBoard.API.Models.Internal
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
            return await PostcodeApiClient.GetCoordinateFromPostcode(postcode);
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