using AppleTree.ProgramData.TreeManagement;
using AppleTree.ProgramData.Utils.Exceptions;


namespace AppleTree.ProgramData.Utils;

public static class Commands
{
    public static bool Exited;

    public static bool Debug;

    private static Tree? _activeLocalTree;

    public static string? LocalTreeName
    {
        get
        {
            if (_activeLocalTree != null)
                return _activeLocalTree.Name ?? null;
            return null;
        }
    }

    private static readonly string WorkingDir = Directory.GetCurrentDirectory();

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
            try
            {
                _activeLocalTree = new Tree { Name = cmds[1], HeadDir = Directory.GetCurrentDirectory() };
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("No tree name provided.");

                return;
            }
            try
            {
                JsonManager.NewTree(_activeLocalTree, _activeLocalTree.HeadDir);
            }
            catch (JsonPresenceException e)
            {
                if (Debug)
                {
                    Console.WriteLine(e);
                }

                Console.WriteLine($"There was an error reading the .tree file: {e.Json}");
            }
            catch (TreePresenceException e)
            {
                if (Debug)
                {
                    Console.WriteLine(e);
                }

                Console.WriteLine("A tree already exists in this directory.");
            }
        }
        else if (baseCmd is "track" or "addApple")
        {
            if (_activeLocalTree == null)
            {
                Console.WriteLine("No active tree.");

                return;
            }
            if (FileManager.IsFile($"{WorkingDir}/{cmds[1]}"))
                _activeLocalTree.AddApple(FileManager.GetFileName($"{WorkingDir}/{cmds[1]}"), $"{WorkingDir}/{cmds[1]}");
            else
            {
                if (cmds[1] == ".")
                {
                    _activeLocalTree.AddApples(WorkingDir);
                }
                else
                {
                    _activeLocalTree.AddApples($"{WorkingDir}/{cmds[1]}");
                }
            }
        }
        else if (baseCmd == "submit")
        {
            if (_activeLocalTree == null)
            {
                Console.WriteLine("No active tree.");

                return;
            }
            _activeLocalTree.UpdateApples();
        }
        else if (baseCmd == "unSubmit")
        {
            if (_activeLocalTree == null)
            {
                Console.WriteLine("No active tree.");

                return;
            }
            _activeLocalTree.RollBackApples();
        }
        else if (baseCmd == "pwd")
        {
            Console.WriteLine(WorkingDir);
        }
        else if (baseCmd == "ptd")
        {
            if (_activeLocalTree != null)
            {
                Console.WriteLine($"{_activeLocalTree.HeadDir}/.tree");
            }
            else
            {
                Console.WriteLine("No active tree");
            }
        }
        else
        {
            if (baseCmd != "")
                Console.WriteLine("That command is not recognized.");
        }
    }

    public static void LoadTree()
    {
        try
        {
            _activeLocalTree = JsonManager.GetTree($"{WorkingDir}/.tree");
        }
        catch (FileNotFoundException e)
        {
            
        }
    }
    
    private static void Help(string specificHelp)
    {
        if (specificHelp == "")
        {
            Console.WriteLine("HELP (no case-sensitive commands):" +
                              "\n    newTree - creates a new tree" +
                              "\n    track/addApple - select file/directory to start tracking in a tree" +
                              "\n    submit - submits changes of tracked files to local tree" +
                              "\n    submit n/network - submits changes of tracked files to network tree (not a thing yet)" +
                              "\n    unSubmit - rolls back changes made to local tree" +
                              "\n    unSubmit n/network - rolls back changes made to network tree (not a thing yet)" +
                              "\n    pwd - prints working directory" +
                              "\n    ptd - prints tree directory");
        }
        /*else if (specificHelp == "get")
        {
            Console.WriteLine("HELP:" +
                              "\nget (pkg)" +
                              "\n    - downloads a repository");
        }*/
    }
}
