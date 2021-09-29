using System.Collections.Generic;
using System.Linq;

namespace SmartSystemMenu.Settings
{
    public class MenuItems
    {
        public IList<WindowSizeMenuItem> WindowSizeItems { get; set; }

        public IList<StartProgramMenuItem> StartProgramItems { get; set; }

        public IList<MenuItem> Items { get; set; }

        public MenuItems()
        {
            WindowSizeItems = new List<WindowSizeMenuItem>();
            StartProgramItems = new List<StartProgramMenuItem>();
            Items = new List<MenuItem>();
        }

        public string GetHotKeysCombination(string name)
        {
            var item = Items.FirstOrDefault(x => x.Name == name);
            var value = item == null ? "" : item.ToString();
            return value;
        }

        public string GetHotKeysCombination(int id)
        {
            var item = WindowSizeItems.FirstOrDefault(x => x.Id == id);
            var value = item == null ? "" : item.ToString();
            return value;
        }
    }
}
