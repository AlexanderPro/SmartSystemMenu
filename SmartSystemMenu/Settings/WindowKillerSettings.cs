using System;
using SmartSystemMenu.HotKeys;

namespace SmartSystemMenu.Settings
{
    public class WindowKillerSettings : ICloneable
    {
        public WindowKillerType Type { get; set; }

        public VirtualKeyModifier Key1 { get; set; }

        public VirtualKeyModifier Key2 { get; set; }

        public MouseButton MouseButton { get; set; }

        public WindowKillerSettings()
        {
            Type = WindowKillerType.CloseWindow;
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
