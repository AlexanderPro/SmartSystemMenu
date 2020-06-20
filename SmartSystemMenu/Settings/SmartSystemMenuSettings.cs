using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Text;

namespace SmartSystemMenu.Settings
{
    public class SmartSystemMenuSettings : ICloneable
    {
        public IList<string> ProcessExclusions { get; private set; }

        public MenuItems MenuItems { get; private set; }

        public bool ShowSystemTrayIcon { get; private set; }

        public SmartSystemMenuSettings()
        {
            ProcessExclusions = new List<string>();
            MenuItems = new MenuItems();
            ShowSystemTrayIcon = true;
        }

        public object Clone()
        {
            var settings = new SmartSystemMenuSettings();

            foreach (var processExclusion in ProcessExclusions)
            {
                settings.ProcessExclusions.Add(processExclusion);
            }

            foreach (var menuItem in MenuItems.StartProgramItems)
            {
                settings.MenuItems.StartProgramItems.Add(new StartProgramItem { Title = menuItem.Title, FileName = menuItem.FileName, Arguments = menuItem.Arguments });
            }

            return settings;
        }

        public override bool Equals(object other)
        {
            if (other == null)
                return false;

            if (object.ReferenceEquals(this, other))
                return true;

            if (this.GetType() != other.GetType())
                return false;

            return this.Equals(other as SmartSystemMenuSettings);
        }

        public bool Equals(SmartSystemMenuSettings other)
        {
            if (other == null)
                return false;

            if (object.ReferenceEquals(this, other))
                return true;

            if (this.GetType() != other.GetType())
                return false;

            if (this.ProcessExclusions.Count != other.ProcessExclusions.Count)
            {
                return false;
            }

            if (this.MenuItems.StartProgramItems.Count != other.MenuItems.StartProgramItems.Count)
            {
                return false;
            }

            for (var i = 0; i < this.ProcessExclusions.Count; i++)
            {
                if (string.Compare(this.ProcessExclusions[i], other.ProcessExclusions[i], StringComparison.CurrentCultureIgnoreCase) != 0)
                {
                    return false;
                }
            }

            for (var i = 0; i < this.MenuItems.StartProgramItems.Count; i++)
            {
                if (string.Compare(this.MenuItems.StartProgramItems[i].Title, other.MenuItems.StartProgramItems[i].Title, StringComparison.CurrentCultureIgnoreCase) != 0 ||
                    string.Compare(this.MenuItems.StartProgramItems[i].FileName, other.MenuItems.StartProgramItems[i].FileName, StringComparison.CurrentCultureIgnoreCase) != 0 ||
                    string.Compare(this.MenuItems.StartProgramItems[i].Arguments, other.MenuItems.StartProgramItems[i].Arguments, StringComparison.CurrentCultureIgnoreCase) != 0)
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            var hashCode = 0;

            foreach (var processExclusion in ProcessExclusions)
            {
                hashCode ^= processExclusion.GetHashCode();
            }

            foreach (var startProgramItem in MenuItems.StartProgramItems)
            {
                hashCode ^= startProgramItem.Title.GetHashCode() ^ startProgramItem.FileName.GetHashCode() ^ startProgramItem.Arguments.GetHashCode();
            }

            return hashCode;
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

            var systemTrayIconElement = document.XPathSelectElement("/smartSystemMenu/systemTrayIcon");
            if (systemTrayIconElement != null && systemTrayIconElement.Attribute("show") != null && systemTrayIconElement.Attribute("show").Value != null && systemTrayIconElement.Attribute("show").Value.ToLower() == "false")
            {
                settings.ShowSystemTrayIcon = false;
            }

            return settings;
        }

        public static void Save(string fileName, SmartSystemMenuSettings settings)
        {
            var document = new XDocument();
            document.Add(new XElement("smartSystemMenu",
                                 new XElement("processExclusions", settings.ProcessExclusions.Select(x => new XElement("processName", x))),
                                 new XElement("menuItems",
                                     new XElement("startProgramItem", settings.MenuItems.StartProgramItems.Select(x => new XElement("item", 
                                         new XAttribute("title", x.Title),
                                         new XAttribute("fileName", x.FileName),
                                         new XAttribute("arguments", x.Arguments)))))));
            Save(fileName, document);
        }

        private static void Save(string fileName, XDocument document)
        {
            using (TextWriter writer = new Utf8StringWriter())
            {
                document.Save(writer, SaveOptions.None);
                File.WriteAllText(fileName, writer.ToString());
            }
        }

        private class Utf8StringWriter : StringWriter
        {
            public override Encoding Encoding { get { return Encoding.UTF8; } }
        }
    }
}
