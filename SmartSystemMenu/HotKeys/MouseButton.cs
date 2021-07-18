using System.ComponentModel;

namespace SmartSystemMenu.HotKeys
{
    public enum MouseButton : int
    {
        [Description("None")]
        None = 0x00,

        [Description("Left")]
        Left = 0x01,

        [Description("Right")]
        Right = 0x02,

        [Description("Middle")]
        Middle = 0x03
    }
}
