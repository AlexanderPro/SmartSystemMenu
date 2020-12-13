using System.ComponentModel;

namespace SmartSystemMenu.HotKeys
{
    public enum VirtualKeyModifier : int
    {
        [Description("None")]
        None = 0x00,

        [Description("Shift")]
        Shift = 0x10,

        [Description("Ctrl")]
        Ctrl = 0x11,

        [Description("Alt")]
        Alt = 0x12,

        [Description("WinL")]
        WinL = 0x5B,

        [Description("WinR")]
        WinR = 0x5C
    }
}
