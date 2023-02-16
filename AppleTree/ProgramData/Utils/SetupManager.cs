namespace AppleTree.ProgramData.Utils;

public static class SetupManager
{
    public static void Setup()
    {
        Setup(new Settings());
    }

    public static void Setup(Settings? settings)
    {
        JsonManager.OverwriteSettings(settings);
    }
}
