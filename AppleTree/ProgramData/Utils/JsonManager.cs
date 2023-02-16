using System.Text.Json;

using AppleTree.ProgramData.AppleManagement;
using AppleTree.ProgramData.TreeManagement;
using AppleTree.ProgramData.Utils.Exceptions;

namespace AppleTree.ProgramData.Utils;


public static class JsonManager
{
    private static readonly string ExePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) ?? throw new Exception("Could not find application exe path.");
    public static void OverwriteSettings(Settings? settings)
    {
        Directory.CreateDirectory($"{ExePath}/data/");
        FileStream fs = File.Open($"{ExePath}/data/settings.json", FileMode.OpenOrCreate, FileAccess.ReadWrite);
        StreamWriter sw = new StreamWriter(fs);
        sw.Write(JsonSerializer.Serialize(settings, new JsonSerializerOptions {WriteIndented = true, }));
        sw.Close();
        fs.Close();
    }

    public static Settings? GetSettings()
    {
        if (!Path.Exists($"{ExePath}/data/settings.json"))
        {
            throw new SettingsExistsException();
        }
        
        FileStream fs = File.Open($"{ExePath}/data/settings.json", FileMode.Open, FileAccess.Read);
        StreamReader sr = new StreamReader(fs);
        string json = sr.ReadToEnd();
        sr.Close();
        fs.Close();

        return JsonSerializer.Deserialize<Settings>(json) ?? throw new JsonPresenceException();
    }

    public static void NewApple(Apple apple, string applePath)
    {
        if (!Path.Exists(applePath))
            throw new PathExistsException(applePath);

        FileStream fs = File.Open($"{applePath}/.apple", FileMode.CreateNew, FileAccess.ReadWrite);
        StreamWriter sw = new StreamWriter(fs);
        string json = JsonSerializer.Serialize(apple, new JsonSerializerOptions {WriteIndented = true});
        sw.Write(json);
        sw.Close();
        fs.Close();
        Console.WriteLine($"New apple created at: {applePath}");
    }

    public static void NewTree(Tree tree, string treePath)
    {
        if (!Path.Exists(treePath))
            throw new PathExistsException(treePath);

        FileStream fs = File.Open($"{treePath}/.tree", FileMode.CreateNew, FileAccess.ReadWrite);
        StreamWriter sw = new StreamWriter(fs);
        string json = JsonSerializer.Serialize(tree, new JsonSerializerOptions {WriteIndented = true});
        sw.Write(json);
        sw.Close();
        fs.Close();
        Console.WriteLine($"New tree created at: {treePath}");
    }
}
