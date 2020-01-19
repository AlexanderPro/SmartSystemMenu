using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;

namespace SmartSystemMenu.Settings
{
    public class SmartSystemMenuSettings
    {
        public IList<string> ProcessExclusions { get; private set; }

        public SmartSystemMenuSettings()
        {
            ProcessExclusions = new List<string>();
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
            return settings;
        }
    }
}
