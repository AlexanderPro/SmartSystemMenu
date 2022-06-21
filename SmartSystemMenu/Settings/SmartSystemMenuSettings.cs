using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Text;
using System.Threading;
using SmartSystemMenu.HotKeys;
using SmartSystemMenu.Utils;

namespace SmartSystemMenu.Settings
{
    public class SmartSystemMenuSettings : ICloneable
    {
        public IList<string> ProcessExclusions { get; private set; }

        public MenuItems MenuItems { get; private set; }

        public CloserSettings Closer { get; private set; }

        public SaveSelectedItemsSettings SaveSelectedItems { get; set; }

        public bool ShowSystemTrayIcon { get; private set; }

        public bool EnableHighDPI { get; set; }

        public WindowSizerType Sizer { get; set; }

        public string LanguageName { get; set; }

        public LanguageSettings Language { get; set; }


        public SmartSystemMenuSettings()
        {
            ProcessExclusions = new List<string>();
            MenuItems = new MenuItems();
            Closer = new CloserSettings();
            SaveSelectedItems = new SaveSelectedItemsSettings();
            Sizer = WindowSizerType.WindowWithMargins;
            ShowSystemTrayIcon = true;
            EnableHighDPI = false;
            LanguageName = "";
            Language = new LanguageSettings();
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

            foreach (var languageItem in Language.Items)
            {
                settings.Language.Items.Add(new LanguageItem { Name = languageItem.Name, Value = languageItem.Value });
            }

            settings.Closer= (CloserSettings)Closer.Clone();
            settings.SaveSelectedItems = (SaveSelectedItemsSettings)SaveSelectedItems.Clone();
            settings.Sizer = Sizer;
            settings.ShowSystemTrayIcon = ShowSystemTrayIcon;
            settings.EnableHighDPI = EnableHighDPI;
            settings.LanguageName = LanguageName;
            return settings;
        }

        public override bool Equals(object other)
        {
            if (other == null)
            {
                return false;
            }

            if (object.ReferenceEquals(this, other))
            {
                return true;
            }

            if (GetType() != other.GetType())
            {
                return false;
            }

            return Equals(other as SmartSystemMenuSettings);
        }

        public bool Equals(SmartSystemMenuSettings other)
        {
            if (other == null)
            {
                return false;
            }

            if (object.ReferenceEquals(this, other))
            {
                return true;
            }

            if (GetType() != other.GetType())
            {
                return false;
            }

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
                    MenuItems.WindowSizeItems[i].Left != other.MenuItems.WindowSizeItems[i].Left ||
                    MenuItems.WindowSizeItems[i].Top != other.MenuItems.WindowSizeItems[i].Top ||
                    MenuItems.WindowSizeItems[i].Width != other.MenuItems.WindowSizeItems[i].Width ||
                    MenuItems.WindowSizeItems[i].Height != other.MenuItems.WindowSizeItems[i].Height ||
                    MenuItems.WindowSizeItems[i].Key1 != other.MenuItems.WindowSizeItems[i].Key1 ||
                    MenuItems.WindowSizeItems[i].Key2 != other.MenuItems.WindowSizeItems[i].Key2 ||
                    MenuItems.WindowSizeItems[i].Key3 != other.MenuItems.WindowSizeItems[i].Key3)
                {
                    return false;
                }
            }

            for (var i = 0; i < MenuItems.StartProgramItems.Count; i++)
            {
                if (string.Compare(MenuItems.StartProgramItems[i].Title, other.MenuItems.StartProgramItems[i].Title, StringComparison.CurrentCultureIgnoreCase) != 0 ||
                    string.Compare(MenuItems.StartProgramItems[i].FileName, other.MenuItems.StartProgramItems[i].FileName, StringComparison.CurrentCultureIgnoreCase) != 0 ||
                    string.Compare(MenuItems.StartProgramItems[i].Arguments, other.MenuItems.StartProgramItems[i].Arguments, StringComparison.CurrentCultureIgnoreCase) != 0 ||
                    string.Compare(MenuItems.StartProgramItems[i].BeginParameter, other.MenuItems.StartProgramItems[i].BeginParameter, StringComparison.CurrentCultureIgnoreCase) != 0 ||
                    string.Compare(MenuItems.StartProgramItems[i].EndParameter, other.MenuItems.StartProgramItems[i].EndParameter, StringComparison.CurrentCultureIgnoreCase) != 0 ||
                    MenuItems.StartProgramItems[i].ShowWindow != other.MenuItems.StartProgramItems[i].ShowWindow ||
                    MenuItems.StartProgramItems[i].RunAs != other.MenuItems.StartProgramItems[i].RunAs ||
                    MenuItems.StartProgramItems[i].UseWindowWorkingDirectory != other.MenuItems.StartProgramItems[i].UseWindowWorkingDirectory)
                {
                    return false;
                }
            }

