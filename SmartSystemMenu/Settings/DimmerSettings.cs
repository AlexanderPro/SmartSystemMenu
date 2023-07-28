using System;

namespace SmartSystemMenu.Settings
{
    public class DimmerSettings : ICloneable
    {
        public string Color { get; set; }

        public int Transparency { get; set; }

        public DimmerSettings()
        {
            Color = string.Empty;
            Transparency = 0;
        }

        public object Clone() => MemberwiseClone();
    }
}
