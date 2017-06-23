using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.AspNetCore.Routing;
using PortfolioBackend.Core;
using PortfolioBackend.Core.DAL;

namespace PortfolioBackend.Globalization
{
    public class CultureHelper
    {
        private static IList<Culture> _cultures;
        public static IList<Culture> Cultures
        {
            get
            {
                if (_cultures == null)
                    _cultures = SiteSettings.Cultures;//new Configurations.GlobalizationConfigurationSection().Cultures.Cast<Configurations.CultureElement>().ToDictionary(a => a.Code, a => a.Id);

                return _cultures;
            }
        }

        public static CultureInfo CurrentCultureInfo
        {
            get { var res = CultureInfo.DefaultThreadCurrentCulture; return res; }
        }

        public static string GetDefaultCulture()
        {
            return Cultures.OrderBy(a => a.Id).FirstOrDefault().Code;
        }

        private static string _currentCultureName;
        public static string CurrentCultureName
        {
            get
            {
                if (string.IsNullOrEmpty(_currentCultureName))
                    _currentCultureName = CurrentCultureInfo != null ? CurrentCultureInfo.Name : GetDefaultCulture();
                return _currentCultureName;
            }
        }

        private static int _currentCultureId = -1;
        public static int CurrentCultureId
        {
            get
            {
                if (_currentCultureId <= 0)
                {
                    if (Cultures.Any(a => a.Code.Contains(CurrentCultureName)))
                    {
                        var currentCulture = Cultures.FirstOrDefault(a => a.Code == CurrentCultureName);
                        _currentCultureId = currentCulture != null ? currentCulture.Id : 0;
                    }
                }
                return _currentCultureId;
            }
        }

        public static void ChangeCulture(RouteValueDictionary routeValues)
        {
            //Get culture 
            string cultureName = (routeValues["lang"] != null) ? routeValues["lang"].ToString() : GetDefaultCulture();

            // Modify current thread's culture           
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(cultureName);
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo(cultureName);

            _currentCultureName = null;
            _currentCultureId = -1;
        }
    }
}
