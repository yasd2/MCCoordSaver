using Rage;
using System.Reflection;

[assembly: Rage.Attributes.Plugin(
    "Position Tracker", Description = "Save Vector Coordinates for developers!",
    Author = "Yasd", 
    SupportUrl = "https://discord.gg/utNranQJSr", 
    PrefersSingleInstance = true)]

public class EntryPoint
{
    // Gets automatically loaded on plugin load
    internal static void Main()
    {
        Game.DisplayNotification("~HC_9~~h~PositionTracker~h~~s~ " +
            Assembly.GetExecutingAssembly().GetName().Version +
            " ~g~is loaded.");

        Game.LogTrivial(Assembly.GetExecutingAssembly().GetName().Version +
            " is loaded.");

        Config.ReadINI();

        PositionTracker.Initialize();
    }


    // Gets called on plugin unload and aborts all running gamefibers
    internal static void OnUnload(bool isTerminating)
    {
        Game.DisplayNotification("~HC_9~~h~PositionTracker~h~~s~ " +
            Assembly.GetExecutingAssembly().GetName().Version +
            " ~r~is unloaded.");

        Game.LogTrivial(Assembly.GetExecutingAssembly().GetName().Version + 
            " is unloaded.");
    }
}

