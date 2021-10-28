using System;
using System.Collections.Generic;
using SmartSystemMenu.Extensions;
using SmartSystemMenu.HotKeys;

namespace SmartSystemMenu.Settings
{
    public class MenuItem : ICloneable
    {
        public MenuItemType Type { get; set; }

        public string Name { get; set; }

        public bool Show { get; set; }

        public VirtualKeyModifier Key1 { get; set; }

        public VirtualKeyModifier Key2 { get; set; }

        public VirtualKey Key3 { get; set; }

        public IList<MenuItem> Items { get; set; }

        public MenuItem()
        {
            Type = MenuItemType.Item;
            Name = "";
            Show = true;
            Key1 = VirtualKeyModifier.None;
            Key2 = VirtualKeyModifier.None;
            Key3 = VirtualKey.None;
            Items = new List<MenuItem>();
        }

        public object Clone()
        {
            var menuItemClone = (MenuItem)MemberwiseClone();
            menuItemClone.Items = new List<MenuItem>();
            foreach (var item in Items)
            {
                menuItemClone.Items.Add((MenuItem)item.Clone());
            }
            return menuItemClone;
        }

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