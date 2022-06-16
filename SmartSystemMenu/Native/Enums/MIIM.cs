using System;

namespace SmartSystemMenu.Native.Enums
{
    [Flags]
    enum MIIM
    {
        BITMAP = 0x00000080,
        CHECKMARKS = 0x00000008,
        DATA = 0x00000020,
        FTYPE = 0x00000100,
        ID = 0x00000002,
        STATE = 0x00000001,
        STRING = 0x00000040,
        SUBMENU = 0x00000004,
        TYPE = 0x00000010
    }
}
