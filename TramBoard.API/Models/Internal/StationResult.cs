namespace TramBoard.API.Models.Internal;

public class StationResult
{
    public double Distance;
    public List<Platform> Platforms;
    public Station Station;

    public StationResult(Station station, double distance)
    {
        Station = station;
        Distance = distance;
        Platforms = new List<Platform>();
    }

    public void AddPlatform(Platform platform)
    {
        Platforms.Add(platform);
    }
}