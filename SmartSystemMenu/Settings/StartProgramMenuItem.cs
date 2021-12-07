using System;

namespace SmartSystemMenu.Settings
{
    public class StartProgramMenuItem : ICloneable
    {
        public string Title { get; set; }

        public string FileName { get; set; }

        public string Arguments { get; set; }

        public UserType RunAs { get; set; }

        public bool ShowWindow { get; set; }

        public string BeginParameter { get; set; }

        public string EndParameter { get; set; }

        public StartProgramMenuItem()
        {
            Title = string.Empty;
            FileName = string.Empty;
            Arguments = string.Empty;
            RunAs = UserType.Normal;
            ShowWindow = true;
            BeginParameter = string.Empty;
            EndParameter = string.Empty;
        }

        public object Clone()
        {
            var menuItemClone = (StartProgramMenuItem)MemberwiseClone();
            return menuItemClone;
        }
    }
}
