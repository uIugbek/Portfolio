using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace PortfolioBackend.Core.DAL
{
    [Table("Users")]
    [DisplayName("Пользователи")]
    public class User : SimpleBaseEntity
    {
        public User()
        {
            UserInRoles = new HashSet<UserInRole>();
        }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        //[Required]
        [MaxLength(50)]
        //[Index(IsUnique = true)]
        public string Login { get; set; }
        //[Required]
        [MaxLength(50)]
        public string PasswordSalt { get; set; }
        //[Required]
        [MaxLength(50)]
        public string PasswordHash { get; set; }
        //[Required]
        [MaxLength(50)]
        //[Index(IsUnique = true)]
        public string Email { get; set; }
        [MaxLength(50)]
        public string Photo { get; set; }
        public bool IsBlocked { get; set; }

        public virtual ICollection<UserInRole> UserInRoles { get; set; }
        
    }

    [Table("UserInRoles")]
    public class UserInRole : BaseEntity
    {
        public int RoleId { get; set; }
        public int UserId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}