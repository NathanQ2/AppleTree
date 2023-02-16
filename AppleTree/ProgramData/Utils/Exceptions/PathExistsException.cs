namespace AppleTree.ProgramData.Utils.Exceptions;

public class PathExistsException : Exception
{
    public string? Path;
    public PathExistsException(string path)
    {
        Path = path;
    }

    public PathExistsException()
    {
        
    }
}
