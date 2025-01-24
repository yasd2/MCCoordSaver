using static Rage.Native.NativeFunction;

namespace MCCoordSaver.Utils;

internal class NativesMC
{
    internal static void DISPLAY_​ONSCREEN_​KEYBOARD(int p0,
        string windowTitle,
        string p2,
        string defaultText,
        string defaultConcat1,
        string defaultConcat2,
        string defaultConcat3,
        int maxInputLength
        )
        => Natives.DISPLAY_​ONSCREEN_​KEYBOARD(p0, windowTitle, p2, defaultText, defaultConcat1, defaultConcat2, defaultConcat3, maxInputLength);


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


    internal static string GET_ONSCREEN_KEYBOARD_RESULT()
        => Natives.GET_ONSCREEN_KEYBOARD_RESULT<string>();


    internal static void FORCE_CLOSE_TEXT_INPUT_BOX()
        => Natives.FORCE_CLOSE_TEXT_INPUT_BOX();
}

