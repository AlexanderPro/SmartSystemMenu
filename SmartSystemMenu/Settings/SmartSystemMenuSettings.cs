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

            foreach (var menuItem in MenuItems.WindowSizeItems)
            {
                settings.MenuItems.WindowSizeItems.Add(new WindowSizeMenuItem { Title = menuItem.Title, Width = menuItem.Width, Height = menuItem.Height });
            }

            foreach (var menuItem in MenuItems.StartProgramItems)
            {
                settings.MenuItems.StartProgramItems.Add(new StartProgramMenuItem { Title = menuItem.Title, FileName = menuItem.FileName, Arguments = menuItem.Arguments });
            }

            foreach (var menuItem in MenuItems.Items)
            {
                settings.MenuItems.Items.Add(new MenuItem { Name = menuItem.Name, Key1 = menuItem.Key1, Key2 = menuItem.Key2, Key3 = menuItem.Key3 });
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

            if (GetType() != other.GetType())
                return false;

            return Equals(other as SmartSystemMenuSettings);
        }

        public bool Equals(SmartSystemMenuSettings other)
        {
            if (other == null)
                return false;

            if (object.ReferenceEquals(this, other))
                return true;

            if (GetType() != other.GetType())
                return false;

            if (ProcessExclusions.Count != other.ProcessExclusions.Count)
            {
                return false;
            }

            if (MenuItems.WindowSizeItems.Count != other.MenuItems.WindowSizeItems.Count)
            {
                return false;
            }

            if (MenuItems.StartProgramItems.Count != other.MenuItems.StartProgramItems.Count)
            {
                return false;
            }

            if (MenuItems.Items.Count != other.MenuItems.Items.Count)
            {
                return false;
            }

            for (var i = 0; i < ProcessExclusions.Count; i++)
            {
                if (string.Compare(ProcessExclusions[i], other.ProcessExclusions[i], StringComparison.CurrentCultureIgnoreCase) != 0)
                {
                    return false;
                }
            }

            for (var i = 0; i < MenuItems.WindowSizeItems.Count; i++)
            {
                if (string.Compare(MenuItems.WindowSizeItems[i].Title, other.MenuItems.WindowSizeItems[i].Title, StringComparison.CurrentCultureIgnoreCase) != 0 ||
                    MenuItems.WindowSizeItems[i].Width != other.MenuItems.WindowSizeItems[i].Width ||
                    MenuItems.WindowSizeItems[i].Height != other.MenuItems.WindowSizeItems[i].Height)
                {
                    return false;
                }
            }

            for (var i = 0; i < MenuItems.StartProgramItems.Count; i++)
            {
                if (string.Compare(MenuItems.StartProgramItems[i].Title, other.MenuItems.StartProgramItems[i].Title, StringComparison.CurrentCultureIgnoreCase) != 0 ||
                    string.Compare(MenuItems.StartProgramItems[i].FileName, other.MenuItems.StartProgramItems[i].FileName, StringComparison.CurrentCultureIgnoreCase) != 0 ||
                    string.Compare(MenuItems.StartProgramItems[i].Arguments, other.MenuItems.StartProgramItems[i].Arguments, StringComparison.CurrentCultureIgnoreCase) != 0)
                {
                    return false;
                }
            }

            for (var i = 0; i < MenuItems.Items.Count; i++)
            {
                if (string.Compare(MenuItems.Items[i].Name, other.MenuItems.Items[i].Name, StringComparison.CurrentCultureIgnoreCase) != 0 ||
                    MenuItems.Items[i].Show != other.MenuItems.Items[i].Show ||
                    MenuItems.Items[i].Key1 != other.MenuItems.Items[i].Key1 ||
                    MenuItems.Items[i].Key2 != other.MenuItems.Items[i].Key2 ||
                    MenuItems.Items[i].Key3 != other.MenuItems.Items[i].Key3)
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

            foreach (var item in MenuItems.WindowSizeItems)
            {
                hashCode ^= item.Title.GetHashCode() ^ item.Width.GetHashCode() ^ item.Height.GetHashCode();
            }

            foreach (var item in MenuItems.StartProgramItems)
            {
                hashCode ^= item.Title.GetHashCode() ^ item.FileName.GetHashCode() ^ item.Arguments.GetHashCode();
            }

            foreach (var item in MenuItems.Items)
            {
                hashCode ^= item.Name.GetHashCode() ^ item.Key1.GetHashCode() ^ item.Key2.GetHashCode() ^ item.Key3.GetHashCode();
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

            settings.MenuItems.WindowSizeItems = document
                .XPathSelectElements("/smartSystemMenu/menuItems/windowSizeItems/item")
                .Select(x => new WindowSizeMenuItem
                {
                    Title = x.Attribute("title") != null ? x.Attribute("title").Value : "",
                    Width = int.Parse(x.Attribute("width").Value),
                    Height = int.Parse(x.Attribute("height").Value)
                })
                .ToList();

            settings.MenuItems.StartProgramItems = document
                .XPathSelectElements("/smartSystemMenu/menuItems/startProgramItems/item")
                .Select(x => new StartProgramMenuItem
                {
                    Title = x.Attribute("title") != null ? x.Attribute("title").Value : "",
                    FileName = x.Attribute("fileName") != null ? x.Attribute("fileName").Value : "",
                    Arguments = x.Attribute("arguments") != null ? x.Attribute("arguments").Value : "",
                })
                .ToList();

            settings.MenuItems.Items = document
                .XPathSelectElements("/smartSystemMenu/menuItems/items/item")
                .Select(x => new MenuItem
                {
                    Name = x.Attribute("name") != null ? x.Attribute("name").Value : "",
                    Show = x.Attribute("show") != null ? x.Attribute("show").Value.ToLower() != "false" : true,
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
            var languageNameList = new[] { "en", "ru", "zh_cn", "zh_tw", "ja", "ko", "de", "sr" };
            if (languageElement != null && languageElement.Attribute("name") != null && languageElement.Attribute("name").Value != null)
            {
                languageName = languageElement.Attribute("name").Value.ToLower().Trim();
                settings.LanguageName = languageName;
            }

            if (languageName == "" && (Thread.CurrentThread.CurrentCulture.Name == "zh-CN"))
            {
                languageName = "zh_cn";
            }

            if (languageName == "" && (Thread.CurrentThread.CurrentCulture.Name == "zh-TW"))
            {
                languageName = "zh_tw";
            }

            if (languageName == "" && Thread.CurrentThread.CurrentCulture.Name == "ja-JP")
            {
                languageName = "ja";
            }

            if (languageName == "" && (Thread.CurrentThread.CurrentCulture.Name == "ko-KR" || Thread.CurrentThread.CurrentCulture.Name == "ko-KP"))
            {
                languageName = "ko";
            }

            if (languageName == "" && Thread.CurrentThread.CurrentCulture.Name == "ru-RU")
            {
                languageName = "ru";
            }

            if (languageName == "" && Thread.CurrentThread.CurrentCulture.Name == "de-DE")
            {
                languageName = "de";
            }

            if (languageName == "" && (Thread.CurrentThread.CurrentCulture.Name == "sr-Cyrl" ||
                Thread.CurrentThread.CurrentCulture.Name == "sr-Cyrl-BA" ||
                Thread.CurrentThread.CurrentCulture.Name == "sr-Cyrl-ME" ||
                Thread.CurrentThread.CurrentCulture.Name == "sr-Cyrl-RS" ||
                Thread.CurrentThread.CurrentCulture.Name == "sr-Cyrl-CS"))
            {
                languageName = "sr";
            }

            if (languageName == "" || !languageNameList.Contains(languageName))
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
                                     new XElement("items", settings.MenuItems.Items.Select(x => new XElement("item",
                                         new XAttribute("name", x.Name),
                                         x.Show == false ? new XAttribute("show", x.Show.ToString().ToLower()) : null,
                                         new XAttribute("key1", x.Key1 == VirtualKeyModifier.None ? "" : ((int)x.Key1).ToString()),
                                         new XAttribute("key2", x.Key2 == VirtualKeyModifier.None ? "" : ((int)x.Key2).ToString()),
                                         new XAttribute("key3", x.Key3 == VirtualKey.None ? "" : ((int)x.Key3).ToString())))),
                                     new XElement("windowSizeItems", settings.MenuItems.WindowSizeItems.Select(x => new XElement("item",
                                         new XAttribute("title", x.Title),
                                         new XAttribute("width", x.Width),
                                         new XAttribute("height", x.Height)))),
                                     new XElement("startProgramItems", settings.MenuItems.StartProgramItems.Select(x => new XElement("item",
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
