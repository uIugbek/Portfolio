using PortfolioBackend.Core.DAL;
using PortfolioBackend.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using PortfolioBackend.Core.Attributes;
using System.ComponentModel.DataAnnotations;
using PortfolioBackend.Globalization;

namespace PortfolioBackend.Models.ViewModels
{
    public class PermissionViewModel : LocalizableModel<Permission, Permission_Locale, Permission_LocaleViewModel>
    {
        public string Name { get; set; }

        public int Code { get; set; }

        public override void LoadEntity(Permission t)
        {
            base.LoadEntity(t);
            Name = t.GetCurrentLocale().Name;
        }
    }

    public class Permission_LocaleViewModel : Localizable_LocaleModel<Permission_Locale>
    {
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}
