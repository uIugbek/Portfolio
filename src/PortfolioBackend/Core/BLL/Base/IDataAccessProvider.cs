using PortfolioBackend.Core.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioBackend.Core.BLL.Base
{
    public interface IDataAccessProvider
    {
        void AddUser(User userRecord);
        void UpdateUser(long userId, User user);
        void DeleteUser(long userId);
        User GetUser(long userId);
        List<User> GetUsers();
    }
}
