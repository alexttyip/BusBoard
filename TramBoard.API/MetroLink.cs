using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TramBoard.API.Clients;
using TramBoard.API.Models.Internal;

namespace TramBoard.API
{
    public class MetroLink
    {
        public List<Station> Stations;

        public MetroLink()
        {
            Stations = new List<Station>();
        }

        public static async Task<MetroLink> CreateFromCsv(string url)
        {
            var metroLink = new MetroLink();

            var parser = await StationsCsvClient.GetStationsCsvParser(url);
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

        public void AddStation(Station station)
        {
            Stations.Add(station);
        }

        public List<KeyValuePair<double, Station>> FindNearestStations(Coordinate userCoordinate, int limit)
        {
            var nearestStations = new SortedList<double, Station>();
            foreach (var station in Stations)
            {
                var distance = station.Coordinate.DistFromOther(userCoordinate);
                nearestStations.Add(distance, station);
            }

            return nearestStations.Take(limit).ToList();
        }

        public async Task<List<StationResult>> FetchNearbyTrams(Coordinate userCoordinate)
        {
            var nearbyStations = FindNearestStations(userCoordinate, 2);

            var output = nearbyStations.Select(pair => new StationResult(pair.Value, pair.Key)).ToList();

            var listOfPlatforms = await ArrivalsApiClient.GetListOfArrivalsPlatforms();

            foreach (var platformWrapper in listOfPlatforms)
            {
                var platform = platformWrapper.ToPlatform();

                var atcoCode = platform.AtcoCode;

                var idx = nearbyStations.FindIndex(pair => pair.Value.AtcoCode == atcoCode);

                if (idx == -1 ||
                    output[idx].Platforms.Exists(
                        plat => plat.PlatformNumber == platform.PlatformNumber))
                    continue;

                output[idx].AddPlatform(platform);
            }

            return output;
        }

        public void DisplayNearbyTrams(string postcode, List<StationResult> stationResults)
        {
            Console.Out.WriteLine($"The next trams near {postcode} are:");
            foreach (var stationResult in stationResults)
            {
                Console.Out.WriteLine();
                Console.Out.WriteLine($"{stationResult.Station.Name} ({stationResult.Distance:F2} mi)");
                stationResult.Platforms.Sort((p1, p2) => p1.PlatformNumber > p2.PlatformNumber ? 1 : -1);
                foreach (var stationResultPlatform in stationResult.Platforms)
                {
                    Console.Out.WriteLine();
                    Console.Out.WriteLine($"Platform {stationResultPlatform.PlatformNumber}:");
                    foreach (var tram in stationResultPlatform.Trams)
                    {
                        if (tram == null) continue;
                        var tramStatus = tram.Wait == 0 ? tram.Status : tram.Wait + " mins";
                        Console.Out.WriteLine($"{tramStatus} - {tram.Destination} ({tram.Carriages})");
                    }
                }
            }
        }
    }
}