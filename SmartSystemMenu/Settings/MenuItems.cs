using System.Collections.Generic;

namespace SmartSystemMenu.Settings
{
    public class MenuItems
    {
        public IList<StartProgramItem> StartProgramItems { get; set; }

        public MenuItems()
        {
            StartProgramItems = new List<StartProgramItem>();
        }
    }
}
