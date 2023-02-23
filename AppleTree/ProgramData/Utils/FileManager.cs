namespace AppleTree.ProgramData.Utils;

public static class FileManager
{
    /// <summary>
    /// Gets file name of file
    /// </summary>
    /// <param name="filePath">Path of file</param>
    /// <returns>File name</returns>
    public static string GetFileName(string filePath)
    {
        string[] folders = filePath.Split('/');
        Console.WriteLine(folders[^1]);
        return folders[^1];
    }
}
