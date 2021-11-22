using System.Collections.Generic;

namespace TramBoard.API.Models.Internal
{
    public class Platform
    {
        public readonly string AtcoCode;
        public int PlatformNumber;

        public Platform(int platformNumber, string atcoCode)
        {
            PlatformNumber = platformNumber;
            AtcoCode = atcoCode;
            Trams = new List<Tram>();
        }

        public List<Tram> Trams { get; }

        public void AddTram(Tram tram)
        {
            Trams.Add(tram);
        }

        public void AddAllTrams(IEnumerable<Tram> trams)
        {
            Trams.AddRange(trams);
        }
    }
}