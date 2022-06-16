using System.Runtime.InteropServices;

namespace SmartSystemMenu.Native.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    struct Coord
    {
        public short X;
        public short Y;

        public Coord(short x, short y)
        {
            X = x;
            Y = y;
        }
    }
}
