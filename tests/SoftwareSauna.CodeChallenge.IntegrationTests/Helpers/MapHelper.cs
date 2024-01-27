namespace SoftwareSauna.CodeChallenge.IntegrationTests.Helpers;
internal static class MapHelper
{
    public static string ReadMap(string fileName) =>
        File.ReadAllText(
            Path.Combine(
                "Maps", 
                fileName.EndsWith(".txt") ? fileName : fileName + ".txt"));
}
