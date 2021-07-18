using System;
using SmartSystemMenu.Native;

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
