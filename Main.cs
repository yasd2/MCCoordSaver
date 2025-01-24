using MCCoordSaver.Utils;
using System.Reflection;

[assembly: Rage.Attributes.Plugin("MC Coord Saver",
    Description = "Save Vector4 Coordinates for developers!",
    Author = "Yasd",
    SupportUrl = "https://discord.gg/utNranQJSr",
    PrefersSingleInstance = true)]

namespace MCCoordSaver;

public class EntryPoint
{
    internal static void Main()
    {
        Helper.Log(" " + Assembly.GetExecutingAssembly().GetName().Version + " is loaded.");
        Notifications.Short("~g~MC Coord Saver~s~ " + Assembly.GetExecutingAssembly().GetName().Version + " is loaded.");

        Config.ReadINI();

        CoordManager.Initialize();
    }



    internal static void OnUnload(bool isTerminating)
    {
        Helper.Log("successfully unloaded");
    }
}

