using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TramBoard.API;
using TramBoard.API.Models.Internal;
using TramBoard.WEB.Models;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("{postcode}")]
    public async Task<IActionResult> PostcodeResult([FromRoute] string postcode)
    {
        var userCoordinate = await Coordinate.CreateFromPostcode(postcode);

        var metroLink =
            await MetroLink.CreateFromCsv("http://odata.tfgm.com/opendata/downloads/TfGMMetroRailStops.csv");
        var stationResults = await metroLink.FetchNearbyTrams(userCoordinate);

        return View(new HomeViewModel(postcode, stationResults));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}