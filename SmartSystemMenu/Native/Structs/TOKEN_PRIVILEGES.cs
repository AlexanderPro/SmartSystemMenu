using System;
using System.Runtime.InteropServices;

namespace SmartSystemMenu.Native.Structs
{
    struct TOKEN_PRIVILEGES
    {
        public uint PrivilegeCount;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        public LUID_AND_ATTRIBUTES[] Privileges;
    }
}
