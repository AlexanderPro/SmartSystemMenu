using SmartSystemMenu.Native.Enums;

namespace SmartSystemMenu.Extensions
{
    static class PriorityClassExtensions
    {
        public static Priority GetPriority(this PriorityClass priorityClass) => priorityClass switch
        {
            PriorityClass.REALTIME_PRIORITY_CLASS => Priority.RealTime,
            PriorityClass.HIGH_PRIORITY_CLASS => Priority.High,
            PriorityClass.ABOVE_NORMAL_PRIORITY_CLASS => Priority.AboveNormal,
            PriorityClass.NORMAL_PRIORITY_CLASS => Priority.Normal,
            PriorityClass.BELOW_NORMAL_PRIORITY_CLASS => Priority.BelowNormal,
            PriorityClass.IDLE_PRIORITY_CLASS => Priority.Idle,
            _ => Priority.Normal
        };
    }
}
