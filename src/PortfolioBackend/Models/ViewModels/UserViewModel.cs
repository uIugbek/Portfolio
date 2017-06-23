using PortfolioBackend.Core.DAL;
using PortfolioBackend.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioBackend.Models.ViewModels
{
    public class UserViewModel : Model<User>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; }
        public bool IsBlocked { get; set; }
    }
}
