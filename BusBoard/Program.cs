using System.Threading.Tasks;

namespace BusBoard
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Coordinate.CreateFromPostcode("OL10 4AR");
        }
    }
}