﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using SmartSystemMenu.Utils;
using SmartSystemMenu.Native.Enums;

namespace SmartSystemMenu.Settings
{
    public class WindowSettings
    {
        public List<WindowState> Items { get; set; }

        public WindowSettings()
        {
            Items = new List<WindowState>();
        }

        public IList<WindowState> Find(string className, string processName)
        {
            var items = Items
                .Where(x => string.Compare(x.ClassName, className, StringComparison.CurrentCulture) == 0 && string.Compare(x.ProcessName, processName, StringComparison.CurrentCultureIgnoreCase) == 0)
                .ToList();
            return items;
        }

        public IList<WindowState> Find(string className)
        {
            var items = Items
                .Where(x => string.Compare(x.ClassName, className, StringComparison.CurrentCulture) == 0)
                .ToList();
            return items;
        }

        public void Remove(string className, string processName)
        {
            Items.RemoveAll(x => string.Compare(x.ClassName, className, StringComparison.CurrentCulture) == 0 && string.Compare(x.ProcessName, processName, StringComparison.CurrentCultureIgnoreCase) == 0);
        }

        public static WindowSettings Read(string fileName)
        {
            var settings = new WindowSettings();
            var document = XDocument.Load(fileName);
            settings.Items = document
                .XPathSelectElements("/windows/window")
                .Select(x => {
                    var positionElement = x.XPathSelectElement("./position");
                    var systemMenuElement = x.XPathSelectElement("./systemMenu");
                    return new WindowState
                    {
                        ProcessName = x.Attribute("processName").Value,
                        ClassName = x.Attribute("className").Value,
                        Left = int.Parse(positionElement.Attribute("left").Value),
                        Top = int.Parse(positionElement.Attribute("top").Value),
                        Width = int.Parse(positionElement.Attribute("width").Value),
                        Height = int.Parse(positionElement.Attribute("height").Value),
                        AeroGlass = systemMenuElement.Attribute("aeroGlass") == null ? (bool?)null : systemMenuElement.Attribute("aeroGlass").Value.ToLower() == "true",
                        AlwaysOnTop = systemMenuElement.Attribute("alwaysOnTop") == null ? (bool?)null : systemMenuElement.Attribute("alwaysOnTop").Value.ToLower() == "true",
                        HideForAltTab = systemMenuElement.Attribute("hideForAltTab") == null ? (bool?)null : systemMenuElement.Attribute("hideForAltTab").Value.ToLower() == "true",
                        Alignment = systemMenuElement.Attribute("alignment") == null ? (WindowAlignment?)null : (WindowAlignment)int.Parse(systemMenuElement.Attribute("alignment").Value),
                        Transparency = systemMenuElement.Attribute("transparency") == null ? (int?)null : int.Parse(systemMenuElement.Attribute("transparency").Value),
                        Priority = systemMenuElement.Attribute("priority") == null ? (Priority?)null : (Priority)int.Parse(systemMenuElement.Attribute("priority").Value),
                        MinimizeToTrayAlways = systemMenuElement.Attribute("minimizeToTrayAlways") == null ? (bool?)null : systemMenuElement.Attribute("minimizeToTrayAlways").Value.ToLower() == "true"
                    };
                })
                .ToList();

            return settings;
        }

        public static void Save(string fileName, WindowSettings windowSettings, SmartSystemMenuSettings settings)
        {
            var document = new XDocument();
            document.Add(new XElement("windows", windowSettings.Items.Select(x => new XElement("window",
                                         new XAttribute("className", x.ClassName),
                                         new XAttribute("processName", x.ProcessName),
                                         new XElement("position",
                                         new XAttribute("left", x.Left),
                                         new XAttribute("top", x.Top),
                                         new XAttribute("width", x.Width),
                                         new XAttribute("height", x.Height)),
                                         new XElement("systemMenu",
                                         settings.SaveSelectedItems.AeroGlass && x.AeroGlass.HasValue ? new XAttribute("aeroGlass", x.AeroGlass.Value.ToString().ToLower()) : null,
                                         settings.SaveSelectedItems.AlwaysOnTop && x.AlwaysOnTop.HasValue ? new XAttribute("alwaysOnTop", x.AlwaysOnTop.Value.ToString().ToLower()) : null,
                                         settings.SaveSelectedItems.HideForAltTab && x.HideForAltTab.HasValue ? new XAttribute("hideForAltTab", x.HideForAltTab.Value.ToString().ToLower()) : null,
                                         settings.SaveSelectedItems.Alignment && x.Alignment.HasValue ? new XAttribute("alignment", (int)x.Alignment.Value) : null,
                                         settings.SaveSelectedItems.Transparency && x.Transparency.HasValue ? new XAttribute("transparency", x.Transparency.Value.ToString().ToLower()) : null,
                                         settings.SaveSelectedItems.Priority && x.Priority.HasValue ? new XAttribute("priority", (int)x.Priority) : null,
                                         settings.SaveSelectedItems.MinimizeToTrayAlways && x.MinimizeToTrayAlways.HasValue ? new XAttribute("minimizeToTrayAlways", x.MinimizeToTrayAlways.Value.ToString().ToLower()) : null)))));
            FileUtils.Save(fileName, document);
        }
    }
}
