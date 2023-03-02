using AppleTree.ProgramData.AppleManagement;

namespace AppleTree.ProgramData.Utils.Exceptions;

public class InvalidAppleException : Exception
{
    // ReSharper disable once MemberCanBePrivate.Global
    public readonly Apple Apple;
    
    public InvalidAppleException(Apple apple)
    {
        Apple = apple;
    }
}
