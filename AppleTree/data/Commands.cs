using System.Runtime.CompilerServices;

namespace AppleTree.misc;

public static class Commands
{
    public static bool Exited;

    public static bool Debug;

    public static void DoCommand(string[] cmds)
    {
        string baseCmd = cmds[0].ToLower();

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
        else if (baseCmd == "addtree")
        {
            
        }
    }
    
    public static void Help(string specificHelp)
    {
        if (specificHelp == "")
        {
            Console.WriteLine("HELP (no case-sensitive commands):" +
                              "\n    - appletree install (pkg)" +
                              "\n    - appletree addTree (ip)" +
                              "\n    - appletree lsTrees" +
                              "\n    - appletree lsApples");
        }
        else if (specificHelp == "install")
        {
            Console.WriteLine("HELP:" +
                              "\nInstall (pkg)" +
                              "\n    - downloads a repository");
        }
    }
}
