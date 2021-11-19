using System;

namespace BusBoard
{
    public class Coordinate
    {
        public double Lat;
        public double Lon;

        public Coordinate()
        {
        }

        public Coordinate(double lat, double lon)
        {
            Lat = lat;
            Lon = lon;
        }

        public Coordinate CreateFromPostcode(string postcode)
        {
            // TODO fetch from API
            return new Coordinate();
        }

        public double DistFromOther(Coordinate other)
        {
            return Math.Sqrt(Math.Pow(Lat - other.Lat, 2) + Math.Pow(Lon - other.Lon, 2));
        }
    }
}