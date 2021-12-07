using System;
using System.Collections.Generic;

namespace SmartSystemMenu.Extensions
{
    static class StringExtensions
    {
        public static IList<string> GetParams(this string value, string begin, string end)
        {
            var result = new List<string>();

            if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(begin) || string.IsNullOrEmpty(end))
            {
                return result;
            }

            var startIndex = 0;
            var beginIndex = -1;
            while ((beginIndex = value.IndexOf(begin, startIndex, StringComparison.Ordinal)) >= 0)
            {
                if (beginIndex < 0)
                {
                    break;
                }

                startIndex = beginIndex + 1;
                var endIndex = value.IndexOf(end, startIndex, StringComparison.Ordinal);

                if (endIndex < 0 || startIndex >= endIndex)
                {
                    break;
                }

                var parameter = value.Substring(beginIndex, endIndex - beginIndex + 1);
                if (!string.IsNullOrEmpty(parameter) && !result.Contains(parameter))
                {
                    result.Add(parameter);
                }

                startIndex = endIndex + 1;
            }
            return result;
        }

        public static string TrimStart(this string value, string trimString)
        {
            if (string.IsNullOrEmpty(trimString))
            {
                return value;
            }

            var result = value;
            while (result.StartsWith(trimString))
            {
                result = result.Substring(trimString.Length);
            }

            return result;
        }

        public static string TrimEnd(this string value, string trimString)
        {
            if (string.IsNullOrEmpty(trimString))
            {
                return value;
            }

            var result = value;
            while (result.EndsWith(trimString))
            {
                result = result.Substring(0, result.Length - trimString.Length);
            }

            return result;
        }
    }
}