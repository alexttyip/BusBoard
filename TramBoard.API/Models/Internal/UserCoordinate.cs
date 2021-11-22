using System.Threading.Tasks;
using TramBoard.API.Clients;

namespace TramBoard.API.Models.Internal
{
    public class UserCoordinate : Coordinate
    {
        public UserCoordinate(double latitude, double longitude, string postcode) : base(latitude, longitude)
        {
            Postcode = postcode;
        }

        public string Postcode { get; set; }

        public static async Task<UserCoordinate> CreateFromPostcode(string postcode)
        {
            return await PostcodeApiClient.GetCoordinateFromPostcode(postcode);
        }
    }
}