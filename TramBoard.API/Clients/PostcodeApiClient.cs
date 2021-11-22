using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TramBoard.API.Models.Api;
using TramBoard.API.Models.Internal;

namespace TramBoard.API.Clients
{
    public class PostcodeApiClient
    {
        public static async Task<UserCoordinate> GetCoordinateFromPostcode(string postcode)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"https://api.postcodes.io/postcodes/{postcode}");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var wrapper = await response.Content.ReadFromJsonAsync<CoordinateWrapper>();
                return wrapper.Result;
            }

            Console.Out.WriteLine(response.StatusCode);
            throw new PostcodeNotFoundException();
        }
    }

    public class PostcodeNotFoundException : Exception
    {
    }
}