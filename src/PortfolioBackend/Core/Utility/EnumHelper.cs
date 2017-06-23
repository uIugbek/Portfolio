using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;

namespace PortfolioBackend.Core.Utility
{

    public class EnumHelper
    {
        public static IEnumerable<SelectListItem> ToSelectListItems(Type enumType, IEnumerable<int> selectedItems = null)
        {
            return selectedItems == null
                ? Enum.GetValues(enumType).Cast<int>().Select(e =>
                {
                    string name = Enum.GetName(enumType, e);
                    var descAttr = (DescriptionAttribute[])enumType.GetField(name).GetCustomAttributes(typeof(DescriptionAttribute), false);

                    return new SelectListItem
                    {
                        Value = e.ToString(),
                        Text = descAttr.Length > 0 ? descAttr[0].Description : name
                    };
                })
                : Enum.GetValues(enumType).Cast<int>().Select(e =>
                {
                    string name = Enum.GetName(enumType, e);
                    var descAttr = (DescriptionAttribute[])enumType.GetField(name).GetCustomAttributes(typeof(DescriptionAttribute), false);

                    return new SelectListItem
                    {
                        Selected = selectedItems.Contains(e),
                        Value = e.ToString(),
                        Text = (descAttr.Length > 0 ? descAttr[0].Description : name)
                    };
                });
        }

        public static IEnumerable<SelectListItem> ToLocalizedSelectListItems(Type enumType, IEnumerable<int> selectedItems = null)
        {
            return selectedItems == null
                ? Enum.GetValues(enumType).Cast<int>().Select(e =>
                    {
                        string name = Enum.GetName(enumType, e);
                        var descAttr = (DescriptionAttribute[])enumType.GetField(name).GetCustomAttributes(typeof(DescriptionAttribute), false);

                        return new SelectListItem
                        {
                            Value = e.ToString(),
                            Text = (descAttr.Length > 0 ? descAttr[0].Description : name).Localize()
                        };
                    })
                : Enum.GetValues(enumType).Cast<int>().Select(e =>
                    {
                        string name = Enum.GetName(enumType, e);
                        var descAttr = (DescriptionAttribute[])enumType.GetField(name).GetCustomAttributes(typeof(DescriptionAttribute), false);

                        return new SelectListItem
                        {
                            Selected = selectedItems.Contains(e),
                            Value = e.ToString(),
                            Text = (descAttr.Length > 0 ? descAttr[0].Description : name).Localize()
                        };
                    });
        }

        public static IDictionary<int, string> ToDictionary(Type enumType)
        {
            return Enum.GetValues(enumType).Cast<int>().ToDictionary(e => e, e =>
            {
                string name = Enum.GetName(enumType, e);
                var descAttr = (DescriptionAttribute[])enumType.GetField(name).GetCustomAttributes(typeof(DescriptionAttribute), false);

                return descAttr.Length > 0 ? descAttr[0].Description : name;
            });
        }

        public static IDictionary<int, string> ToLocalizedDictionary(Type enumType)
        {
            return Enum.GetValues(enumType).Cast<int>().ToDictionary(e => e, e =>
            {
                string name = Enum.GetName(enumType, e);
                var descAttr = (DescriptionAttribute[])enumType.GetField(name).GetCustomAttributes(typeof(DescriptionAttribute), false);

                return (descAttr.Length > 0 ? descAttr[0].Description : name).Localize();
            });
        }

        public static IEnumerable<object> ToLocalizedAnonymList(Type enumType, Func<string, string, object> mapper)
        {
            return Enum.GetValues(enumType).Cast<int>().Select(e =>
            {
                string name = Enum.GetName(enumType, e);
                var descAttr = (DescriptionAttribute[])enumType.GetField(name).GetCustomAttributes(typeof(DescriptionAttribute), false);

                return mapper(e.ToString(), (descAttr.Length > 0 ? descAttr[0].Description : name).Localize());
            });
        }

        public static IEnumerable<object> ToAnonymList(Type enumType, Func<string, string, object> mapper)
        {
            return Enum.GetValues(enumType).Cast<int>().Select(e =>
            {
                string name = Enum.GetName(enumType, e);
                var descAttr = (DescriptionAttribute[])enumType.GetField(name).GetCustomAttributes(typeof(DescriptionAttribute), false);

                return mapper(e.ToString(), (descAttr.Length > 0 ? descAttr[0].Description : name));
            });
        }

    }

    public class EnumHelper<TEnum>
        where TEnum : struct
    {

        public static SelectList ToSelectList()
        {
            return new SelectList(EnumHelper.ToSelectListItems(typeof(TEnum)), "Value", "Text");
        }

        public static IEnumerable<SelectListItem> ToSelectListItems(IEnumerable<int> selectedItems = null)
        {
            return EnumHelper.ToSelectListItems(typeof(TEnum), selectedItems);
        }

        public static IEnumerable<SelectListItem> ToLocalizedSelectListItems(IEnumerable<int> selectedItems = null)
        {
            return EnumHelper.ToLocalizedSelectListItems(typeof(TEnum), selectedItems);
        }

        public static IEnumerable<SelectListItem> ToSelectListItems(params TEnum[] selectedItems)
        {
            return EnumHelper<TEnum>.ToSelectListItems(selectedItems.Cast<int>());
        }

        public static IEnumerable<SelectListItem> ToLocalizedSelectListItems(params TEnum[] selectedItems)
        {
            return EnumHelper<TEnum>.ToLocalizedSelectListItems(selectedItems.Cast<int>());
        }

        public static SelectList ToLocalizedSelectList()
        {
            return new SelectList(EnumHelper.ToLocalizedSelectListItems(typeof(TEnum)), "Value", "Text");
        }

        public static IDictionary<int, string> ToDictionary()
        {
            return EnumHelper.ToDictionary(typeof(TEnum));
        }

        public static IDictionary<int, string> ToLocalizedDictionary()
        {
            return EnumHelper.ToLocalizedDictionary(typeof(TEnum));
        }

        public static IEnumerable<object> ToAnonymList(Func<string, string, object> mapper = null)
        {
            if (mapper == null)
                mapper = (a, b) => new { id = a, text = b };

            return EnumHelper.ToAnonymList(typeof(TEnum), mapper);
        }

        public static IEnumerable<object> ToLocalizedAnonymList(Func<string, string, object> mapper = null)
        {
            if (mapper == null)
                mapper = (a, b) => new { id = a, text = b };

            return EnumHelper.ToLocalizedAnonymList(typeof(TEnum), mapper);
        }

    }

}