using AppleTree.ProgramData.TreeManagement;
using AppleTree.ProgramData.Utils.Exceptions;


namespace AppleTree.ProgramData.Utils;

public static class Commands
{

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
            catch (IndexOutOfRangeException)
            {
                Help("");
            }
        }
        else if (baseCmd == "exit")
        {
            Constants.Exited = true;
        }
        else if (baseCmd == "debug")
        {
            Constants.Debug = !Constants.Debug;
            Console.WriteLine($"Debugging set to: {Constants.Debug}");
        }
        else if (baseCmd == "newTree")
        {
            try
            {
                Constants.ActiveLocalTree = new Tree { Name = cmds[1], HeadDir = Directory.GetCurrentDirectory() };
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("No tree name provided.");

                return;
            }
            try
            {
                JsonManager.NewTree(Constants.ActiveLocalTree, Constants.ActiveLocalTree.HeadDir);
            }
            catch (JsonPresenceException e)
            {
                if (Constants.Debug)
                {
                    Console.WriteLine(e);
                }

                Console.WriteLine($"There was an error reading the .tree file: {e.Json}");
            }
            catch (TreePresenceException e)
            {
                if (Constants.Debug)
                {
                    Console.WriteLine(e);
                }

                Console.WriteLine("A tree already exists in this directory.");
            }
        }
        else if (baseCmd is "track" or "addApple")
        {
            if (Constants.ActiveLocalTree == null)
            {
                Console.WriteLine("No active tree.");

                return;
            }
            if (FileManager.IsFile($"{WorkingDir}/{cmds[1]}"))
                Constants.ActiveLocalTree.AddApple(FileManager.GetFileName($"{WorkingDir}/{cmds[1]}"), $"{WorkingDir}/{cmds[1]}");
            else
            {
                Constants.ActiveLocalTree.AddApples(cmds[1] == "." ? WorkingDir : $"{WorkingDir}/{cmds[1]}");
            }
        }
        else if (baseCmd == "submit")
        {
            if (Constants.ActiveLocalTree == null)
            {
                Console.WriteLine("No active tree.");

                return;
            }
            Constants.ActiveLocalTree.UpdateApples();
        }
        else if (baseCmd == "revert")
        {
            if (Constants.ActiveLocalTree == null)
            {
                Console.WriteLine("No active tree.");

                return;
            }
            Constants.ActiveLocalTree.RollBackApples();
        }
        else if (baseCmd == "pwd")
        {
            Console.WriteLine(WorkingDir);
        }
        else if (baseCmd == "ptd")
        {
            Console.WriteLine(Constants.ActiveLocalTree != null ? $"{Constants.ActiveLocalTree.HeadDir}/.tree" : "No active tree");
        }
        else if (baseCmd == "addToPath")
        {
            FileManager.AddToPath(JsonManager.ExePath);
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
            Constants.ActiveLocalTree = JsonManager.GetTree($"{WorkingDir}/.tree");
        }
        catch (FileNotFoundException)
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
                              "\n    revert - rolls back un-submitted changes made to local tree" +
                              "\n    revert n/network - rolls back un-submitted changes made to network tree (not a thing yet)" +
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
