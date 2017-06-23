using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioBackend.Core.DAL
{
    public static class DbInitializer
    {
        //public static void Initialize(ApplicationDbContext context)
        //{
        //    context.Database.EnsureCreated();
            
        //    if (!context.Cultures.Any())
        //        foreach (var culture in SiteSettings.Cultures)
        //            context.Cultures.Add(culture);
        //    context.SaveChanges();

        //    if (!context.Users.Any())
        //        context.Users.Add(new User
        //        {
        //            Name = "Admin",
        //            Login = "admin",
        //            Email = "admin@test.ru",
        //        });

        //    context.SaveChanges();
        //}

        public static void InitializePG(PostgresDbContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Cultures.Any())
                foreach (var culture in SiteSettings.Cultures)
                    context.Cultures.Add(culture);
            context.SaveChanges();

            if (!context.Users.Any())
                context.Users.Add(new User
                {
                    Name = "Admin",
                    Login = "admin",
                    Email = "admin@test.ru",
                });

            context.SaveChanges();
        }
    }
}
