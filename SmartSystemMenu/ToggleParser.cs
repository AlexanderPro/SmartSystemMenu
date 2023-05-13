using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartSystemMenu
{
    class ToggleParser
    {
        private readonly Dictionary<string, string> toggles;

        public ToggleParser(string[] args)
        {
            toggles =
                args.Zip(args.Skip(1).Concat(new[] { string.Empty }), (first, second) => new { first, second })
                    .Where(pair => IsToggle(pair.first))
                    .ToDictionary(pair => RemovePrefix(pair.first).ToLowerInvariant(), g => IsToggle(g.second) ? string.Empty : g.second);
        }

        private static string RemovePrefix(string toggle) => new string(toggle.SkipWhile(c => c == '-').ToArray());

        private static bool IsToggle(string arg) => arg.StartsWith("-", StringComparison.InvariantCulture);

        public bool HasToggle(string toggle) => toggles.ContainsKey(toggle.ToLowerInvariant());

        public string GetToggleValueOrDefault(string toggle, string defaultValue) => toggles.TryGetValue(toggle.ToLowerInvariant(), out var value) ? value : defaultValue;
    }
}
