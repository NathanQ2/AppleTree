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

    public static void WriteTo(string filePath, string fileContents)
    {
        File.Delete(filePath);
        FileStream fs = File.Open(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        StreamWriter sw = new StreamWriter(fs);
        sw.WriteLine(fileContents);
        sw.Close();
        fs.Close();
    }
}
