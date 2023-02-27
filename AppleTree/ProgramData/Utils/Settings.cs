using System.Text.Json.Serialization;
using AppleTree.ProgramData.TreeManagement;

namespace AppleTree.ProgramData.Utils;


public class Settings
{
    public bool Debug { get; set; }
    /*[JsonInclude]
    public List<Tree> Trees { get; set; } = new List<Tree>();*/
}
