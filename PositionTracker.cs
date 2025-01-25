using Rage;
using RAGENativeUI;
using RAGENativeUI.Elements;
using System;
using System.Drawing;
using System.IO;

internal class PositionTracker
{
    private static MenuPool MenuPool { get; set; }
    private static UIMenu MainMenu { get; set; }
    private static Vector4 CurrentPlayerCoords { get; set; }
    private static Ped Player => Game.LocalPlayer.Character;
    private static bool IsKeyboardActive { get; set; }


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
        if (!Config.UseMenu) { return; }

        MenuPool = new MenuPool();
        MainMenu = new UIMenu("MC PositionTracker", "Save the players coordinates");

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
        GameFiber.ExecuteNewWhile(
            MenuPool.ProcessMenus, 
            "MenuProcessor", 
            () => Config.UseMenu);


        // Toggle RNUI menu // and open text box
        GameFiber.ExecuteNewWhile(() =>  { 
            if (Game.IsKeyDown(Config.SaveKey)) ToggleMainMenuVisibility(); }, 
            "MenuProcessor", 
            () => Config.UseMenu);


        // Open text box without menu
        GameFiber.ExecuteNewWhile(() =>  { 
            if (Game.IsKeyDown(Config.SaveKey)) OpenKeyboardForInput(); }, 
            "KeyboardProcessor", 
            () => !Config.UseMenu);


        // Permanently checking for Keyboardstatus finished
        GameFiber.ExecuteNewWhile(
            CheckKeyboardInputStatus, 
            "KeyboardStatusChecker", 
            () => true);
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
        if (NativesMC.UPDATE_ONSCREEN_KEYBOARD() == NativesMC.KeyboardStatus.Finished) 
            SaveCoordinatesToFile(); 
    }


    /// <summary>
    /// Saves the current coordinates to a file.
    /// </summary>
    private static void SaveCoordinatesToFile()
    {
        if (!IsKeyboardActive) return;

        IsKeyboardActive = false;
        string inputResult = NativesMC.GET_ONSCREEN_KEYBOARD_RESULT();

        string filePath = @$"{Config.FilePath}/Coords_{DateTime.Now:yy-MM-dd}.txt";

        if (!File.Exists(filePath)) File.WriteAllText(filePath, string.Empty);

        using (var writer = new StreamWriter(filePath, true))
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
        => MainMenu.Visible = !MainMenu.Visible;
}
