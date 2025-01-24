using MCCoordSaver.Utils;
using Rage;
using System;
using System.Windows.Forms;

namespace MCCoordSaver;

internal class Config
{
    public static Keys SaveKey = Keys.F12;

    public static string FilePath = @"plugins\MCCoordSaver\";

    public static Boolean UseMenu = true;


    internal static void ReadINI()
    {
        InitializationFile ini = new InitializationFile(@"plugins/MCCoordSaver.ini");

        if (!ini.Exists())
        {
            Helper.Log("ERROR, .ini doesn't exist at plugins/MCCoordSaver.ini!");
            return;
        }

        SaveKey = ini.ReadEnum("Keys", "SaveKey", SaveKey);

        FilePath = ini.ReadString("General", "FilePath", FilePath);

        UseMenu = ini.ReadBoolean("General", "UseMenu", UseMenu);
    }
}

