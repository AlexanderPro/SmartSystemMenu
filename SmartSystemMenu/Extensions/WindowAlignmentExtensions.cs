using System;

namespace SmartSystemMenu.Extensions
{
    static class WindowAlignmentExtensions
    {
        public static int GetMenuItemId(this WindowAlignment alignment)
        {
            switch (alignment)
            {
                case WindowAlignment.TopLeft: return MenuItemId.SC_ALIGN_TOP_LEFT;
                case WindowAlignment.TopCenter: return MenuItemId.SC_ALIGN_TOP_CENTER;
                case WindowAlignment.TopRight: return MenuItemId.SC_ALIGN_TOP_RIGHT;
                case WindowAlignment.MiddleLeft: return MenuItemId.SC_ALIGN_MIDDLE_LEFT;
                case WindowAlignment.MiddleCenter: return MenuItemId.SC_ALIGN_MIDDLE_CENTER;
                case WindowAlignment.MiddleRight: return MenuItemId.SC_ALIGN_MIDDLE_RIGHT;
                case WindowAlignment.BottomLeft: return MenuItemId.SC_ALIGN_BOTTOM_LEFT;
                case WindowAlignment.BottomCenter: return MenuItemId.SC_ALIGN_BOTTOM_CENTER;
                case WindowAlignment.BottomRight: return MenuItemId.SC_ALIGN_BOTTOM_RIGHT;
                case WindowAlignment.CenterHorizontally: return MenuItemId.SC_ALIGN_CENTER_HORIZONTALLY;
                case WindowAlignment.CenterVertically: return MenuItemId.SC_ALIGN_CENTER_VERTICALLY;
                default: throw new ArgumentException(nameof(alignment));
            }
        }
    }
}
