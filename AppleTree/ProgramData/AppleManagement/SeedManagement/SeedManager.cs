namespace AppleTree.ProgramData.AppleManagement.SeedManagement;

public class SeedManager // a seed = a file (Ex: helloWorld.py -> a seed)
{
    public string? LocalDir { get; set; }
    public List<string>? TrackedFile { get; set; }
}
