using System;

namespace SmartSystemMenu.Settings
{
    public class SaveSelectedItemsSettings : ICloneable
    {
        public bool AeroGlass { get; set; }

        public bool AlwaysOnTop { get; set; }

        public bool Alignment { get; set; }

        public bool Transparency { get; set; }

        public bool Priority { get; set; }

        public bool MinimizeToTrayAlways { get; set; }

        public SaveSelectedItemsSettings()
        {
            AeroGlass = true;
            AlwaysOnTop = true;
            Alignment = true;
            Transparency = true;
            Priority = true;
            MinimizeToTrayAlways = true;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
