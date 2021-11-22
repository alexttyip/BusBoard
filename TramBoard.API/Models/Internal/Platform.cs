using System.Collections.Generic;

namespace TramBoard.API.Models.Internal
{
    public class Platform
    {
        public readonly string AtcoCode;
        public readonly string Message;
        public int PlatformNumber;

        public Platform(int platformNumber, string atcoCode, string message)
        {
            PlatformNumber = platformNumber;
            AtcoCode = atcoCode;
            Trams = new List<Tram>();
            Message = message;
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