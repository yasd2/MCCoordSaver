using Rage;
using System;
using System.IO;
using System.Windows.Forms;

internal class Config
{
    public static Keys SaveKey = Keys.F12;

    public static string FilePath = @"plugins/MCPositionTracker/";

    public static Boolean UseMenu = true;

    public static Boolean UseVector4 = true;

    public static String XParameter = "X";
    public static String YParameter = "Y";
    public static String ZParameter = "Z";
    public static String WParameter = "W";
    public static String TimeParameter = "time";
    public static String NameParameter = "name";

    public static String Style = "new Vector4(new Vector3(Xf, Yf, Zf,), Wf) | time | name";


    internal static void ReadINI()
    {
        if (!Directory.Exists(FilePath))
        {
            Game.LogTrivial($"'{FilePath}' doesn't exist, creating directory.");

            Directory.CreateDirectory(FilePath);
        }

        InitializationFile ini = new InitializationFile(@"plugins/MCPositionTracker.ini");

        if (!ini.Exists())
        {
            Game.LogTrivial("'plugins/MCPositionTracker.ini' doesn't exist, creating a new one.");

            ini.Create();

            File.WriteAllText(ini.FileName,
@"[Keys]
; The key to save the coordinate, default: F12
SaveKey = F12

[General]
; here the coords-file is located, default: plugins\MCPositionTracker\
FilePath = plugins\MCPositionTracker\

; UseMenu = true -> means you have a RNUI menu, is default
; UseMenu = false -> skips the menu and directly jumps to the text box
UseMenu = true

[CustomStyle]
; The sign to replace the coordinate with, case sensitive
XParameter = X
YParameter = Y
ZParameter = Z
WParameter = W
TimeParameter = time
NameParameter = name

; save style, default: new Vector4(new Vector3(Xf, Yf, Zf,), Wf) | time | name
; Example: Style = time | new Location(Xf, Yf, Zf, Wf) | name
; it would be saved as f.e.: 25-01-25 23:34:14 | new Location(12.3456f, 98.7654f, 12.3456f, 98.7654f) | MyPosition
; if you use X,Y,Z,W,time,name in it, change them above, otherwise those are replaced with the coords!
; if you would use: Style = new Warehouse(X, Y, Z), W 
; -> it would result in new 12.3456arehouse(12.3456, 98.76564, 12.3456), 98.7654 -> W is replaced twice
Style = new Vector4(new Vector3(Xf, Yf, Zf,), Wf) | time | name");
        }

        SaveKey = ini.ReadEnum("Keys", "SaveKey", SaveKey);

        FilePath = ini.ReadString("General", "FilePath", FilePath);

        UseMenu = ini.ReadBoolean("General", "UseMenu", UseMenu);

        XParameter = ini.ReadString("CustomStyle", "XParameter", XParameter);
        YParameter = ini.ReadString("CustomStyle", "YParameter", YParameter);
        ZParameter = ini.ReadString("CustomStyle", "ZParameter", ZParameter);
        WParameter = ini.ReadString("CustomStyle", "WParameter", WParameter);
        TimeParameter = ini.ReadString("CustomStyle", "TimeParameter", TimeParameter);
        NameParameter = ini.ReadString("CustomStyle", "NameParameter", NameParameter);

        Style = ini.ReadString("CustomStyle", "Style", Style);
    }
}

