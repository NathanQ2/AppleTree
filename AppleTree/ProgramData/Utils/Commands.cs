using AppleTree.ProgramData.TreeManagement;


namespace AppleTree.ProgramData.Utils;

public static class Commands
{
    public static bool Exited;

    public static bool Debug;

    public static Tree? ActiveLocalTree;

    public static void DoCommand(string[] cmds)
    {
        string baseCmd = cmds[0];

        if (baseCmd == "help")
        {
            try
            {
                Help(cmds[1]);
            }
            catch (IndexOutOfRangeException e)
            {
                Help("");
            }
        }
        else if (baseCmd == "exit")
        {
            Exited = true;
        }
        else if (baseCmd == "debug")
        {
            Debug = !Debug;
            Console.WriteLine($"Debugging set to: {Debug}");
        }
        else if (baseCmd == "newTree")
        {
            ActiveLocalTree = new Tree {Name = cmds[1], HeadDir = Directory.GetCurrentDirectory()};
            JsonManager.NewTree(ActiveLocalTree, ActiveLocalTree.HeadDir);
        }
        else
        {
            Console.WriteLine("Command not recognized");
        }
    }
    
    public static void Help(string specificHelp)
    {
        if (specificHelp == "")
        {
            Console.WriteLine("HELP (no case-sensitive commands):" +
                              "\n    - appletree get (treeName/appleName)" +
                              "\n    - appletree addTree (ip)" +
                              "\n    - appletree lsTrees" +
                              "\n    - appletree lsApples (treeName)");
        }
        else if (specificHelp == "get")
        {
            Console.WriteLine("HELP:" +
                              "\nget (pkg)" +
                              "\n    - downloads a repository");
        }
    }
}
