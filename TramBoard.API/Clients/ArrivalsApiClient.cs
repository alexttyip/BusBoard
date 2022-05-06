using System.Net;
using System.Net.Http.Json;
using TramBoard.API.Models.Api;
using TramBoard.API.Models.Internal;

namespace TramBoard.API.Clients;

public class ArrivalsApiClient
{
    private const string ApiKey = "cb452a7dacbd4c4e9291d28d515b1dc5";

    public static async Task<List<Platform>> FetchListOfArrivalsPlatforms()
    {
        var response = await MakeRequest();
        return await ParseResponse(response);
    }

    private static Task<HttpResponseMessage> MakeRequest()
    {
        var httpClient = new HttpClient();
        var requestMessage =
            new HttpRequestMessage(HttpMethod.Get, "https://api.tfgm.com/odata/Metrolinks");

        requestMessage.Headers.Add("Ocp-Apim-Subscription-Key", ApiKey);

        return httpClient.SendAsync(requestMessage);
    }

    private static async Task<List<Platform>> ParseResponse(HttpResponseMessage response)
    {
        if (response.StatusCode != HttpStatusCode.OK)
        {
            Console.Out.WriteLine(response.StatusCode);
            throw new Exception();
        }

        var arrivalsWrapper = await response.Content.ReadFromJsonAsync<ArrivalsWrapper>();
        var listOfPlatformWrappers = arrivalsWrapper.Value;
        return listOfPlatformWrappers.Select(platformWrapper => platformWrapper.ToPlatform()).ToList();
    }
}
