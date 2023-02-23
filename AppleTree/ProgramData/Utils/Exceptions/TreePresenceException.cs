using AppleTree.ProgramData.TreeManagement;

namespace AppleTree.ProgramData.Utils.Exceptions;

public class TreePresenceException : Exception
{
    public readonly Tree Tree;
    
    public TreePresenceException(Tree tree)
    {
        Tree = tree;
    }
}
