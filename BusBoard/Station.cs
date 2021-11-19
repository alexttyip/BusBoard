namespace BusBoard
{
    public class Station
    {
        public string AtcoCode;
        public Coordinate Coordinate;
        public string Name;

        public Station(string atcoCode, string name, Coordinate coordinate)
        {
            AtcoCode = atcoCode;
            Name = name;
            Coordinate = coordinate;
        }
    }
}