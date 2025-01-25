using static Rage.Native.NativeFunction;

internal class NativesMC
{
    /// <summary>
    /// Opens the text box to put text in
    /// </summary>
    internal static void DISPLAY_​ONSCREEN_​KEYBOARD(int p0 = 0,
        string windowTitle = "FMMC_KEY_TIP8",
        string p2 = "",
        string defaultText = "",
        string defaultConcat1 = "",
        string defaultConcat2 = "",
        string defaultConcat3 = "",
        int maxInputLength = 40
        )
        => Natives.DISPLAY_​ONSCREEN_​KEYBOARD(p0, windowTitle, p2, defaultText, 
            defaultConcat1, defaultConcat2, defaultConcat3, maxInputLength);


    /// <summary>
    /// Returns the current status of the text box
    /// </summary>
    internal static KeyboardStatus UPDATE_ONSCREEN_KEYBOARD()
    {
        KeyboardStatus result = (KeyboardStatus)Natives.UPDATE_ONSCREEN_KEYBOARD<int>();
        return result;
    }


    internal enum KeyboardStatus
    {
        Inactive = -1,
        Editing = 0,
        Finished = 1,
        Canceled = 2
    }


    /// <summary>
    /// Called after keyboard status returns 'Finished'
    /// </summary>
    /// <returns>returns the input string from the text box</returns>
    internal static string GET_ONSCREEN_KEYBOARD_RESULT()
        => Natives.GET_ONSCREEN_KEYBOARD_RESULT<string>();


    /// <summary>
    /// Better native to close the text box
    /// </summary>
    internal static void FORCE_CLOSE_TEXT_INPUT_BOX()
        => Natives.FORCE_CLOSE_TEXT_INPUT_BOX();
}

