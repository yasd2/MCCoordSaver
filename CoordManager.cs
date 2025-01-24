using Rage;
using RAGENativeUI;
using RAGENativeUI.Elements;
using System;
using System.Drawing;
using System.IO;

namespace MCCoordSaver.Utils;

internal class CoordManager
{
    private static MenuPool MenuPool { get; set; }
    private static UIMenu MainMenu { get; set; }
    private static Vector4 CurrentPlayerCoords { get; set; }
    private static Ped Player => Game.LocalPlayer.Character;
    private static bool IsKeyboardActive { get; set; }

    private static string FilePath = Config.FilePath;

    /// <summary>
    /// Initializes the coordinate manager and its processes.
    /// </summary>
    internal static void Initialize()
    {
        CreateMainMenu();
        StartProcessing();
    }

    /// <summary>
    /// Creates and configures the main menu.
    /// </summary>
    private static void CreateMainMenu()
    {
        MenuPool = new MenuPool();
        MainMenu = new UIMenu("MC Coord Saver", "Save the players coordinates");

        var savePositionItem = new UIMenuItem("Save Current Position", "Saves the player's current coordinates to a file");
        savePositionItem.Activated += (_, _) => StartCoordSaveProcess();

        MainMenu.MouseControlsEnabled = false;
        MainMenu.AllowCameraMovement = true;
        MainMenu.SetBannerType(Color.Black);

        MainMenu.TitleStyle = MainMenu.TitleStyle with
        {
            Font = TextFont.Monospace,
            Color = Color.LightSeaGreen,
            DropShadow = true,
        };

        MainMenu.DescriptionSeparatorColor = Color.LightSeaGreen;
        MainMenu.AddItem(savePositionItem);

        MenuPool.Add(MainMenu);
    }

    /// <summary>
    /// Handles background processing for menu and input.
    /// </summary>
    private static void StartProcessing()
    {
        // Menu processing
        GameFiber.StartNew(() =>
        {
            while (Config.UseMenu)
            {
                GameFiber.Yield();
                MenuPool.ProcessMenus();

                if (Game.IsKeyDown(Config.SaveKey))
                {
                    ToggleMainMenuVisibility();
                }
            }
        }, "MenuProcessor");

        // Coordinate saving without menu
        GameFiber.StartNew(() =>
        {
            while (!Config.UseMenu)
            {
                GameFiber.Yield();

                if (Game.IsKeyDown(Config.SaveKey))
                {
                    OpenKeyboardForInput();
                }
            }
        }, "KeyboardProcessor");

        // Monitor keyboard status
        GameFiber.StartNew(() =>
        {
            while (true)
            {
                GameFiber.Yield();
                CheckKeyboardInputStatus();
            }
        }, "KeyboardStatusChecker");
    }

    /// <summary>
    /// Opens the on-screen keyboard for input.
    /// </summary>
    private static void OpenKeyboardForInput()
    {
        IsKeyboardActive = true;
        CurrentPlayerCoords = new Vector4(Player.Position, Player.Heading);

        NativesMC.DISPLAY_ONSCREEN_KEYBOARD(0, "FMMC_KEY_TIP8", "", "", "", "", "", 40);
    }

    /// <summary>
    /// Checks the status of the on-screen keyboard and saves the input when finished.
    /// </summary>
    private static void CheckKeyboardInputStatus()
    {
        switch (NativesMC.UPDATE_ONSCREEN_KEYBOARD())
        {
            case NativesMC.KeyboardStatus.Finished:
                SaveCoordinatesToFile();
                break;

            case NativesMC.KeyboardStatus.Canceled:
            case NativesMC.KeyboardStatus.Inactive:
            case NativesMC.KeyboardStatus.Editing:
                break;
        }
    }

    /// <summary>
    /// Saves the current coordinates to a file.
    /// </summary>
    private static void SaveCoordinatesToFile()
    {
        if (!IsKeyboardActive) return;

        IsKeyboardActive = false;
        string inputResult = NativesMC.GET_ONSCREEN_KEYBOARD_RESULT();

        FilePath = @$"{FilePath}/Coords_{DateTime.Now:yy-MM-dd}.txt";

        if (!File.Exists(FilePath))
        {
            File.WriteAllText(FilePath, string.Empty);
        }

        using (var writer = new StreamWriter(FilePath, true))
        {
            string timestamp = DateTime.Now.ToString("dd-MM-yy HH:mm:ss");
            writer.WriteLine($"new Vector4(new Vector3({CurrentPlayerCoords.X}f, {CurrentPlayerCoords.Y}f, {CurrentPlayerCoords.Z}f), {CurrentPlayerCoords.W}f) | {timestamp} | {inputResult}");
        }
    }

    /// <summary>
    /// Starts the coordinate saving process via the menu.
    /// </summary>
    private static void StartCoordSaveProcess()
    {
        MainMenu.Visible = false;
        OpenKeyboardForInput();
    }

    /// <summary>
    /// Toggles the visibility of the main menu.
    /// </summary>
    private static void ToggleMainMenuVisibility()
    {
        MainMenu.Visible = !MainMenu.Visible;
    }
}
