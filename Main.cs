using Rage;
using System.Reflection;

[assembly: Rage.Attributes.Plugin(
    "MC Position Tracker", Description = "Save Vector4 Coordinates for developers!",
    Author = "Yasd", 
    SupportUrl = "https://discord.gg/utNranQJSr", 
    PrefersSingleInstance = true)]

public class EntryPoint
{
    internal static void Main()
    {
        Game.DisplayNotification("~HC_9~~h~MC PositionTracker~h~~s~ " +
            Assembly.GetExecutingAssembly().GetName().Version +
            " ~g~is loaded.");

        Game.LogTrivial(Assembly.GetExecutingAssembly().GetName().Version +
            " is loaded.");

        Config.ReadINI();

        PositionTracker.Initialize();
    }


    internal static void OnUnload(bool isTerminating)
        => Game.LogTrivial(Assembly.GetExecutingAssembly().GetName().ToString() + " is unloaded.");
}

