using AppleTree.ProgramData.AppleManagement;

namespace AppleTree.ProgramData.Utils.Exceptions;

public class InvalidAppleException : Exception
{
    public Apple Apple;
    
    public InvalidAppleException(Apple apple)
    {
        Apple = apple;
    }
}
