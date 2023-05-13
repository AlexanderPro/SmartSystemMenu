using System;
using SmartSystemMenu.Extensions;
using SmartSystemMenu.HotKeys;

namespace SmartSystemMenu.Settings
{
    public class WindowSizeMenuItem : ICloneable
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int? Left { get; set; }
        
        public int? Top { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public VirtualKeyModifier Key1 { get; set; }

        public VirtualKeyModifier Key2 { get; set; }

        public VirtualKey Key3 { get; set; }

        public WindowSizeMenuItem()
        {
            Id = 0;
            Title = "";
            Left = null;
            Top = null;
            Width = 0;
            Height = 0;
            Key1 = VirtualKeyModifier.None;
            Key2 = VirtualKeyModifier.None;
            Key3 = VirtualKey.None;
        }

        public object Clone() => MemberwiseClone();

        public override string ToString()
        {
            var combination = "";

            if (Key1 != VirtualKeyModifier.None)
            {
                combination = Key1.GetDescription();
            }

            if (Key2 != VirtualKeyModifier.None)
            {
                combination += string.IsNullOrEmpty(combination) ? Key2.GetDescription() : "+" + Key2.GetDescription();
            }

            if (Key3 != VirtualKey.None)
            {
                combination += string.IsNullOrEmpty(combination) ? Key3.GetDescription() : "+" + Key3.GetDescription();
            }
            else
            {
                combination = "";
            }

            return combination;
        }
    }
}
