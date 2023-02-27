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

    public static bool IsFile(string path)
    {
        if (path[^1] == '.')
        {
            return false;
        }
        try
        {
            string[] subFolders = Directory.GetDirectories(path);
            Console.WriteLine(subFolders);

            return false;
        }
        catch (IOException e)
        {
            return true;
        }
    }

    public static string[] GetAllFilesIn(string dirPath)
    {
        List<string> files = new List<string>();

        try
        {
            foreach (string file in Directory.GetFiles(dirPath)) // find all files in this dir
            {
                files.Add(file);
            }
        }
        catch (DirectoryNotFoundException)
        {
            return files.ToArray();
        }
        
        foreach (string subDir in Directory.GetDirectories(dirPath)) // go to next subDir and find all files there
        {
            foreach (string file in GetAllFilesIn(subDir))
            {
                files.Add(file);
            }
        }

        return files.ToArray();
    }

    public static string GetRelativePath(string mainPath, string relativeStart)
    {
        string[] folders = mainPath.Split('/');
        string[] relativeFolders = relativeStart.Split('/');
        string finishedPath = "";

        for (int i = 0; i < folders.Length; i++)
        {
            string folder = folders[i];
            string relativeFolder = "";

            try
            {
                relativeFolder = relativeFolders[i];
            }
            catch (IndexOutOfRangeException)
            {
                
            }

            if (folder != relativeFolder)
            {
                finishedPath += $"/{folder}";
            }
        }

        return finishedPath;
    }
}
