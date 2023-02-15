﻿using AppleTree.data;
using AppleTree.data.exceptions;

namespace AppleTree;

internal static class Program
{
    private static Settings? _settings;
    
    
    private static void Main(string[] args)
    {
        GetSettings();
        
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
                Commands.Debug = !Commands.Debug;
            }
            Commands.DoCommand(args);
        }
        catch (IndexOutOfRangeException e)
        {
            if(Commands.Debug)
                Console.WriteLine(e);
        }
    }

    private static void Cli()
    {
        while (true) // application loop
        {
            Console.Write(">");
            string cmd = Console.ReadLine() ?? "";
            Commands.DoCommand(cmd.Split(' '));

            if (Commands.Exited) // check if exit command was run
                Environment.Exit(0);
        }
        // ReSharper disable once FunctionNeverReturns
    }

    private static void GetSettings()
    {
        try
        {
            _settings = JsonManager.GetSettings();
        }
        catch (SettingsExistsException e)
        {
            if (Commands.Debug)
                Console.WriteLine(e);
            _settings = new Settings();
            JsonManager.OverwriteSettings(_settings);
        }
        catch (JsonPresenceException e)
        {
            if (Commands.Debug)
                Console.WriteLine(e);

            Console.WriteLine("There was an error reading the settings.json file");
            Environment.Exit(3);
        }
    }
}