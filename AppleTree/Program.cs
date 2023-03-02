using AppleTree.ProgramData.Utils;
using AppleTree.ProgramData.Utils.Exceptions;

// Apple = a file
// Tree = a project
// Branch = a branch

namespace AppleTree;

internal static class Program
{
    private static void Main(string[] args)
    {
        GetSettings();
        Commands.LoadTree();
        
        if (args.Length != 0)
            ReadArgs(args);
        else
        {
            Cli();
        }
    }

    private static void ReadArgs(string[] args)
    {
        try
        {
            if (args[0] == "debug")
            {
                Constants.Debug = !Constants.Debug;
            }
            Commands.DoCommand(args);
        }
        catch (IndexOutOfRangeException e)
        {
            if(Constants.Debug)
                Console.WriteLine(e);
        }
    }

    private static void Cli()
    {
        while (true) // application loop
        {
            Console.Write($"{Constants.LocalTreeName ?? ""}>");
            string cmd = Console.ReadLine() ?? "";
            Commands.DoCommand(cmd.Split(' '));

            if (Constants.Exited) // check if exit command was run
                Environment.Exit(0);
        }
        // ReSharper disable once FunctionNeverReturns
    }

    private static void GetSettings()
    {
        try
        {
            Constants.Settings = JsonManager.GetSettings();
        }
        catch (SettingsExistsException e)
        {
            if (Constants.Debug)
                Console.WriteLine(e);
            Constants.Settings = new Settings();
            JsonManager.OverwriteSettings(Constants.Settings);
        }
        catch (JsonPresenceException e)
        {
            if (Constants.Debug)
                Console.WriteLine(e);

            Console.WriteLine("There was an error reading the settings.json file");
            Environment.Exit(3);
        }
    }
}