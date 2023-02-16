using AppleTree.ProgramData.AppleManagement;
using AppleTree.ProgramData.Utils;

namespace AppleTree.ProgramData.TreeManagement;

public class Tree
{
    public string? Name { get; set; }
    public string? HeadDir { get; set; }
    public List<Apple> Apples { get; private set; } = new List<Apple>();

    public void NewApple(Apple apple, string parentPath)
    {
        Apples.Add(apple);
        JsonManager.NewApple(apple, parentPath); // this func will write all required info files
    }

    public void NewApple(string name, string parentPath)
    {
        Apples.Add(new Apple {TrackedFileName = name});
        JsonManager.NewApple(Apples[^1], parentPath);
    }
}
