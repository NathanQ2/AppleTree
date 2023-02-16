namespace AppleTree.ProgramData.AppleManagement;

public class Apple
{
    public string? TrackedFilePath { get; set; }
    public string? TrackedFileName { get; set; }

    public bool IsLocalOnly { get; set; } = true;
    public bool IsLocal { get; set; } = true;
}
