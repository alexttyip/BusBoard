using System.Collections.Generic;
using TramBoard.API.Models.Internal;

namespace TramBoard.WEB.Models
{
    public class HomeViewModel
    {
        public HomeViewModel(string postcode, List<StationResult> stationResults, int limit)
        {
            Postcode = postcode;
            StationResults = stationResults;
            Limit = limit;
        }

        public string Postcode { get; set; }
        public List<StationResult> StationResults { get; set; }
        public int Limit { get; set; }

        public static string GetStatusString(Tram tram)
        {
            return tram.Wait == 0 ? tram.Status : $"Due in {tram.Wait} min{(tram.Wait != 1 ? "s" : "")}";
        }
    }
}