using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Text;
using System.Threading;
using SmartSystemMenu.HotKeys;

namespace SmartSystemMenu.Settings
{
    public class SmartSystemMenuSettings : ICloneable
    {
        public IList<string> ProcessExclusions { get; private set; }

        public MenuItems MenuItems { get; private set; }

        public bool ShowSystemTrayIcon { get; private set; }

        public string LanguageName { get; set; }

        public LanguageSettings LanguageSettings { get; set; }

        public SmartSystemMenuSettings()
        {
            ProcessExclusions = new List<string>();
            MenuItems = new MenuItems();
            ShowSystemTrayIcon = true;
            LanguageName = "";
            LanguageSettings = new LanguageSettings();
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
                settings.MenuItems.StartProgramItems.Add(new StartProgramMenuItem { Title = menuItem.Title, FileName = menuItem.FileName, Arguments = menuItem.Arguments });
            }

            foreach (var languageItem in LanguageSettings.Items)
            {
                settings.LanguageSettings.Items.Add(new LanguageItem { Name = languageItem.Name, Value = languageItem.Value });
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

            if (string.Compare(LanguageName, other.LanguageName, StringComparison.CurrentCultureIgnoreCase) != 0)
            {
                return false;
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

            hashCode ^= LanguageName.GetHashCode();
            return hashCode;
        }

        public static SmartSystemMenuSettings Read(string fileName, string languageFileName)
        {
            var settings = new SmartSystemMenuSettings();
            var document = XDocument.Load(fileName);
            var languageDocument = XDocument.Load(languageFileName);

            settings.ProcessExclusions = document
                .XPathSelectElements("/smartSystemMenu/processExclusions/processName")
                .Where(x => !string.IsNullOrWhiteSpace(x.Value))
                .Select(x => x.Value.ToLower())
                .ToList();

            settings.MenuItems.StartProgramItems = document
                .XPathSelectElements("/smartSystemMenu/menuItems/startProgramItem/item")
                .Select(x => new StartProgramMenuItem {
                    Title = x.Attribute("title") != null ? x.Attribute("title").Value : "",
                    FileName = x.Attribute("fileName") != null ? x.Attribute("fileName").Value : "",
                    Arguments = x.Attribute("arguments") != null ? x.Attribute("arguments").Value : "",
                })
                .ToList();

            settings.MenuItems.Items = document
                .XPathSelectElements("/smartSystemMenu/menuItems/item")
                .Select(x => new MenuItem {
                   Name = x.Attribute("name") != null ? x.Attribute("name").Value : "",
                   HotKeyEnabled = x.Attribute("hotKeyEnabled") != null && !string.IsNullOrEmpty(x.Attribute("hotKeyEnabled").Value) ? x.Attribute("hotKeyEnabled").Value.ToLower() == "true" : false,
                   Key1 = x.Attribute("key1") != null && !string.IsNullOrEmpty(x.Attribute("key1").Value) ? (VirtualKeyModifier)int.Parse(x.Attribute("key1").Value) : VirtualKeyModifier.None,
                   Key2 = x.Attribute("key2") != null && !string.IsNullOrEmpty(x.Attribute("key2").Value) ? (VirtualKeyModifier)int.Parse(x.Attribute("key2").Value) : VirtualKeyModifier.None,
                   Key3 = x.Attribute("key3") != null && !string.IsNullOrEmpty(x.Attribute("key3").Value) ? (VirtualKey)int.Parse(x.Attribute("key3").Value) : VirtualKey.None
                })
                .ToList();

            var systemTrayIconElement = document.XPathSelectElement("/smartSystemMenu/systemTrayIcon");
            if (systemTrayIconElement != null && systemTrayIconElement.Attribute("show") != null && systemTrayIconElement.Attribute("show").Value != null && systemTrayIconElement.Attribute("show").Value.ToLower() == "false")
            {
                settings.ShowSystemTrayIcon = false;
            }

            var languageElement = document.XPathSelectElement("/smartSystemMenu/language");
            var languageName = "";
            if (languageElement != null && languageElement.Attribute("name") != null && languageElement.Attribute("name").Value != null)
            {
                languageName = languageElement.Attribute("name").Value.ToLower().Trim();
                settings.LanguageName = languageName;
            }

            if (languageName == "" && Thread.CurrentThread.CurrentCulture.Name == "zh-CN" || Thread.CurrentThread.CurrentCulture.Name == "zh-TW")
            {
                languageName = "cn";
            }

            if (languageName == "" && Thread.CurrentThread.CurrentCulture.Name == "ja-JP")
            {
                languageName = "ja";
            }

            if (languageName == "" && Thread.CurrentThread.CurrentCulture.Name == "ko-KR" || Thread.CurrentThread.CurrentCulture.Name == "ko-KP")
            {
                languageName = "ko";
            }

            if (languageName == "" && Thread.CurrentThread.CurrentCulture.Name == "ru-RU")
            {
                languageName = "ru";
            }

            if (languageName == "")
            {
                languageName = "en";
            }

            var languageItemPath = "/language/items/" + languageName + "/item";
            settings.LanguageSettings.Items = languageDocument
                .XPathSelectElements(languageItemPath)
                .Select(x => new LanguageItem
                {
                    Name = x.Attribute("name") != null ? x.Attribute("name").Value : "",
                    Value = x.Attribute("value") != null ? x.Attribute("value").Value : "",
                })
                .ToList();

            return settings;
        }

        public static void Save(string fileName, SmartSystemMenuSettings settings)
        {
            var document = new XDocument();
            document.Add(new XElement("smartSystemMenu",
                                 new XElement("processExclusions", settings.ProcessExclusions.Select(x => new XElement("processName", x))),
                                 new XElement("menuItems",
                                     new XElement("item", settings.MenuItems.Items.Select(x => new XElement("item",
                                         new XAttribute("name", x.Name),
                                         new XAttribute("hotKeyEnabled", x.HotKeyEnabled.ToString().ToLower()),
                                         new XAttribute("key1", x.Key1 == VirtualKeyModifier.None ? "" : ((int)x.Key1).ToString()),
                                         new XAttribute("key2", x.Key2 == VirtualKeyModifier.None ? "" : ((int)x.Key2).ToString()),
                                         new XAttribute("key3", x.Key3 == VirtualKey.None ? "" : ((int)x.Key3).ToString())))),
                                     new XElement("startProgramItem", settings.MenuItems.StartProgramItems.Select(x => new XElement("item", 
                                         new XAttribute("title", x.Title),
                                         new XAttribute("fileName", x.FileName),
                                         new XAttribute("arguments", x.Arguments))))),
                                 new XElement("systemTrayIcon",
                                     new XAttribute("show", settings.ShowSystemTrayIcon.ToString().ToLower())
                                 ),
                                 new XElement("language",
                                     new XAttribute("name", settings.LanguageName.ToLower())
                                 )));
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
