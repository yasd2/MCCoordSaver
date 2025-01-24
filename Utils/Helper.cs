using Rage;

namespace MCCoordSaver.Utils;

internal class Helper
{
    internal static void Log(string text)
        => Game.LogTrivial("MCCoordSaver: " + text);
}
