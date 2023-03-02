using AppleTree.ProgramData.TreeManagement;

namespace AppleTree.ProgramData.Utils;

public static class Constants
{
    internal static bool Exited;
    internal static bool Debug;
    internal static Tree? ActiveLocalTree;
    internal static Settings? Settings;
    
    public static string? LocalTreeName
    {
        get
        {
            if (ActiveLocalTree != null)
                return ActiveLocalTree.Name ?? null;
            return null;
        }
    }
}
