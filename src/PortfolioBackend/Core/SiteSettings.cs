using PortfolioBackend.Core.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioBackend.Core
{
    public static class SiteSettings
    {
        #region Constants

        public static IList<Culture> Cultures
        {
            get
            {
                return new List<Culture>()
                {
                    new Culture() { Id = 1, Code = "uz", Name = "Uzbek"},
                    new Culture() { Id = 2, Code = "ru", Name = "Russian"},
                    new Culture() { Id = 3, Code = "en", Name = "English"}
                };
            }
        }

        #endregion
    }
}
