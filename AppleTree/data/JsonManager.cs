using System.Text.Json;
using AppleTree.data.exceptions;

namespace AppleTree.data;

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
}
