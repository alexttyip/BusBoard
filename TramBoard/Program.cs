using System.Threading.Tasks;

namespace TramBoard
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var userCoordinate = await Coordinate.CreateFromPostcode("M44GQ");

            var metroLink =
                await MetroLink.CreateFromCsv("http://odata.tfgm.com/opendata/downloads/TfGMMetroRailStops.csv");
            await metroLink.DisplayNearbyTrams(userCoordinate);
        }
    }
}