using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;

namespace BusBoard
{
    public class MetroLink
    {
        private const string ApiKey = "cb452a7dacbd4c4e9291d28d515b1dc5";

        public List<Station> Stations;

        public MetroLink()
        {
            Stations = new List<Station>();
        }

        public static async Task<MetroLink> CreateFromCsv(string url)
        {
            var metroLink = new MetroLink();

            var httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(url);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStreamAsync();

                var parser = new TextFieldParser(content);
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                // Skip header line
                parser.ReadLine();
                while (!parser.EndOfData)
                {
                    //Processing row
                    var fields = parser.ReadFields();

                    // Filter out non-MetroLink stations
                    if (fields[9] != "M") continue;

                    var coordinate = new Coordinate(double.Parse(fields[2]), double.Parse(fields[3]));
                    var station = new Station(fields[0], fields[6], coordinate);

                    metroLink.AddStation(station);
                }

                return metroLink;
            }

            Console.Out.WriteLine(response.StatusCode);
            throw new Exception();
        }

        public void AddStation(Station station)
        {
            Stations.Add(station);
        }

        public List<Station> FindNearestStations(Coordinate userCoordinate, int limit)
        {
            var nearestStations = new SortedList<double, Station>();
            foreach (var station in Stations)
            {
                double distance = station.Coordinate.DistFromOther(userCoordinate);
                nearestStations.Add(distance, station);
            }

            return nearestStations.Values.Take(limit).ToList();
        }

        public async Task DisplayNearbyTrams(Coordinate userCoordinate)
        {
            var nearbyStations = FindNearestStations(userCoordinate, 2);

            var output = new List<StationResult>();
            foreach (var nearbyStation in nearbyStations)
            {
                output.Add(new StationResult(nearbyStation));
            }

            // read in json
            var httpClient = new HttpClient();
            var requestMessage =
                new HttpRequestMessage(HttpMethod.Get, "https://api.tfgm.com/odata/Metrolinks");

            requestMessage.Headers.Add("Ocp-Apim-Subscription-Key", ApiKey);

            var response = await httpClient.SendAsync(requestMessage);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var arrivalsWrapper = await response.Content.ReadFromJsonAsync<ArrivalsWrapper>();
                var listOfPlatforms = arrivalsWrapper.value;

                foreach (var platformWrapper in listOfPlatforms)
                {
                    var platform = platformWrapper.ToPlatform();

                    var atcoCode = platform.AtcoCode;

                    var idx = nearbyStations.FindIndex(station => station.AtcoCode == atcoCode);

                    if (idx != -1)
                    {
                        output[idx].AddPlatform(platform);
                    }
                }

                // TODO print results
                Console.Out.WriteLine("Done!");
                return;
            }

            Console.Out.WriteLine(response.StatusCode);
            throw new Exception();
        }
    }
}