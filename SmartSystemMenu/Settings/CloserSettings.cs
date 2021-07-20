using System;
using SmartSystemMenu.HotKeys;

namespace SmartSystemMenu.Settings
{
    public class CloserSettings : ICloneable
    {
        public WindowCloserType Type { get; set; }

        public VirtualKeyModifier Key1 { get; set; }

        public VirtualKeyModifier Key2 { get; set; }

        public MouseButton MouseButton { get; set; }

        public CloserSettings()
        {
            Type = WindowCloserType.CloseForegroundWindow;
            Key1 = VirtualKeyModifier.None;
            Key2 = VirtualKeyModifier.None;
            MouseButton = MouseButton.None;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
