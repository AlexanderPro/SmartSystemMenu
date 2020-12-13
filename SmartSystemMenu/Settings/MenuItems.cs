using System.Collections.Generic;
using System.Linq;

namespace SmartSystemMenu.Settings
{
    public class MenuItems
    {
        public IList<StartProgramMenuItem> StartProgramItems { get; set; }

        public IList<MenuItem> Items { get; set; }

        public MenuItems()
        {
            StartProgramItems = new List<StartProgramMenuItem>();
            Items = new List<MenuItem>();
        }

        public string GetHotKeysCombination(string name)
        {
            var item = Items.FirstOrDefault(x => x.Name == name);
            var value = item == null ? "" : item.ToString();
            return value;
        }
    }
}
