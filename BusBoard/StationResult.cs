using System.Collections.Generic;

namespace BusBoard
{
    public class StationResult
    {
        public List<Platform> Platforms;
        public Station Station;

        public StationResult(Station station)
        {
            Station = station;
            Platforms = new List<Platform>();
        }

        public void AddPlatform(Platform platform)
        {
            Platforms.Add(platform);
        }
    }
}