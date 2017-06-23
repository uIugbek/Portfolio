using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using PortfolioBackend.Globalization;
using PortfolioBackend.Core.Cultures;
using PortfolioBackend;

namespace PortfolioBackend
{
    public static class StringExtensions
    {
        public static string Localize(this string key)
        {
            if (string.IsNullOrEmpty(key))
                return string.Empty;
            return LocalizationDictionary.Current[key];
        }

        public static string Localize(this string key, int cultureId)
        {
            if (string.IsNullOrEmpty(key))
                return string.Empty;
            return LocalizationDictionary.Current.Get(key, cultureId);
        }

        public static string Localize(this string key, string defaultValue)
        {
            if (string.IsNullOrEmpty(key))
                return defaultValue;
            return LocalizationDictionary.Current[key];
        }

        public static IEnumerable<int> AsIntEnumerable(this string s)
        {
            return s.AsIntEnumerable(',');
        }

        public static IEnumerable<int> AsIntEnumerable(this string s, char separator)
        {
            string[] array = string.IsNullOrEmpty(s)
                             ? new string[] { }
                             : s.Split(new char[] { separator, ' ' }, StringSplitOptions.RemoveEmptyEntries);

            return array.Select(a => a.AsInt())
                        .Where(a => a.HasValue)
                        .Cast<int>();
        }

        public static TEnum AsEnum<TEnum>(this string value, TEnum defaultValue)
            where TEnum : struct
        {
            TEnum result;
            if (Enum.TryParse(value, out result))
                return result;
            return defaultValue;
        }

        public static TEnum? AsEnum<TEnum>(this string value)
            where TEnum : struct
        {
            TEnum result;
            if (Enum.TryParse(value, out result))
                return result;
            return null;
        }

        //public static string ToCapitalize(this string s)
        //{
        //    if (s == null)
        //        return default(string);
        //    return CultureHelper.CurrentCultureInfo.TextInfo.ToTitleCase(s);
        //}

        public static string PrependZero(this int value)
        {
            if (value > 0 && value < 10)
                return string.Format("{0}{1}", "0", value);

            return value.AsString();
        }
    }
}