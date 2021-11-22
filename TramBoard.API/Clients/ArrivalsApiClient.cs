using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TramBoard.API.Models.Api;
using TramBoard.API.Models.Internal;

namespace TramBoard.API.Clients
{
    public class ArrivalsApiClient
    {
        private const string ApiKey = "cb452a7dacbd4c4e9291d28d515b1dc5";

        public static async Task<List<Platform>> GetListOfArrivalsPlatforms()
        {
            var httpClient = new HttpClient();
            var requestMessage =
                new HttpRequestMessage(HttpMethod.Get, "https://api.tfgm.com/odata/Metrolinks");

            requestMessage.Headers.Add("Ocp-Apim-Subscription-Key", ApiKey);

            var response = await httpClient.SendAsync(requestMessage);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var arrivalsWrapper = await response.Content.ReadFromJsonAsync<ArrivalsWrapper>();
                var listOfPlatformWrappers = arrivalsWrapper.Value;
                return listOfPlatformWrappers.Select(platformWrapper => platformWrapper.ToPlatform()).ToList();
            }

            Console.Out.WriteLine(response.StatusCode);
            throw new Exception();
        }
    }
}