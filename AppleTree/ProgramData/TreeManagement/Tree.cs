using System.Text.Json.Serialization;
using AppleTree.ProgramData.AppleManagement;
using AppleTree.ProgramData.Utils;
using AppleTree.ProgramData.Utils.Exceptions;

namespace AppleTree.ProgramData.TreeManagement;

public class Tree
{
    public string? Name { get; set; }
    public string? HeadDir { get; set; }
    [JsonInclude]
    public List<Apple> Apples { get; private set; } = new List<Apple>();

    public void AddApple(Apple apple, string filePath)
    {
        Apples.Add(apple);
        JsonManager.NewApple(apple, filePath, HeadDir ?? throw new TreePresenceException(this)); // this func will write all required info files
    }

    public void AddApple(string name, string filePath)
    {
        Apples.Add(new Apple {TrackedFileName = name, TrackedFilePath = filePath, TrackedFile = File.ReadAllText(filePath)});
        JsonManager.OverwriteTree(HeadDir ?? throw new TreeException(this), this);
        Console.WriteLine($"Tracking: {FileManager.GetRelativePath(filePath, HeadDir)}");
    }

    public void AddApples(string directoryPath)
    {
        string[] filePaths = FileManager.GetAllFilesIn(directoryPath);
        List<string> fileNames = new List<string>();
        foreach (string file in filePaths)
        {
            fileNames.Add(FileManager.GetFileName(file));
        }
        
        if (fileNames.Count != filePaths.Length)
            throw new InvalidNameAndPathException();

        for (int i = 0; i < fileNames.Count; i++)
        {
            AddApple(fileNames[i], filePaths[i]);
        }
    }
    public void UpdateApple(Apple apple)
    {
        string updatedApple = File.ReadAllText(apple.TrackedFilePath ?? throw new InvalidAppleException(apple));
        apple.TrackedFile = updatedApple;
        JsonManager.OverwriteTree(HeadDir ?? throw new TreeException(this), this);
    }
    public void UpdateApples()
    {
        foreach (Apple apple in Apples)
        {
            Console.WriteLine($"Updated: {apple.TrackedFileName}");
            UpdateApple(apple);
        }
    }

    public void RollBackApple(Apple apple)
    {
        FileManager.WriteTo(apple.TrackedFilePath ?? throw new InvalidAppleException(apple), apple.TrackedFile ?? throw new InvalidAppleException(apple));
    }
    
    public void RollBackApples()
    {
        foreach (Apple apple in Apples)
        {
            Console.WriteLine($"Rolled back {apple.TrackedFileName}");
            RollBackApple(apple);
        }
    }
}
