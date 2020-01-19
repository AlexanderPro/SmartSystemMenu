using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;

namespace SmartSystemMenu.Settings
{
    public class SmartSystemMenuSettings
    {
        public IList<string> ProcessExclusions { get; private set; }

        public MenuItems MenuItems { get; set; }

        public SmartSystemMenuSettings()
        {
            ProcessExclusions = new List<string>();
            MenuItems = new MenuItems();
        }

        public static SmartSystemMenuSettings Read(string fileName)
        {
            var settings = new SmartSystemMenuSettings();
            var document = XDocument.Load(fileName);

            settings.ProcessExclusions = document
                .XPathSelectElements("/smartSystemMenu/processExclusions/processName")
                .Where(x => !string.IsNullOrWhiteSpace(x.Value))
                .Select(x => x.Value.ToLower())
                .ToList();

            settings.MenuItems.StartProgramItems = document
                .XPathSelectElements("/smartSystemMenu/menuItems/startProgramItem/item")
                .Select(x => new StartProgramItem {
                    Title = x.Attribute("title") != null ? x.Attribute("title").Value : "",
                    FileName = x.Attribute("fileName") != null ? x.Attribute("fileName").Value : "",
                    Arguments = x.Attribute("arguments") != null ? x.Attribute("arguments").Value : "",
                })
                .ToList();

            return settings;
        }
    }
}
