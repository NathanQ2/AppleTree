using AppleTree.ProgramData.AppleManagement;
using AppleTree.ProgramData.Utils;
using AppleTree.ProgramData.Utils.Exceptions;

namespace AppleTree.ProgramData.TreeManagement;

public class Tree
{
    public string? Name { get; set; }
    public string? HeadDir { get; set; }
    public List<Apple> Apples { get; private set; } = new List<Apple>();

    public void AddApple(Apple apple, string filePath)
    {
        Apples.Add(apple);
        JsonManager.NewApple(apple, filePath, HeadDir ?? throw new TreePresenceException(this)); // this func will write all required info files
    }

    public void AddApple(string name, string filePath)
    {
        Apples.Add(new Apple {TrackedFileName = name, TrackedFilePath = filePath, TrackedFile = File.ReadAllText(filePath)});
        //JsonManager.NewApple(Apples[^1], filePath, HeadDir ?? throw new TreePresenceException(this));
        JsonManager.OverwriteTree(HeadDir ?? throw new TreeException(this), this);
    }

    public void AddApples(List<string> names, List<string> filePaths)
    {
        if (names.Count != filePaths.Count)
            throw new InvalidNameAndPathException();

        for (int i = 0; i < names.Count; i++)
        {
            AddApple(names[i], filePaths[i]);
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
