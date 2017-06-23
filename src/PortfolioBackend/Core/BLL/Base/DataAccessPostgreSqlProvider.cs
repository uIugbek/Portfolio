using Microsoft.Extensions.Logging;
using PortfolioBackend.Core.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioBackend.Core.BLL.Base
{
    public class DataAccessPostgreSqlProvider : IDataAccessProvider
    {
        private readonly PostgresDbContext _context;
        private readonly ILogger _logger;

        public DataAccessPostgreSqlProvider(PostgresDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("DataAccessPostgreSqlProvider");
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void UpdateUser(long userId, User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void DeleteUser(long userId)
        {
            var entity = _context.Users.First(t => t.Id == userId);
            _context.Users.Remove(entity);
            _context.SaveChanges();
        }

        public User GetUser(long userId)
        {
            return _context.Users.First(t => t.Id == userId);
        }

        public List<User> GetUsers()
        {
            // Using the shadow property EF.Property<DateTime>(user)
            return _context.Users.OrderByDescending(user => Microsoft.EntityFrameworkCore.EF.Property<DateTime>(user, "UpdatedTimestamp")).ToList();
        }
    }
}
