using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Threading;
using SmartSystemMenu.HotKeys;
using SmartSystemMenu.Utils;

namespace SmartSystemMenu.Settings
{
    static class ApplicationSettingsFile
    {
        public static ApplicationSettings Read(string fileName, string languageFileName)
        {
            var settings = new ApplicationSettings();
            var document = XDocument.Load(fileName);
            var languageDocument = XDocument.Load(languageFileName);

            settings.ExcludedProcessItems = document
                .XPathSelectElements("/smartSystemMenu/processExclusions/processName")
                .Where(x => !string.IsNullOrWhiteSpace(x.Value))
                .Select(x => new ExcludedProcessItem
                {
                   Name = x.Value.ToLower(),
                   IgnoreHook = x.Attribute("ignoreHook") != null && x.Attribute("ignoreHook").Value.ToLower() == "true",
                })
                .ToList();

            settings.InitEventProcessNames = document
                .XPathSelectElements("/smartSystemMenu/createMenuOnInitEvent/processName")
                .Where(x => !string.IsNullOrWhiteSpace(x.Value))
                .Select(x => x.Value.ToLower())
                .ToList();

            settings.NoRestoreMenuProcessNames = document
                .XPathSelectElements("/smartSystemMenu/noRestoreMenuOnExit/processName")
                .Where(x => !string.IsNullOrWhiteSpace(x.Value))
                .Select(x => x.Value.ToLower())
                .ToList();

            settings.MenuItems.WindowSizeItems = document
                .XPathSelectElements("/smartSystemMenu/menuItems/windowSizeItems/item")
                .Select(x => new WindowSizeMenuItem
                {
                    Title = x.Attribute("title") != null ? x.Attribute("title").Value : string.Empty,
                    Left = x.Attribute("left") != null && !string.IsNullOrEmpty(x.Attribute("left").Value) ? int.Parse(x.Attribute("left").Value) : null,
                    Top = x.Attribute("top") != null && !string.IsNullOrEmpty(x.Attribute("top").Value) ? int.Parse(x.Attribute("top").Value) : null,
                    Width = x.Attribute("width") != null && !string.IsNullOrEmpty(x.Attribute("width").Value) ? int.Parse(x.Attribute("width").Value) : null,
                    Height = x.Attribute("height") != null && !string.IsNullOrEmpty(x.Attribute("height").Value) ? int.Parse(x.Attribute("height").Value) : null,
                    Key1 = x.Attribute("key1") != null && !string.IsNullOrEmpty(x.Attribute("key1").Value) ? (VirtualKeyModifier)int.Parse(x.Attribute("key1").Value) : VirtualKeyModifier.None,
                    Key2 = x.Attribute("key2") != null && !string.IsNullOrEmpty(x.Attribute("key2").Value) ? (VirtualKeyModifier)int.Parse(x.Attribute("key2").Value) : VirtualKeyModifier.None,
                    Key3 = x.Attribute("key3") != null && !string.IsNullOrEmpty(x.Attribute("key3").Value) ? (VirtualKey)int.Parse(x.Attribute("key3").Value) : VirtualKey.None,
                    Type = x.Attribute("type") != null && !string.IsNullOrEmpty(x.Attribute("type").Value) ? (MenuItemType)Enum.Parse(typeof(MenuItemType), x.Attribute("type").Value, true) : MenuItemType.Item
                })
                .ToList();

            settings.MenuItems.StartProgramItems = document
                .XPathSelectElements("/smartSystemMenu/menuItems/startProgramItems/item")
                .Select(x => new StartProgramMenuItem
                {
                    Title = x.Attribute("title") != null ? x.Attribute("title").Value : string.Empty,
                    FileName = x.Attribute("fileName") != null ? x.Attribute("fileName").Value : string.Empty,
                    Arguments = x.Attribute("arguments") != null ? x.Attribute("arguments").Value : string.Empty,
                    BeginParameter = x.Attribute("beginParameter") != null ? x.Attribute("beginParameter").Value : string.Empty,
                    EndParameter = x.Attribute("endParameter") != null ? x.Attribute("endParameter").Value : string.Empty,
                    RunAs = x.Attribute("runAs") != null && !string.IsNullOrEmpty(x.Attribute("runAs").Value) ? (UserType)Enum.Parse(typeof(UserType), x.Attribute("runAs").Value, true) : UserType.Normal,
                    ShowWindow = x.Attribute("showWindow") == null || string.IsNullOrEmpty(x.Attribute("showWindow").Value) || x.Attribute("showWindow").Value.ToLower() == "true",
                    UseWindowWorkingDirectory = x.Attribute("useWindowWorkingDirectory") != null && !string.IsNullOrEmpty(x.Attribute("useWindowWorkingDirectory").Value) && x.Attribute("useWindowWorkingDirectory").Value.ToLower() == "true",
                    Type = x.Attribute("type") != null && !string.IsNullOrEmpty(x.Attribute("type").Value) ? (MenuItemType)Enum.Parse(typeof(MenuItemType), x.Attribute("type").Value, true) : MenuItemType.Item
                })
                .ToList();

            settings.MenuItems.Items = document
                .XPathSelectElements("/smartSystemMenu/menuItems/items/item")
                .Select(x => {
                    var menuItem = new MenuItem
                    {
                        Name = x.Attribute("name") != null ? x.Attribute("name").Value : string.Empty,
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
                        Name = y.Attribute("name") != null ? y.Attribute("name").Value : string.Empty,
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

            var dimmerElement = document.XPathSelectElement("/smartSystemMenu/dimmer");
            settings.Dimmer.Color = dimmerElement.Attribute("color") != null ? dimmerElement.Attribute("color").Value : string.Empty;
            settings.Dimmer.Transparency = dimmerElement.Attribute("transparency") != null ? int.Parse(dimmerElement.Attribute("transparency").Value) : 0;

            var sizerElement = document.XPathSelectElement("/smartSystemMenu/sizer");
            settings.Sizer.SizerType = sizerElement.Attribute("type") != null && !string.IsNullOrEmpty(sizerElement.Attribute("type").Value) ? (WindowSizerType)int.Parse(sizerElement.Attribute("type").Value) : WindowSizerType.WindowWithMargins;
            settings.Sizer.ResizableByDefault = sizerElement.Attribute("resizableByDefault") != null && !string.IsNullOrEmpty(sizerElement.Attribute("resizableByDefault").Value) && sizerElement.Attribute("resizableByDefault").Value.ToLower() == "true";

            var saveSelectedItemsElement = document.XPathSelectElement("/smartSystemMenu/saveSelectedItems");
            settings.SaveSelectedItems.AeroGlass = saveSelectedItemsElement.Attribute("aeroGlass") != null && !string.IsNullOrEmpty(saveSelectedItemsElement.Attribute("aeroGlass").Value) ? saveSelectedItemsElement.Attribute("aeroGlass").Value.ToLower() == "true" : true;
            settings.SaveSelectedItems.AlwaysOnTop = saveSelectedItemsElement.Attribute("alwaysOnTop") != null && !string.IsNullOrEmpty(saveSelectedItemsElement.Attribute("alwaysOnTop").Value) ? saveSelectedItemsElement.Attribute("alwaysOnTop").Value.ToLower() == "true" : true;
            settings.SaveSelectedItems.HideForAltTab = saveSelectedItemsElement.Attribute("hideForAltTab") != null && !string.IsNullOrEmpty(saveSelectedItemsElement.Attribute("hideForAltTab").Value) ? saveSelectedItemsElement.Attribute("hideForAltTab").Value.ToLower() == "true" : true;
            settings.SaveSelectedItems.Resizable = saveSelectedItemsElement.Attribute("resizable") != null && !string.IsNullOrEmpty(saveSelectedItemsElement.Attribute("resizable").Value) ? saveSelectedItemsElement.Attribute("resizable").Value.ToLower() == "true" : true;
            settings.SaveSelectedItems.Alignment = saveSelectedItemsElement.Attribute("alignment") != null && !string.IsNullOrEmpty(saveSelectedItemsElement.Attribute("alignment").Value) ? saveSelectedItemsElement.Attribute("alignment").Value.ToLower() == "true" : true;
            settings.SaveSelectedItems.Transparency = saveSelectedItemsElement.Attribute("transparency") != null && !string.IsNullOrEmpty(saveSelectedItemsElement.Attribute("transparency").Value) ? saveSelectedItemsElement.Attribute("transparency").Value.ToLower() == "true" : true;
            settings.SaveSelectedItems.Priority = saveSelectedItemsElement.Attribute("priority") != null && !string.IsNullOrEmpty(saveSelectedItemsElement.Attribute("priority").Value) ? saveSelectedItemsElement.Attribute("priority").Value.ToLower() == "true" : true;
            settings.SaveSelectedItems.MinimizeToTrayAlways = saveSelectedItemsElement.Attribute("minimizeToTrayAlways") != null && !string.IsNullOrEmpty(saveSelectedItemsElement.Attribute("minimizeToTrayAlways").Value) ? saveSelectedItemsElement.Attribute("minimizeToTrayAlways").Value.ToLower() == "true" : true;
            settings.SaveSelectedItems.Buttons = saveSelectedItemsElement.Attribute("buttons") != null && !string.IsNullOrEmpty(saveSelectedItemsElement.Attribute("buttons").Value) ? saveSelectedItemsElement.Attribute("buttons").Value.ToLower() == "true" : true;

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

            var cultureName = Thread.CurrentThread.CurrentUICulture.Name;
            var languageElement = document.XPathSelectElement("/smartSystemMenu/language");
            var languageName = languageElement != null && languageElement.Attribute("name") != null && !string.IsNullOrWhiteSpace(languageElement.Attribute("name").Value) ?
                languageElement.Attribute("name").Value.ToLower().Trim() :
                cultureName switch
                {
                    "zh-CN" => "zh_cn",
                    "zh-TW" => "zh_tw",
                    "ja-JP" => "ja",
                    "ru-RU" => "ru",
                    "de-DE" => "de",
                    "fr-FR" => "fr",
                    "hu-HU" => "hu",
                    "he-IL" => "he",
                    "es-ES" => "es",
                    "ko-KR" or "ko-KP" => "ko",
                    "pt-BR" or "pt-PT" => "pt",
                    "it-IT" or "it-SM" or "it-CH" or "it-VA" => "it",
                    "sr-Cyrl" or "sr-Cyrl-BA" or "sr-Cyrl-ME" or "sr-Cyrl-RS" or "sr-Cyrl-CS" => "sr",
                    _ => "en"
                };

            settings.LanguageName = languageName;
            settings.Language.Items = languageDocument
                .XPathSelectElements($"/language/items/{languageName}/item")
                .Select(x => new
                {
                    Name = x.Attribute("name") != null ? x.Attribute("name").Value : string.Empty,
                    Value = x.Attribute("value") != null ? x.Attribute("value").Value : string.Empty,
                })
                 .ToDictionary(x => x.Name, y => y.Value, StringComparer.OrdinalIgnoreCase);

            return settings;
        }

        public static void Save(string fileName, ApplicationSettings settings)
        {
            var document = new XDocument();
            document.Add(new XElement("smartSystemMenu",
                                 new XElement("processExclusions", settings.ExcludedProcessItems.Select(x => new XElement("processName",
                                 x.IgnoreHook ? new XAttribute("ignoreHook", x.IgnoreHook.ToString().ToLower()) : null, x.Name))),
                                 new XElement("createMenuOnInitEvent", settings.InitEventProcessNames.Select(x => new XElement("processName", x))),
                                 new XElement("noRestoreMenuOnExit", settings.NoRestoreMenuProcessNames.Select(x => new XElement("processName", x))),
                                 new XElement("menuItems",
                                     new XElement("items", settings.MenuItems.Items.Select(x => new XElement("item",
                                         new XAttribute("type", x.Type.ToString().ToLower()),
                                         x.Type == MenuItemType.Item || x.Type == MenuItemType.Group ? new XAttribute("name", x.Name) : null,
                                         x.Show == false ? new XAttribute("show", x.Show.ToString().ToLower()) : null,
                                         x.Type == MenuItemType.Item ? new XAttribute("key1", x.Key1 == VirtualKeyModifier.None ? string.Empty : ((int)x.Key1).ToString()) : null,
                                         x.Type == MenuItemType.Item ? new XAttribute("key2", x.Key2 == VirtualKeyModifier.None ? string.Empty : ((int)x.Key2).ToString()) : null,
                                         x.Type == MenuItemType.Item ? new XAttribute("key3", x.Key3 == VirtualKey.None ? string.Empty : ((int)x.Key3).ToString()) : null,
                                         x.Items.Any() ?
                                            new XElement("items", x.Items.Select(y => new XElement("item",
                                            new XAttribute("type", y.Type.ToString().ToLower()),
                                            y.Type == MenuItemType.Item || y.Type == MenuItemType.Group ? new XAttribute("name", y.Name) : null,
                                            y.Show == false ? new XAttribute("show", y.Show.ToString().ToLower()) : null,
                                            y.Type == MenuItemType.Item ? new XAttribute("key1", y.Key1 == VirtualKeyModifier.None ? string.Empty : ((int)y.Key1).ToString()) : null,
                                            y.Type == MenuItemType.Item ? new XAttribute("key2", y.Key2 == VirtualKeyModifier.None ? string.Empty : ((int)y.Key2).ToString()) : null,
                                            y.Type == MenuItemType.Item ? new XAttribute("key3", y.Key3 == VirtualKey.None ? string.Empty : ((int)y.Key3).ToString()) : null))) : null))),
                                     new XElement("windowSizeItems", settings.MenuItems.WindowSizeItems.Select(x => new XElement("item",
                                         x.Type == MenuItemType.Separator ? new XAttribute("type", x.Type.ToString().ToLower()) : null,
                                         x.Type == MenuItemType.Item ? new XAttribute("title", x.Title) : null,
                                         x.Type == MenuItemType.Item ? new XAttribute("left", x.Left == null ? string.Empty : x.Left.Value.ToString()) : null,
                                         x.Type == MenuItemType.Item ? new XAttribute("top", x.Top == null ? string.Empty : x.Top.Value.ToString()) : null,
                                         x.Type == MenuItemType.Item ? new XAttribute("width", x.Width == null ? string.Empty : x.Width.ToString()) : null,
                                         x.Type == MenuItemType.Item ? new XAttribute("height", x.Height == null ? string.Empty : x.Height.ToString()) : null,
                                         x.Type == MenuItemType.Item ? new XAttribute("key1", x.Key1 == VirtualKeyModifier.None ? string.Empty : ((int)x.Key1).ToString()) : null,
                                         x.Type == MenuItemType.Item ? new XAttribute("key2", x.Key2 == VirtualKeyModifier.None ? string.Empty : ((int)x.Key2).ToString()) : null,
                                         x.Type == MenuItemType.Item ? new XAttribute("key3", x.Key3 == VirtualKey.None ? string.Empty : ((int)x.Key3).ToString()) : null))),
                                     new XElement("startProgramItems", settings.MenuItems.StartProgramItems.Select(x => new XElement("item",
                                         x.Type == MenuItemType.Separator ? new XAttribute("type", x.Type.ToString().ToLower()) : null,
                                         x.Type == MenuItemType.Item ? new XAttribute("title", x.Title) : null,
                                         x.Type == MenuItemType.Item ? new XAttribute("fileName", x.FileName) : null,
                                         x.Type == MenuItemType.Item ? new XAttribute("arguments", x.Arguments) : null,
                                         x.Type == MenuItemType.Item ? new XAttribute("useWindowWorkingDirectory", x.UseWindowWorkingDirectory.ToString().ToLower()) : null,
                                         x.Type == MenuItemType.Item ? new XAttribute("runAs", x.RunAs.ToString().ToLower()) : null,
                                         x.Type == MenuItemType.Item ? new XAttribute("showWindow", x.ShowWindow.ToString().ToLower()) : null,
                                         x.Type == MenuItemType.Item ? new XAttribute("beginParameter", x.BeginParameter) : null,
                                         x.Type == MenuItemType.Item ? new XAttribute("endParameter", x.EndParameter) : null)))),
                                 new XElement("closer",
                                     new XAttribute("type", ((int)settings.Closer.Type).ToString()),
                                     new XAttribute("key1", settings.Closer.Key1 == VirtualKeyModifier.None ? string.Empty : ((int)settings.Closer.Key1).ToString()),
                                     new XAttribute("key2", settings.Closer.Key2 == VirtualKeyModifier.None ? string.Empty : ((int)settings.Closer.Key2).ToString()),
                                     new XAttribute("mouseButton", settings.Closer.MouseButton == MouseButton.None ? string.Empty : ((int)settings.Closer.MouseButton).ToString())
                                 ),
                                 new XElement("dimmer",
                                     new XAttribute("color", settings.Dimmer.Color),
                                     new XAttribute("transparency", settings.Dimmer.Transparency.ToString())
                                 ),
                                 new XElement("sizer",
                                     new XAttribute("type", ((int)settings.Sizer.SizerType).ToString()),
                                     new XAttribute("resizableByDefault", settings.Sizer.ResizableByDefault.ToString().ToLower())
                                 ),
                                 new XElement("saveSelectedItems",
                                     new XAttribute("aeroGlass", settings.SaveSelectedItems.AeroGlass.ToString().ToLower()),
                                     new XAttribute("alwaysOnTop", settings.SaveSelectedItems.AlwaysOnTop.ToString().ToLower()),
                                     new XAttribute("hideForAltTab", settings.SaveSelectedItems.HideForAltTab.ToString().ToLower()),
                                     new XAttribute("resizable", settings.SaveSelectedItems.Resizable.ToString().ToLower()),
                                     new XAttribute("alignment", settings.SaveSelectedItems.Alignment.ToString().ToLower()),
                                     new XAttribute("transparency", settings.SaveSelectedItems.Transparency.ToString().ToLower()),
                                     new XAttribute("priority", settings.SaveSelectedItems.Priority.ToString().ToLower()),
                                     new XAttribute("minimizeToTrayAlways", settings.SaveSelectedItems.MinimizeToTrayAlways.ToString().ToLower()),
                                     new XAttribute("buttons", settings.SaveSelectedItems.Buttons.ToString().ToLower())
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
