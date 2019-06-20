using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartSystemMenu.Extensions
{
    static class PriorityExtensions
    {
        public static int GetMenuItemId(this Priority priority)
        {
            switch (priority)
            {
                case Priority.RealTime: return (int)SystemMenu.SC_PRIORITY_REAL_TIME;
                case Priority.High: return (int)SystemMenu.SC_PRIORITY_HIGH;
                case Priority.AboveNormal: return (int)SystemMenu.SC_PRIORITY_ABOVE_NORMAL;
                case Priority.Normal: return (int)SystemMenu.SC_PRIORITY_NORMAL;
                case Priority.BelowNormal: return (int)SystemMenu.SC_PRIORITY_BELOW_NORMAL;
                case Priority.Idle: return (int)SystemMenu.SC_PRIORITY_IDLE;
                default: return (int)SystemMenu.SC_PRIORITY_NORMAL;
            }
        }

        public static PriorityClass GetPriorityClass(this Priority priority)
        {
            switch (priority)
            {
                case Priority.RealTime: return PriorityClass.REALTIME_PRIORITY_CLASS;
                case Priority.High: return PriorityClass.HIGH_PRIORITY_CLASS;
                case Priority.AboveNormal: return PriorityClass.ABOVE_NORMAL_PRIORITY_CLASS;
                case Priority.Normal: return PriorityClass.NORMAL_PRIORITY_CLASS;
                case Priority.BelowNormal: return PriorityClass.BELOW_NORMAL_PRIORITY_CLASS;
                case Priority.Idle: return PriorityClass.IDLE_PRIORITY_CLASS;
                default: return PriorityClass.NORMAL_PRIORITY_CLASS;
            }
        }
    }
}
