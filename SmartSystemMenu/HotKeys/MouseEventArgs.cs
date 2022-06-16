using System;
using SmartSystemMenu.Native.Structs;

namespace SmartSystemMenu.HotKeys
{
    class MouseEventArgs : EventArgs
    {
        public Point Point { get; private set; }

        public MouseEventArgs(Point point)
        {
            Point = point;
        }
    }
}
