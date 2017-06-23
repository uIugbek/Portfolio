using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace PortfolioBackend.Core.DAL
{
    [Table("Permissions")]
    public class Permission : BaseLocalizableEntity<Permission, Permission_Locale>
    {
        public Permission()
        {
            this.RoleInPermissions = new List<RoleInPermission>();
        }
        
        public int Code { get; set; }
        public virtual ICollection<RoleInPermission> RoleInPermissions { get; set; }

    }

    [Table("Permission_Locales")]
    public class Permission_Locale : BaseLocalizableEntity_Locale<Permission>
    {
        [MaxLength(1023)]
        public string Description { get; set; }
    }
}