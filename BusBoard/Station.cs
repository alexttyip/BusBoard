using System.Collections.Generic;

namespace BusBoard
{
    public class Station
    {
        public string AtcoCode;
        public string Name;
        public List<Platform> Platforms { get; }
        public Coordinate Coordinate;

        public Station()
        {
        }

        public Station(string atcoCode, string name, Coordinate coordinate)
        {
            AtcoCode = atcoCode;
            Name = name;
            Platforms = new List<Platform>();
            Coordinate = coordinate;
        }

        public void AddPlatform(Platform platform)
        {
            Platforms.Add(platform);
        }
    }
}