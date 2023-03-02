using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using AppleTree.ProgramData.AppleManagement;
using AppleTree.ProgramData.TreeManagement;
using AppleTree.ProgramData.Utils.Exceptions;

namespace AppleTree.ProgramData.Utils;


public static class JsonManager
{
    private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
    {
        WriteIndented = true,
        AllowTrailingCommas = true,
        TypeInfoResolver = new DefaultJsonTypeInfoResolver()
    };
    
    public static string ExePath { get; } = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) ?? throw new Exception("Could not find application exe path.");
    public static void OverwriteSettings(Settings? settings)
    {
        Directory.CreateDirectory($"{ExePath}/data/");
        FileStream fs = File.Open($"{ExePath}/data/settings.json", FileMode.OpenOrCreate, FileAccess.ReadWrite);
        StreamWriter sw = new StreamWriter(fs);
        sw.Write(JsonSerializer.Serialize(settings, JsonOptions));
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

        return JsonSerializer.Deserialize<Settings>(json, JsonOptions) ?? throw new JsonPresenceException();
    }

    public static void NewApple(Apple apple, string applePath, string headPath)
    {
        if (!Path.Exists(applePath))
            throw new PathExistsException(applePath);
        //read all apples
        FileStream fs = File.Open($"{headPath}/.apples", FileMode.OpenOrCreate, FileAccess.ReadWrite);
        StreamReader sr = new StreamReader(fs);
        Apple[] apples = Array.Empty<Apple>();
        try
        {
            apples = JsonSerializer.Deserialize<Apple[]>(sr.ReadToEnd(), JsonOptions) ?? throw new JsonPresenceException(sr.ReadToEnd());
        }
        catch (JsonException)
        {
            
        }
        List<Apple> curApples = new List<Apple>(apples ?? Array.Empty<Apple>());
        curApples.Add(apple);
        sr.Close();
        fs.Close();
        
        File.Delete($"{headPath}/.apples");
        fs = File.Open($"{headPath}/.apples", FileMode.Create, FileAccess.ReadWrite);
        //re-write all apples
        StreamWriter sw = new StreamWriter(fs);
        string json = JsonSerializer.Serialize(curApples, JsonOptions);
        sw.Write(json);
        sw.Close();
        fs.Close();
        Console.WriteLine($"New apple created for: {applePath}");
    }

    public static void NewTree(Tree tree, string treePath)
    {
        if (!Path.Exists(treePath))
            throw new PathExistsException(treePath);
        FileStream? fs;
        try
        {
            fs = File.Open($"{treePath}/.tree", FileMode.CreateNew, FileAccess.ReadWrite);
        }
        catch (IOException)
        {
            throw new TreePresenceException(GetTree($"{treePath}/.tree"));
        }
        StreamWriter sw = new StreamWriter(fs);
        string json = JsonSerializer.Serialize(tree, JsonOptions);
        sw.Write(json);
        sw.Close();
        fs.Close();
        Console.WriteLine($"New tree created at: {treePath}");
    }

    public static void OverwriteTree(string path, Tree tree)
    {
        if (!Path.Exists(path))
            throw new PathExistsException(path);
        FileStream? fs;
        try
        {
            File.Delete($"{path}/.tree");
            fs = File.Open($"{path}/.tree", FileMode.Create, FileAccess.ReadWrite);
        }
        catch (IOException)
        {
            throw new TreePresenceException(GetTree($"{path}/.tree"));
        }
        StreamWriter sw = new StreamWriter(fs);
        string json = JsonSerializer.Serialize(tree, JsonOptions);
        sw.Write(json);
        sw.Close();
        fs.Close();
    }

    public static Tree GetTree(string treePath)
    {
        FileStream fs = File.Open(treePath, FileMode.Open, FileAccess.Read);
        StreamReader sr = new StreamReader(fs);

        string json = sr.ReadToEnd();
        Tree tree = JsonSerializer.Deserialize<Tree>(json, JsonOptions) ?? throw new JsonPresenceException(json);
        
        // for some reason the jsonSerializer doesn't deserialize the apples array
        
        sr.Close();
        fs.Close();
        return tree;
    }
}
