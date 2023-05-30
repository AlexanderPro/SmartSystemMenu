using System;

namespace SmartSystemMenu.Extensions
{
    static class WindowAlignmentExtensions
    {
        public static int GetMenuItemId(this WindowAlignment alignment) => alignment switch
        {
            WindowAlignment.TopLeft => MenuItemId.SC_ALIGN_TOP_LEFT,
            WindowAlignment.TopCenter => MenuItemId.SC_ALIGN_TOP_CENTER,
            WindowAlignment.TopRight => MenuItemId.SC_ALIGN_TOP_RIGHT,
            WindowAlignment.MiddleLeft => MenuItemId.SC_ALIGN_MIDDLE_LEFT,
            WindowAlignment.MiddleCenter => MenuItemId.SC_ALIGN_MIDDLE_CENTER,
            WindowAlignment.MiddleRight => MenuItemId.SC_ALIGN_MIDDLE_RIGHT,
            WindowAlignment.BottomLeft => MenuItemId.SC_ALIGN_BOTTOM_LEFT,
            WindowAlignment.BottomCenter => MenuItemId.SC_ALIGN_BOTTOM_CENTER,
            WindowAlignment.BottomRight => MenuItemId.SC_ALIGN_BOTTOM_RIGHT,
            WindowAlignment.CenterHorizontally => MenuItemId.SC_ALIGN_CENTER_HORIZONTALLY,
            WindowAlignment.CenterVertically => MenuItemId.SC_ALIGN_CENTER_VERTICALLY,
            _ => throw new ArgumentException(nameof(alignment))
        };
    }
}
