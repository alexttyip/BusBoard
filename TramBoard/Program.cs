using System.Threading.Tasks;

namespace TramBoard
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var postcode = args[0];
            var userCoordinate = await Coordinate.CreateFromPostcode(postcode);

            var metroLink =
                await MetroLink.CreateFromCsv("http://odata.tfgm.com/opendata/downloads/TfGMMetroRailStops.csv");
            var stationResults = await metroLink.FetchNearbyTrams(userCoordinate);
            metroLink.DisplayNearbyTrams(postcode, stationResults);
        }
    }
}