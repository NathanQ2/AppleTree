namespace AppleTree.ProgramData.AppleManagement;

public class Apple
{
    public string? TrackedFilePath { get; init; }
    public string? TrackedFileName { get; init; } //relative path

    public bool IsLocalOnly { get; set; } = true;
    public bool IsLocal { get; set; } = true;
    
    public string? TrackedFile { get; set; }
    
    public string? TrackedFileOld { get; set; }
}
