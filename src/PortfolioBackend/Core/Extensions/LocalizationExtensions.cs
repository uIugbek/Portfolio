using PortfolioBackend.Core.DAL;
using PortfolioBackend.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PortfolioBackend
{
    public static class LocalizationExtensions
    {
        public static TLocale GetLocale<TLocale>(this ILocalizable<TLocale> localizable, int cultureId)
            where TLocale : class, ILocale
        {
            return localizable != null ? localizable.Localizations.SingleOrDefault(a => a.CultureId == cultureId) : Activator.CreateInstance<TLocale>();
        }

        public static TLocale GetLocale<TLocale>(this ILocalizable<TLocale> localizable, string cultureName)
            where TLocale : class, ILocale
        {
            int cultureId = CultureHelper.Cultures.SingleOrDefault(a => a.Code == cultureName).Id;
            return localizable.GetLocale(cultureId);
        }

        public static TLocale GetCurrentLocale<TLocale>(this ILocalizable<TLocale> localizable)
            where TLocale : class, ILocale
        {
            return localizable.GetLocale(CultureHelper.CurrentCultureId);
        }
    }
}