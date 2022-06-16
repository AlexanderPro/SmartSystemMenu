using System;

namespace SmartSystemMenu.Native.Enums
{
    [Flags]
    enum LayeredWindow : uint
    {
        LWA_COLORKEY = 0x00000001,
        LWA_ALPHA = 0x00000002
    }
}
