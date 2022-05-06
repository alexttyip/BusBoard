using System.Net;
using Microsoft.VisualBasic.FileIO;
using TramBoard.API.Models.Internal;

namespace TramBoard.API.Clients;

public class StationsCsvClient
{
    public static async Task<List<Station>> GetStationsCsvParser(string url)
    {
        var response = await MakeRequest(url);

        return await ParseResponse(response);
    }

    private static Task<HttpResponseMessage> MakeRequest(string url)
    {
        var httpClient = new HttpClient();
        return httpClient.GetAsync(url);
    }

    private static async Task<List<Station>> ParseResponse(HttpResponseMessage response)
    {
        if (response.StatusCode != HttpStatusCode.OK)
        {
            Console.Out.WriteLine(response.StatusCode);
            throw new Exception();
        }

        var content = await response.Content.ReadAsStreamAsync();

        var parser = GetContentReader(content);

        var stations = new List<Station>();
        while (!parser.EndOfData)
        {
            //Processing row
            var fields = parser.ReadFields();

            if (fields == null)
            {
                break;
            }

            // Filter out non-MetroLink stations
            if (fields[9] != "M") continue;

            var coordinate = new Coordinate(double.Parse(fields[2]), double.Parse(fields[3]));
            var station = new Station(fields[0], fields[6], coordinate);

            stations.Add(station);
        }

        return stations;
    }

    private static TextFieldParser GetContentReader(Stream content)
    {
        var parser = new TextFieldParser(content);
        parser.TextFieldType = FieldType.Delimited;
        parser.SetDelimiters(",");
        // Skip header line
        parser.ReadLine();

        return parser;
    }
}