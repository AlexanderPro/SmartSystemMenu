using System.Collections.Generic;

namespace SmartSystemMenu.Settings
{
    /// <summary>
    /// Add language class
    /// 2020.3.25
    /// pana
    /// </summary>
    public class MenuLanguage
    {
        public IList<MenuTitleString> MenuTitleString { get; set; }

        public MenuLanguage()
        {
            MenuTitleString = new List<MenuTitleString>();
        }

        public string GetStringValue(string title_stirng)
        {
            for (int i = 0; i < MenuTitleString.Count; i++)
            {
                if (title_stirng == MenuTitleString[i].Title)
                {
                    return MenuTitleString[i].StringValue;
                }
            }

            return "";
        }
    }
}
