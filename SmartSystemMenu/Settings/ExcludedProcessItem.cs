using System;

namespace SmartSystemMenu.Settings
{
    public class ExcludedProcessItem : ICloneable
    {
        public string Name { get; set; }

        public bool IgnoreHook { get; set; }

        public ExcludedProcessItem()
        {
            Name = string.Empty;
            IgnoreHook = false;
        }

        public object Clone() => MemberwiseClone();
    }
}