            for (var i = 0; i < MenuItems.Items.Count; i++)
            {
                if (string.Compare(MenuItems.Items[i].Name, other.MenuItems.Items[i].Name, StringComparison.CurrentCultureIgnoreCase) != 0 ||
                    MenuItems.Items[i].Show != other.MenuItems.Items[i].Show ||
                    MenuItems.Items[i].Type != other.MenuItems.Items[i].Type ||
                    MenuItems.Items[i].Key1 != other.MenuItems.Items[i].Key1 ||
                    MenuItems.Items[i].Key2 != other.MenuItems.Items[i].Key2 ||
                    MenuItems.Items[i].Key3 != other.MenuItems.Items[i].Key3)
                {
                    return false;
                }

                if (MenuItems.Items[i].Items.Count != other.MenuItems.Items[i].Items.Count)
                {
                    return false;
                }

                for (var j = 0; j < MenuItems.Items[i].Items.Count; j++)
                {
                    if (string.Compare(MenuItems.Items[i].Items[j].Name, other.MenuItems.Items[i].Items[j].Name, StringComparison.CurrentCultureIgnoreCase) != 0 ||
                        MenuItems.Items[i].Items[j].Show != other.MenuItems.Items[i].Items[j].Show ||
                        MenuItems.Items[i].Items[j].Type != other.MenuItems.Items[i].Items[j].Type ||
                        MenuItems.Items[i].Items[j].Key1 != other.MenuItems.Items[i].Items[j].Key1 ||
                        MenuItems.Items[i].Items[j].Key2 != other.MenuItems.Items[i].Items[j].Key2 ||
                        MenuItems.Items[i].Items[j].Key3 != other.MenuItems.Items[i].Items[j].Key3)
                    {
                        return false;
                    }
                }
            }

            if (Closer.Type != other.Closer.Type || Closer.Key1 != other.Closer.Key1 || Closer.Key2 != other.Closer.Key2 || Closer.MouseButton != other.Closer.MouseButton)
            {
                return false;
            }

            if (SaveSelectedItems.AeroGlass != other.SaveSelectedItems.AeroGlass ||
                SaveSelectedItems.AlwaysOnTop != other.SaveSelectedItems.AlwaysOnTop ||
                SaveSelectedItems.HideForAltTab != other.SaveSelectedItems.HideForAltTab ||
                SaveSelectedItems.Alignment != other.SaveSelectedItems.Alignment ||
                SaveSelectedItems.Transparency != other.SaveSelectedItems.Transparency ||
                SaveSelectedItems.Priority != other.SaveSelectedItems.Priority ||
                SaveSelectedItems.MinimizeToTrayAlways != other.SaveSelectedItems.MinimizeToTrayAlways)
            {
                return false;
            }

            if (Sizer != other.Sizer)
            {
                return false;
            }

            if (ShowSystemTrayIcon != other.ShowSystemTrayIcon)
            {
                return false;
            }

            if (EnableHighDPI != other.EnableHighDPI)
            {
                return false;
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
                hashCode ^= item.Title.GetHashCode() ^ item.Left.GetHashCode() ^ item.Top.GetHashCode() ^ item.Width.GetHashCode() ^ item.Height.GetHashCode() ^ item.Key1.GetHashCode() ^ item.Key2.GetHashCode() ^ item.Key3.GetHashCode();
            }

            foreach (var item in MenuItems.StartProgramItems)
            {
                hashCode ^= item.Title.GetHashCode() ^ item.FileName.GetHashCode() ^ item.Arguments.GetHashCode() ^ item.UseWindowWorkingDirectory.GetHashCode() ^ item.RunAs.GetHashCode() ^ item.BeginParameter.GetHashCode() ^ item.EndParameter.GetHashCode();
            }

