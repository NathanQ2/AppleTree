using AppleTree.ProgramData.TreeManagement;

namespace AppleTree.ProgramData.Utils.Exceptions;

public class TreeException : Exception
{
    public Tree Tree;
    public TreeException(Tree tree)
    {
        Tree = tree;
    }
}
