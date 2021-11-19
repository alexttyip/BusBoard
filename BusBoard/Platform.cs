using System.Collections.Generic;

namespace BusBoard
{
    public class Platform
    {
        public int PlatCode;
        public List<Tram> Trams { get; }

        public Platform(){}
        
        public Platform(int platCode)
        {
            PlatCode = platCode;
            Trams = new List<Tram>();
        }

        public void AddTram(Tram tram)
        {
            Trams.Add(tram);
        }
}
}