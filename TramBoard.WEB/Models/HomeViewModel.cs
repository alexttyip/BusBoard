using System.Collections.Generic;
using TramBoard.API.Models.Internal;

namespace TramBoard.WEB.Models
{
    public class HomeViewModel
    {
        public HomeViewModel(string postcode, List<StationResult> stationResults)
        {
            Postcode = postcode;
            this.stationResults = stationResults;
        }

        public string Postcode { get; set; }
        public List<StationResult> stationResults { get; set; }

        public static string GetStatusString(Tram tram)
        {
            return tram.Wait == 0 ? tram.Status : $"Due in {tram.Wait} min{(tram.Wait != 1 ? "s" : "")}";
        }
    }
}