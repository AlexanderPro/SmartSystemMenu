using SmartSystemMenu.Extensions;
using SmartSystemMenu.HotKeys;

namespace SmartSystemMenu.Settings
{
    public class MenuItem
    {
        public string Name { get; set; }

        public bool HotKeyEnabled { get; set; }

        public VirtualKeyModifier Key1 { get; set; }

        public VirtualKeyModifier Key2 { get; set; }

        public VirtualKey Key3 { get; set; }

        public override string ToString()
        {
            var combination = "";

            if (!HotKeyEnabled)
            {
                return combination;
            }

            if (Key1 != VirtualKeyModifier.None)
            {
                combination = Key1.GetDescription();
            }

            if (Key2 != VirtualKeyModifier.None)
            {
                combination += string.IsNullOrEmpty(combination) ? Key2.GetDescription() : "+" + Key2.GetDescription();
            }

            combination += string.IsNullOrEmpty(combination) ? Key3.GetDescription() : "+" + Key3.GetDescription();

            return combination;
        }
    }
}