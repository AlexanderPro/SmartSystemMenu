using System;
using System.Collections.Generic;

namespace SmartSystemMenu.Settings
{
    public class ApplicationSettings : ICloneable
    {
        public IList<string> ExcludedProcessNames { get; set; }

        public IList<string> InitEventProcessNames { get; set; }

        public IList<string> NoRestoreMenuProcessNames { get; set; }

        public MenuItems MenuItems { get; set; }

        public CloserSettings Closer { get; set; }

        public DimmerSettings Dimmer { get; set; }

        public SizerSettings Sizer { get; set; }

        public SaveSelectedItemsSettings SaveSelectedItems { get; set; }

        public bool ShowSystemTrayIcon { get; set; }

        public bool EnableHighDPI { get; set; }

        public string LanguageName { get; set; }

        public LanguageSettings Language { get; set; }


        public ApplicationSettings()
        {
            ExcludedProcessNames = new List<string>();
            InitEventProcessNames = new List<string>();
            NoRestoreMenuProcessNames = new List<string>();
            MenuItems = new MenuItems();
            Closer = new CloserSettings();
            Dimmer = new DimmerSettings();
            Sizer = new SizerSettings();
            SaveSelectedItems = new SaveSelectedItemsSettings();
            ShowSystemTrayIcon = true;
            EnableHighDPI = false;
            LanguageName = "";
            Language = new LanguageSettings();
        }

        public object Clone()
        {
            var settings = new ApplicationSettings();

            foreach (var processName in ExcludedProcessNames)
            {
                settings.ExcludedProcessNames.Add(processName);
            }

            foreach (var processName in InitEventProcessNames)
            {
                settings.InitEventProcessNames.Add(processName);
            }

            foreach (var processName in NoRestoreMenuProcessNames)
            {
                settings.NoRestoreMenuProcessNames.Add(processName);
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

            foreach (var languageItemKey in Language.Items.Keys)
            {
                settings.Language.Items.Add(languageItemKey, Language.Items[languageItemKey]);
            }

            settings.Closer= (CloserSettings)Closer.Clone();
            settings.Dimmer = (DimmerSettings)Dimmer.Clone();
            settings.Sizer = (SizerSettings)Sizer.Clone();
            settings.SaveSelectedItems = (SaveSelectedItemsSettings)SaveSelectedItems.Clone();
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

            return Equals(other as ApplicationSettings);
        }

        public bool Equals(ApplicationSettings other)
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

            if (ExcludedProcessNames.Count != other.ExcludedProcessNames.Count)
            {
                return false;
            }

            if (InitEventProcessNames.Count != other.InitEventProcessNames.Count)
            {
                return false;
            }

            if (NoRestoreMenuProcessNames.Count != other.NoRestoreMenuProcessNames.Count)
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

            for (var i = 0; i < ExcludedProcessNames.Count; i++)
            {
                if (string.Compare(ExcludedProcessNames[i], other.ExcludedProcessNames[i], StringComparison.CurrentCultureIgnoreCase) != 0)
                {
                    return false;
                }
            }

            for (var i = 0; i < InitEventProcessNames.Count; i++)
            {
                if (string.Compare(InitEventProcessNames[i], other.InitEventProcessNames[i], StringComparison.CurrentCultureIgnoreCase) != 0)
                {
                    return false;
                }
            }

            for (var i = 0; i < NoRestoreMenuProcessNames.Count; i++)
            {
                if (string.Compare(NoRestoreMenuProcessNames[i], other.NoRestoreMenuProcessNames[i], StringComparison.CurrentCultureIgnoreCase) != 0)
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

            if (string.Compare(Dimmer.Color, other.Dimmer.Color, StringComparison.CurrentCultureIgnoreCase) != 0 || Dimmer.Transparency != other.Dimmer.Transparency)
            {
                return false;
            }

            if (Sizer.SizerType != other.Sizer.SizerType || Sizer.ResizableByDefault != other.Sizer.ResizableByDefault)
            {
                return false;
            }

            if (SaveSelectedItems.AeroGlass != other.SaveSelectedItems.AeroGlass ||
                SaveSelectedItems.AlwaysOnTop != other.SaveSelectedItems.AlwaysOnTop ||
                SaveSelectedItems.HideForAltTab != other.SaveSelectedItems.HideForAltTab ||
                SaveSelectedItems.Resizable != other.SaveSelectedItems.Resizable ||
                SaveSelectedItems.Alignment != other.SaveSelectedItems.Alignment ||
                SaveSelectedItems.Transparency != other.SaveSelectedItems.Transparency ||
                SaveSelectedItems.Priority != other.SaveSelectedItems.Priority ||
                SaveSelectedItems.MinimizeToTrayAlways != other.SaveSelectedItems.MinimizeToTrayAlways ||
                SaveSelectedItems.Buttons != other.SaveSelectedItems.Buttons)
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

            foreach (var processName in ExcludedProcessNames)
            {
                hashCode ^= processName.GetHashCode();
            }

            foreach (var processName in InitEventProcessNames)
            {
                hashCode ^= processName.GetHashCode();
            }

            foreach (var processName in NoRestoreMenuProcessNames)
            {
                hashCode ^= processName.GetHashCode();
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
            hashCode ^= Dimmer.Color.GetHashCode();
            hashCode ^= Dimmer.Transparency.GetHashCode();
            hashCode ^= Sizer.SizerType.GetHashCode();
            hashCode ^= Sizer.ResizableByDefault.GetHashCode();
            hashCode ^= SaveSelectedItems.AeroGlass.GetHashCode();
            hashCode ^= SaveSelectedItems.AlwaysOnTop.GetHashCode();
            hashCode ^= SaveSelectedItems.HideForAltTab.GetHashCode();
            hashCode ^= SaveSelectedItems.Resizable.GetHashCode();
            hashCode ^= SaveSelectedItems.Alignment.GetHashCode();
            hashCode ^= SaveSelectedItems.Transparency.GetHashCode();
            hashCode ^= SaveSelectedItems.Priority.GetHashCode();
            hashCode ^= SaveSelectedItems.MinimizeToTrayAlways.GetHashCode();
            hashCode ^= SaveSelectedItems.Buttons.GetHashCode();
            hashCode ^= LanguageName.GetHashCode();
            hashCode ^= ShowSystemTrayIcon.GetHashCode();
            hashCode ^= EnableHighDPI.GetHashCode();
            return hashCode;
        }
    }
}
