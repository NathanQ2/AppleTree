namespace AppleTree.ProgramData.Utils.Exceptions;

public class InvalidNameAndPathException : Exception
{
    public int NamesCount { get; set; }
    public int PathsCount { get; set; }
}
