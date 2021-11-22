using System.Collections.Generic;
using TramBoard.API.Models.Internal;

namespace TramBoard.API.Models.Api
{
    public class PlatformWrapper
    {
        public int Id { get; set; }
        public string AtcoCode { get; set; }
        public string MessageBoard { get; set; }

        public string Dest0 { get; set; }
        public string Carriages0 { get; set; }
        public string Status0 { get; set; }
        public string Wait0 { get; set; }

        public string Dest1 { get; set; }
        public string Carriages1 { get; set; }
        public string Status1 { get; set; }
        public string Wait1 { get; set; }

        public string Dest2 { get; set; }
        public string Carriages2 { get; set; }
        public string Status2 { get; set; }
        public string Wait2 { get; set; }

        public Platform ToPlatform()
        {
            var tram0 = Dest0.Length != 0
                ? new Tram(
                    Dest0, Carriages0, Status0, int.Parse(Wait0))
                : null;
            var tram1 = Dest1.Length != 0
                ? new Tram(
                    Dest1, Carriages1, Status1, int.Parse(Wait1))
                : null;
            var tram2 = Dest2.Length != 0
                ? new Tram(
                    Dest2, Carriages2, Status2, int.Parse(Wait2))
                : null;

            var trams = new List<Tram> {tram0, tram1, tram2};

            var platformNumber = int.Parse(AtcoCode[^1..]);
            var atcoCode = AtcoCode[..^1];

            var platform = new Platform(platformNumber, atcoCode, MessageBoard);
            platform.AddAllTrams(trams);
            return platform;
        }
    }
}