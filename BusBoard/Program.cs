using System.Threading.Tasks;

namespace BusBoard
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var userCoordinate = await Coordinate.CreateFromPostcode("M44GQ");

            var metrolink =
                await MetroLink.CreateFromCsv("http://odata.tfgm.com/opendata/downloads/TfGMMetroRailStops.csv");
            await metrolink.DisplayNearbyTrams(userCoordinate);
        }
    }
}