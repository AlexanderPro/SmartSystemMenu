using System;
using System.Linq;
using System.ComponentModel;

namespace SmartSystemMenu.Extensions
{
    static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            var attribute = value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
            var description = attribute == null ? null : attribute.Description;
            return description;
        }
    }
}
