using Rage;
using System;

namespace MCCoordSaver.Utils;

internal class Notifications
{
    internal static void Short(String text)
        => Game.DisplayNotification(text);
}

