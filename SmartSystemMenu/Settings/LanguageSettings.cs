using System.Collections.Generic;
using System.Linq;

namespace SmartSystemMenu.Settings
{
    public class LanguageSettings
    {
        public IList<LanguageItem> Items { get; set; }

        public LanguageSettings()
        {
            Items = new List<LanguageItem>();
        }

        public string GetValue(string name)
        {
            var item = Items.FirstOrDefault(x => x.Name == name);
            var value = item == null ? "" : item.Value;
            return value;
        }
    }
}
