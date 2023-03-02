using AppleTree.ProgramData.TreeManagement;

namespace AppleTree.ProgramData.Utils.Exceptions;

public class TreePresenceException : Exception
{
    // ReSharper disable once MemberCanBePrivate.Global
    public readonly Tree Tree;
    
    public TreePresenceException(Tree tree)
    {
        Tree = tree;
    }
}
