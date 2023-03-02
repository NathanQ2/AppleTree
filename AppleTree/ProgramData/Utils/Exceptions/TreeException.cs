using AppleTree.ProgramData.TreeManagement;

namespace AppleTree.ProgramData.Utils.Exceptions;

public class TreeException : Exception
{
    // ReSharper disable once MemberCanBePrivate.Global
    public readonly Tree Tree;
    public TreeException(Tree tree)
    {
        Tree = tree;
    }
}
