namespace AppleTree.ProgramData.Utils.Exceptions;

public class JsonPresenceException : Exception
{
    public readonly string? Json;
    public JsonPresenceException()
    {
        
    }

    public JsonPresenceException(string json)
    {
        Json = json;
    }
}
