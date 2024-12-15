using System.Collections.Generic;

namespace SmartSystemMenu.Settings
{
    public class LanguageSettings
    {
        public Dictionary<string, string> Items { get; set; } = new();

        public string GetValue(string name) => Items.TryGetValue(name, out var value) ? value : string.Empty;
    }
}
