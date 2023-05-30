using SmartSystemMenu.Native.Enums;

namespace SmartSystemMenu.Extensions
{
    static class PriorityExtensions
    {
        public static int GetMenuItemId(this Priority priority) => priority switch
        {
            Priority.RealTime => MenuItemId.SC_PRIORITY_REAL_TIME,
            Priority.High => MenuItemId.SC_PRIORITY_HIGH,
            Priority.AboveNormal => MenuItemId.SC_PRIORITY_ABOVE_NORMAL,
            Priority.Normal => MenuItemId.SC_PRIORITY_NORMAL,
            Priority.BelowNormal => MenuItemId.SC_PRIORITY_BELOW_NORMAL,
            Priority.Idle => MenuItemId.SC_PRIORITY_IDLE,
            _ => MenuItemId.SC_PRIORITY_NORMAL
        };

        public static PriorityClass GetPriorityClass(this Priority priority) => priority switch
        {
            Priority.RealTime => PriorityClass.REALTIME_PRIORITY_CLASS,
            Priority.High => PriorityClass.HIGH_PRIORITY_CLASS,
            Priority.AboveNormal => PriorityClass.ABOVE_NORMAL_PRIORITY_CLASS,
            Priority.Normal => PriorityClass.NORMAL_PRIORITY_CLASS,
            Priority.BelowNormal => PriorityClass.BELOW_NORMAL_PRIORITY_CLASS,
            Priority.Idle => PriorityClass.IDLE_PRIORITY_CLASS,
            _ => PriorityClass.NORMAL_PRIORITY_CLASS
        };
    }
}