            foreach (var item in MenuItems.Items)
            {
                hashCode ^= item.Show.GetHashCode() ^ item.Type.GetHashCode() ^  item.Name.GetHashCode() ^ item.Key1.GetHashCode() ^ item.Key2.GetHashCode() ^ item.Key3.GetHashCode();
                foreach (var subItem in item.Items)
                {
                    hashCode ^= subItem.Show.GetHashCode() ^ subItem.Type.GetHashCode() ^ subItem.Name.GetHashCode() ^ subItem.Key1.GetHashCode() ^ subItem.Key2.GetHashCode() ^ subItem.Key3.GetHashCode();
                }
            }

            hashCode ^= Closer.Type.GetHashCode();
            hashCode ^= Closer.Key1.GetHashCode();
            hashCode ^= Closer.Key2.GetHashCode();
            hashCode ^= Closer.MouseButton.GetHashCode();
            hashCode ^= SaveSelectedItems.AeroGlass.GetHashCode();
            hashCode ^= SaveSelectedItems.AlwaysOnTop.GetHashCode();
            hashCode ^= SaveSelectedItems.HideForAltTab.GetHashCode();
            hashCode ^= SaveSelectedItems.Alignment.GetHashCode();
            hashCode ^= SaveSelectedItems.Transparency.GetHashCode();
            hashCode ^= SaveSelectedItems.Priority.GetHashCode();
            hashCode ^= SaveSelectedItems.MinimizeToTrayAlways.GetHashCode();
            hashCode ^= Sizer.GetHashCode();
            hashCode ^= LanguageName.GetHashCode();
            hashCode ^= ShowSystemTrayIcon.GetHashCode();
            hashCode ^= EnableHighDPI.GetHashCode();
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
                    Left = !string.IsNullOrEmpty(x.Attribute("left").Value) ? int.Parse(x.Attribute("left").Value) : (int?)null,
                    Top = !string.IsNullOrEmpty(x.Attribute("top").Value) ? int.Parse(x.Attribute("top").Value) : (int?)null,
                    Width = int.Parse(x.Attribute("width").Value),
                    Height = int.Parse(x.Attribute("height").Value),
                    Key1 = x.Attribute("key1") != null && !string.IsNullOrEmpty(x.Attribute("key1").Value) ? (VirtualKeyModifier)int.Parse(x.Attribute("key1").Value) : VirtualKeyModifier.None,
                    Key2 = x.Attribute("key2") != null && !string.IsNullOrEmpty(x.Attribute("key2").Value) ? (VirtualKeyModifier)int.Parse(x.Attribute("key2").Value) : VirtualKeyModifier.None,
                    Key3 = x.Attribute("key3") != null && !string.IsNullOrEmpty(x.Attribute("key3").Value) ? (VirtualKey)int.Parse(x.Attribute("key3").Value) : VirtualKey.None
                })
                .ToList();

            settings.MenuItems.StartProgramItems = document
                .XPathSelectElements("/smartSystemMenu/menuItems/startProgramItems/item")
                .Select(x => new StartProgramMenuItem
                {
                    Title = x.Attribute("title") != null ? x.Attribute("title").Value : "",
                    FileName = x.Attribute("fileName") != null ? x.Attribute("fileName").Value : "",
                    Arguments = x.Attribute("arguments") != null ? x.Attribute("arguments").Value : "",
                    BeginParameter = x.Attribute("beginParameter") != null ? x.Attribute("beginParameter").Value : "",
                    EndParameter = x.Attribute("endParameter") != null ? x.Attribute("endParameter").Value : "",
                    RunAs = x.Attribute("runAs") != null && !string.IsNullOrEmpty(x.Attribute("runAs").Value) ? (UserType)Enum.Parse(typeof(UserType), x.Attribute("runAs").Value, true) : UserType.Normal,
                    ShowWindow = x.Attribute("showWindow") != null && !string.IsNullOrEmpty(x.Attribute("showWindow").Value) ? x.Attribute("showWindow").Value.ToLower() == "true" : true,
                    UseWindowWorkingDirectory = x.Attribute("useWindowWorkingDirectory") != null && !string.IsNullOrEmpty(x.Attribute("useWindowWorkingDirectory").Value) ? x.Attribute("useWindowWorkingDirectory").Value.ToLower() == "true" : false
                })
                .ToList();

            settings.MenuItems.Items = document
                .XPathSelectElements("/smartSystemMenu/menuItems/items/item")
                .Select(x => {
                    var menuItem = new MenuItem
                    {
                        Name = x.Attribute("name") != null ? x.Attribute("name").Value : "",
                        Show = x.Attribute("show") != null ? x.Attribute("show").Value.ToLower() != "false" : true,
                        Type = x.Attribute("type") != null && !string.IsNullOrEmpty(x.Attribute("type").Value) ? (MenuItemType)Enum.Parse(typeof(MenuItemType), x.Attribute("type").Value, true) : MenuItemType.Item,
                        Key1 = x.Attribute("key1") != null && !string.IsNullOrEmpty(x.Attribute("key1").Value) ? (VirtualKeyModifier)int.Parse(x.Attribute("key1").Value) : VirtualKeyModifier.None,
                        Key2 = x.Attribute("key2") != null && !string.IsNullOrEmpty(x.Attribute("key2").Value) ? (VirtualKeyModifier)int.Parse(x.Attribute("key2").Value) : VirtualKeyModifier.None,
                        Key3 = x.Attribute("key3") != null && !string.IsNullOrEmpty(x.Attribute("key3").Value) ? (VirtualKey)int.Parse(x.Attribute("key3").Value) : VirtualKey.None
                    };
                    menuItem.Items = menuItem.Type == MenuItemType.Group ?
                    x.XPathSelectElements("./items/item")
                    .Select(y => new MenuItem
                    {
                        Name = y.Attribute("name") != null ? y.Attribute("name").Value : "",
                        Show = y.Attribute("show") != null ? y.Attribute("show").Value.ToLower() != "false" : true,
                        Type = y.Attribute("type") != null && !string.IsNullOrEmpty(y.Attribute("type").Value) ? (MenuItemType)Enum.Parse(typeof(MenuItemType), y.Attribute("type").Value, true) : MenuItemType.Item,
                        Key1 = y.Attribute("key1") != null && !string.IsNullOrEmpty(y.Attribute("key1").Value) ? (VirtualKeyModifier)int.Parse(y.Attribute("key1").Value) : VirtualKeyModifier.None,
                        Key2 = y.Attribute("key2") != null && !string.IsNullOrEmpty(y.Attribute("key2").Value) ? (VirtualKeyModifier)int.Parse(y.Attribute("key2").Value) : VirtualKeyModifier.None,
                        Key3 = y.Attribute("key3") != null && !string.IsNullOrEmpty(y.Attribute("key3").Value) ? (VirtualKey)int.Parse(y.Attribute("key3").Value) : VirtualKey.None
                    }).ToList() : new List<MenuItem>();
                    return menuItem;
                })
                .ToList();

            var closerElement = document.XPathSelectElement("/smartSystemMenu/closer");
            settings.Closer.Type = closerElement.Attribute("type") != null && !string.IsNullOrEmpty(closerElement.Attribute("type").Value) ? (WindowCloserType)int.Parse(closerElement.Attribute("type").Value) : WindowCloserType.CloseForegroundWindow;
            settings.Closer.Key1 = closerElement.Attribute("key1") != null && !string.IsNullOrEmpty(closerElement.Attribute("key1").Value) ? (VirtualKeyModifier)int.Parse(closerElement.Attribute("key1").Value) : VirtualKeyModifier.None;
            settings.Closer.Key2 = closerElement.Attribute("key2") != null && !string.IsNullOrEmpty(closerElement.Attribute("key2").Value) ? (VirtualKeyModifier)int.Parse(closerElement.Attribute("key2").Value) : VirtualKeyModifier.None;
            settings.Closer.MouseButton = closerElement.Attribute("mouseButton") != null && !string.IsNullOrEmpty(closerElement.Attribute("mouseButton").Value) ? (MouseButton)int.Parse(closerElement.Attribute("mouseButton").Value) : MouseButton.None;

            var saveSelectedItemsElement = document.XPathSelectElement("/smartSystemMenu/saveSelectedItems");
            settings.SaveSelectedItems.AeroGlass = saveSelectedItemsElement.Attribute("aeroGlass") != null && !string.IsNullOrEmpty(saveSelectedItemsElement.Attribute("aeroGlass").Value) ? saveSelectedItemsElement.Attribute("aeroGlass").Value.ToLower() == "true" : true;
            settings.SaveSelectedItems.AlwaysOnTop = saveSelectedItemsElement.Attribute("alwaysOnTop") != null && !string.IsNullOrEmpty(saveSelectedItemsElement.Attribute("alwaysOnTop").Value) ? saveSelectedItemsElement.Attribute("alwaysOnTop").Value.ToLower() == "true" : true;
            settings.SaveSelectedItems.HideForAltTab = saveSelectedItemsElement.Attribute("hideForAltTab") != null && !string.IsNullOrEmpty(saveSelectedItemsElement.Attribute("hideForAltTab").Value) ? saveSelectedItemsElement.Attribute("hideForAltTab").Value.ToLower() == "true" : true;
            settings.SaveSelectedItems.Alignment = saveSelectedItemsElement.Attribute("alignment") != null && !string.IsNullOrEmpty(saveSelectedItemsElement.Attribute("alignment").Value) ? saveSelectedItemsElement.Attribute("alignment").Value.ToLower() == "true" : true;
            settings.SaveSelectedItems.Transparency = saveSelectedItemsElement.Attribute("transparency") != null && !string.IsNullOrEmpty(saveSelectedItemsElement.Attribute("transparency").Value) ? saveSelectedItemsElement.Attribute("transparency").Value.ToLower() == "true" : true;
            settings.SaveSelectedItems.Priority = saveSelectedItemsElement.Attribute("priority") != null && !string.IsNullOrEmpty(saveSelectedItemsElement.Attribute("priority").Value) ? saveSelectedItemsElement.Attribute("priority").Value.ToLower() == "true" : true;
            settings.SaveSelectedItems.MinimizeToTrayAlways = saveSelectedItemsElement.Attribute("minimizeToTrayAlways") != null && !string.IsNullOrEmpty(saveSelectedItemsElement.Attribute("minimizeToTrayAlways").Value) ? saveSelectedItemsElement.Attribute("minimizeToTrayAlways").Value.ToLower() == "true" : true;

            var sizerElement = document.XPathSelectElement("/smartSystemMenu/sizer");
            settings.Sizer = sizerElement.Attribute("type") != null && !string.IsNullOrEmpty(sizerElement.Attribute("type").Value) ? (WindowSizerType)int.Parse(sizerElement.Attribute("type").Value) : WindowSizerType.WindowWithMargins;

            var systemTrayIconElement = document.XPathSelectElement("/smartSystemMenu/systemTrayIcon");
            if (systemTrayIconElement != null && systemTrayIconElement.Attribute("show") != null && systemTrayIconElement.Attribute("show").Value != null && systemTrayIconElement.Attribute("show").Value.ToLower() == "false")
            {
                settings.ShowSystemTrayIcon = false;
            }

            var displayElement = document.XPathSelectElement("/smartSystemMenu/display");
            if (displayElement != null && displayElement.Attribute("highDPI") != null && displayElement.Attribute("highDPI").Value != null && displayElement.Attribute("highDPI").Value.ToLower() == "true")
            {
                settings.EnableHighDPI = true;
            }

            var languageElement = document.XPathSelectElement("/smartSystemMenu/language");
            var languageName = "";
            var languageNameList = new[] { "en", "ru", "zh_cn", "zh_tw", "ja", "ko", "de", "fr", "it", "sr", "pt" };
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

            if (languageName == "" && Thread.CurrentThread.CurrentCulture.Name == "fr-FR")
            {
                languageName = "fr";
            }

            if (languageName == "" && (Thread.CurrentThread.CurrentCulture.Name == "it-IT" ||
                Thread.CurrentThread.CurrentCulture.Name == "it-SM" ||
                Thread.CurrentThread.CurrentCulture.Name == "it-CH" ||
                Thread.CurrentThread.CurrentCulture.Name == "it-VA"))
            {
                languageName = "it";
            }


            if (languageName == "" && (Thread.CurrentThread.CurrentCulture.Name == "pt-BR" || Thread.CurrentThread.CurrentCulture.Name == "pt-PT"))
            {
                languageName = "pt";
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
            settings.Language.Items = languageDocument
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
                                         new XAttribute("type", x.Type.ToString()),
                                         x.Type == MenuItemType.Item || x.Type == MenuItemType.Group ? new XAttribute("name", x.Name) : null,
                                         x.Show == false ? new XAttribute("show", x.Show.ToString().ToLower()) : null,
                                         x.Type == MenuItemType.Item ? new XAttribute("key1", x.Key1 == VirtualKeyModifier.None ? "" : ((int)x.Key1).ToString()) : null,
                                         x.Type == MenuItemType.Item ? new XAttribute("key2", x.Key2 == VirtualKeyModifier.None ? "" : ((int)x.Key2).ToString()) : null,
                                         x.Type == MenuItemType.Item ? new XAttribute("key3", x.Key3 == VirtualKey.None ? "" : ((int)x.Key3).ToString()) : null,
                                         x.Items.Any() ?
                                            new XElement("items", x.Items.Select(y => new XElement("item",
                                            new XAttribute("type", y.Type.ToString()),
                                            y.Type == MenuItemType.Item || y.Type == MenuItemType.Group ? new XAttribute("name", y.Name) : null,
                                            y.Show == false ? new XAttribute("show", y.Show.ToString().ToLower()) : null,
                                            y.Type == MenuItemType.Item ? new XAttribute("key1", y.Key1 == VirtualKeyModifier.None ? "" : ((int)y.Key1).ToString()) : null,
                                            y.Type == MenuItemType.Item ? new XAttribute("key2", y.Key2 == VirtualKeyModifier.None ? "" : ((int)y.Key2).ToString()) : null,
                                            y.Type == MenuItemType.Item ? new XAttribute("key3", y.Key3 == VirtualKey.None ? "" : ((int)y.Key3).ToString()) : null))) : null))),
                                     new XElement("windowSizeItems", settings.MenuItems.WindowSizeItems.Select(x => new XElement("item",
                                         new XAttribute("title", x.Title),
                                         new XAttribute("left", x.Left == null ? "" : x.Left.Value.ToString()),
                                         new XAttribute("top", x.Top == null ? "" : x.Top.Value.ToString()),
                                         new XAttribute("width", x.Width),
                                         new XAttribute("height", x.Height),
                                         new XAttribute("key1", x.Key1 == VirtualKeyModifier.None ? "" : ((int)x.Key1).ToString()),
                                         new XAttribute("key2", x.Key2 == VirtualKeyModifier.None ? "" : ((int)x.Key2).ToString()),
                                         new XAttribute("key3", x.Key3 == VirtualKey.None ? "" : ((int)x.Key3).ToString())))),
                                     new XElement("startProgramItems", settings.MenuItems.StartProgramItems.Select(x => new XElement("item",
                                         new XAttribute("title", x.Title),
                                         new XAttribute("fileName", x.FileName),
                                         new XAttribute("arguments", x.Arguments),
                                         new XAttribute("useWindowWorkingDirectory", x.UseWindowWorkingDirectory.ToString().ToLower()),
                                         new XAttribute("runAs", x.RunAs.ToString().ToLower()),
                                         new XAttribute("showWindow", x.ShowWindow.ToString().ToLower()),
                                         new XAttribute("beginParameter", x.BeginParameter),
                                         new XAttribute("endParameter", x.EndParameter))))),
                                 new XElement("closer",
                                     new XAttribute("type", ((int)settings.Closer.Type).ToString()),
                                     new XAttribute("key1", settings.Closer.Key1 == VirtualKeyModifier.None ? "" : ((int)settings.Closer.Key1).ToString()),
                                     new XAttribute("key2", settings.Closer.Key2 == VirtualKeyModifier.None ? "" : ((int)settings.Closer.Key2).ToString()),
                                     new XAttribute("mouseButton", settings.Closer.MouseButton == MouseButton.None ? "" : ((int)settings.Closer.MouseButton).ToString())
                                 ),
                                 new XElement("saveSelectedItems",
                                     new XAttribute("aeroGlass", settings.SaveSelectedItems.AeroGlass.ToString().ToLower()),
                                     new XAttribute("alwaysOnTop", settings.SaveSelectedItems.AlwaysOnTop.ToString().ToLower()),
                                     new XAttribute("hideForAltTab", settings.SaveSelectedItems.HideForAltTab.ToString().ToLower()),
                                     new XAttribute("alignment", settings.SaveSelectedItems.Alignment.ToString().ToLower()),
                                     new XAttribute("transparency", settings.SaveSelectedItems.Transparency.ToString().ToLower()),
                                     new XAttribute("priority", settings.SaveSelectedItems.Priority.ToString().ToLower()),
                                     new XAttribute("minimizeToTrayAlways", settings.SaveSelectedItems.MinimizeToTrayAlways.ToString().ToLower())
                                 ),
                                 new XElement("sizer",
                                     new XAttribute("type", ((int)settings.Sizer).ToString())
                                 ),
                                 new XElement("systemTrayIcon",
                                     new XAttribute("show", settings.ShowSystemTrayIcon.ToString().ToLower())
                                 ),
                                 new XElement("display",
                                     new XAttribute("highDPI", settings.EnableHighDPI.ToString().ToLower())
                                 ),
                                 new XElement("language",
                                     new XAttribute("name", settings.LanguageName.ToLower())
                                 )));
            FileUtils.Save(fileName, document);
        }
    }
}
