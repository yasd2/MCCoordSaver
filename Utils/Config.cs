using Rage;
using System;
using System.Windows.Forms;

internal class Config
{
    public static Keys SaveKey = Keys.F12;

    public static string FilePath = @"plugins/MCPositionTracker/";

    public static Boolean UseMenu = true;


    internal static void ReadINI()
    {
        InitializationFile ini = new InitializationFile(@"plugins/MCPositionTracker.ini");

        if (!ini.Exists())
        {
            Game.LogTrivial("[MC PositionTracker] ERROR: 'plugins/MCPositionTracker.ini' doesn't exist!");
            return;
        }

        SaveKey = ini.ReadEnum("Keys", "SaveKey", SaveKey);

        FilePath = ini.ReadString("General", "FilePath", FilePath);

        UseMenu = ini.ReadBoolean("General", "UseMenu", UseMenu);
    }
}

