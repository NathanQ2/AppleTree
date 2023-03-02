namespace AppleTree.ProgramData.Utils.Exceptions;

public class PathExistsException : Exception
{
    // ReSharper disable once MemberCanBePrivate.Global
    public readonly string? Path;
    public PathExistsException(string path)
    {
        Path = path;
    }

    public PathExistsException()
    {
        
    }
}
