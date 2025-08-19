using System;

namespace SmartSystemMenu.Settings
{
    public class SizerSettings : ICloneable
    {
        public WindowSizerType SizerType { get; set; }

        public bool ResizableByDefault { get; set; }

        public SizerSettings()
        {
            SizerType = WindowSizerType.WindowWithMargins;
            ResizableByDefault = false;
        }

        public object Clone() => MemberwiseClone();
    }
}
