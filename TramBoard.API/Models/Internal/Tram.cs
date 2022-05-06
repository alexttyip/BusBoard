namespace TramBoard.API.Models.Internal;

public class Tram
{
    public string Carriages;
    public string Destination;
    public string Status;
    public int Wait;

    public Tram() { }

    public Tram(string destination, string carriages, string status, int wait)
    {
        Destination = destination;
        Carriages = carriages;
        Status = status;
        Wait = wait;
    }
}