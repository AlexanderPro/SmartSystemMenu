using SmartSystemMenu.Native.Enums;

namespace SmartSystemMenu.Extensions
{
    static class PriorityClassExtensions
    {
        public static Priority GetPriority(this PriorityClass priorityClass)
        {
            switch (priorityClass)
            {
                case PriorityClass.REALTIME_PRIORITY_CLASS: return Priority.RealTime;
                case PriorityClass.HIGH_PRIORITY_CLASS: return Priority.High;
                case PriorityClass.ABOVE_NORMAL_PRIORITY_CLASS: return Priority.AboveNormal;
                case PriorityClass.NORMAL_PRIORITY_CLASS: return Priority.Normal;
                case PriorityClass.BELOW_NORMAL_PRIORITY_CLASS: return Priority.BelowNormal;
                case PriorityClass.IDLE_PRIORITY_CLASS: return Priority.Idle;
                default: return Priority.Normal;
            }
        }
    }
}
