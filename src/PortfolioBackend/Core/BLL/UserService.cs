using PortfolioBackend.Configurations;
using PortfolioBackend.Core.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioBackend.Core.BLL.Services
{
    public class UserService : SimpleBaseService<User>
    {
        public static UserService Instance
        {
            get
            {
                return AppDependencyResolver.Current.GetService<UserService>();
            }
        }
    }
}
