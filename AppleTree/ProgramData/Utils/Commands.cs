using AppleTree.ProgramData.AppleManagement;
using AppleTree.ProgramData.TreeManagement;
using AppleTree.ProgramData.Utils.Exceptions;


namespace AppleTree.ProgramData.Utils;

public static class Commands
{
    public static bool Exited;

    public static bool Debug;

    public static Tree? ActiveLocalTree;

    public static string? LocalTreeName
    {
        get
        {
            if (ActiveLocalTree != null)
                return ActiveLocalTree.Name ?? null;
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
            ActiveLocalTree = new Tree {Name = cmds[1], HeadDir = Directory.GetCurrentDirectory()};

            try
            {
                JsonManager.NewTree(ActiveLocalTree, ActiveLocalTree.HeadDir);
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
            if (ActiveLocalTree == null)
            {
                Console.WriteLine("No active tree.");

                return;
            }

            if (cmds[1] == ".")
            {
                string[] potentialDirs = Directory.GetDirectories(WorkingDir);
                List<string> finalPaths = new List<string>(), finalNames = new List<string>();

                for (int i = 0; i < potentialDirs.Length; i++) // exclude apple already tracked
                {
                    Apple curApple = ActiveLocalTree.Apples[i];
                    if (WorkingDir + "/" + ActiveLocalTree.Apples[i].TrackedFilePath != potentialDirs[i])
                    {
                        finalPaths.Add(curApple.TrackedFilePath ?? throw new InvalidAppleException(curApple));
                        finalNames.Add(curApple.TrackedFileName ?? throw new InvalidAppleException(curApple));
                    }
                }
                ActiveLocalTree.AddApples(finalNames, finalPaths);
                Console.WriteLine("Added File(s)");
                foreach (string path in finalPaths)
                {
                    Console.WriteLine(path);
                }
            }
            else
            {
                ActiveLocalTree.AddApple(FileManager.GetFileName($"{WorkingDir}/{cmds[1]}"), $"{WorkingDir}/{cmds[1]}");
            }
        }
        else if (baseCmd == "submit")
        {
            
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
