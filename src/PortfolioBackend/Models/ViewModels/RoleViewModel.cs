using PortfolioBackend.Core.DAL;
using PortfolioBackend.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using PortfolioBackend.Core.Attributes;
using System.ComponentModel.DataAnnotations;
using PortfolioBackend.Globalization;
using Microsoft.EntityFrameworkCore.LazyLoading;
using PortfolioBackend.Core.BLL.Services;

namespace PortfolioBackend.Models.ViewModels
{
    public class RoleViewModel : LocalizableModel<Role, Role_Locale, Role_LocaleViewModel>
    {
        public RoleViewModel()
        {
            RoleInPermissions = new List<RoleInPermission>();
        }

        public string Name { get; set; }

        public IList<RoleInPermission> RoleInPermissions { get; set; }

        public override void LoadEntity(Role t)
        {
            using (var dbContext = new PostgresDbContext())//ApplicationDbContext
            {
                foreach (var p in dbContext.Permissions)
                {
                    if (t.RoleInPermissions.Any(a => a.PermissionId == p.Id))
                        continue;
                    t.RoleInPermissions.Add(new RoleInPermission
                    {
                        PermissionId = p.Id,
                        Permission = p,
                        IsAccessible = false,
                        RoleId = t.Id
                    });
                } 
            }
            base.LoadEntity(t);
            Name = t.GetCurrentLocale().Name;
        }
    }

    public class Role_LocaleViewModel : Localizable_LocaleModel<Role_Locale>
    {
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}
