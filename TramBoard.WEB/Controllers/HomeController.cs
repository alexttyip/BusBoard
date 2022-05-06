using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TramBoard.API;
using TramBoard.API.Clients;
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

    [HttpGet("postcodeNotFound")]
    public IActionResult PostcodeNotFound()
    {
        return View();
    }

    [HttpGet("results")]
    public async Task<IActionResult> PostcodeResult([FromQuery] string postcode, [FromQuery] int limit)
    {
        // TODO Error handling
        postcode = postcode.ToUpper();

        UserCoordinate userCoordinate;
        try
        {
            userCoordinate = await UserCoordinate.CreateFromPostcode(postcode);
        }
        catch (PostcodeNotFoundException e)
        {
            return Redirect("postcodeNotFound");
        }

        var metroLink =
            await MetroLink.CreateFromCsv("http://odata.tfgm.com/opendata/downloads/TfGMMetroRailStops.csv");
        var stationResults = await metroLink.FetchNearbyTrams(userCoordinate, limit);

        return View(new HomeViewModel(userCoordinate.Postcode, stationResults, limit));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}