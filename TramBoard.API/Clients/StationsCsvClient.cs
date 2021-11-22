using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;

namespace TramBoard.API.Clients
{
    public class StationsCsvClient
    {
        public static async Task<TextFieldParser> GetStationsCsvParser(string url)
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
                return parser;
            }

            Console.Out.WriteLine(response.StatusCode);
            throw new Exception();
        }
    }
}