using System.Net;
using Microsoft.VisualBasic.FileIO;
using TramBoard.API.Models.Internal;

namespace TramBoard.API.Clients;

public class StationsCsvClient
{
    public static async Task<List<Station>> GetStationsCsvParser(string url)
    {
        var httpClient = new HttpClient();
        var response = await httpClient.GetAsync(url);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            var content = await response.Content.ReadAsStreamAsync();

            var parser = new TextFieldParser(content);
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");
            // Skip header line
            parser.ReadLine();

            var stations = new List<Station>();
            while (!parser.EndOfData)
            {
                //Processing row
                var fields = parser.ReadFields();

                // Filter out non-MetroLink stations
                if (fields[9] != "M") continue;

                var coordinate = new Coordinate(double.Parse(fields[2]), double.Parse(fields[3]));
                var station = new Station(fields[0], fields[6], coordinate);

                stations.Add(station);
            }

            return stations;
        }

        Console.Out.WriteLine(response.StatusCode);
        throw new Exception();
    }
}