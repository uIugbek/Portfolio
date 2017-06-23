using Microsoft.EntityFrameworkCore.LazyLoading;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace PortfolioBackend.Core.DAL
{
    [Table("Roles")]
    public class Role : BaseLocalizableEntity<Role, Role_Locale>
    {
        public Role()
        {
            this.RoleInPermissions = new List<RoleInPermission>();
            this.UserInRoles = new List<UserInRole>();
        }
        
        [InverseProperty("Role")]
        public virtual ICollection<RoleInPermission> RoleInPermissions { get; set; }
        public virtual ICollection<UserInRole> UserInRoles { get; set; }
    }

    [Table("Role_Locales")]
    public class Role_Locale : BaseLocalizableEntity_Locale<Role>
    {
        [MaxLength(1024)]
        public string Description { get; set; }
    }

    [Table("RoleInPermissions")]
    public class RoleInPermission : BaseEntity
    {
        //private LazyReference<Role> _role = new LazyReference<Role>();

        public int RoleId { get; set; }
        public int PermissionId { get; set; }
        public bool IsAccessible { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role
        {
            get;
            //{
            //    return _role.GetValue(this, nameof(Role));
            //}
            set;
            //{
            //    _role.SetValue(value);
            //}
        }
        [ForeignKey("PermissionId")]
        public virtual Permission Permission { get; set; }
    }
}