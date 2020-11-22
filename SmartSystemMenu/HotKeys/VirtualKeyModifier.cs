using System.ComponentModel;

namespace SmartSystemMenu.HotKeys
{
    public enum VirtualKeyModifier : int
    {
        [Description("")]
        None = 0x00,

        [Description("Shift")]
        Shift = 0x10,

        [Description("Ctrl")]
        Ctrl = 0x11,

        [Description("Alt")]
        Alt = 0x12
    }
}
