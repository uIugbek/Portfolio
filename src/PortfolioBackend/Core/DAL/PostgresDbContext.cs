using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioBackend.Core.DAL
{
    public class PostgresDbContext : BaseDbContext
    {
        public PostgresDbContext()
        {
        }

        public PostgresDbContext(DbContextOptions<PostgresDbContext> options) :base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Role_Locale> Role_Locales { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Permission_Locale> Permission_Locales { get; set; }
        public DbSet<UserInRole> UserInRoles { get; set; }
        public DbSet<RoleInPermission> RoleInPermissions { get; set; }
        public DbSet<Culture> Cultures { get; set; }
        public DbSet<LocalizedString> LocalizedStrings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Startup.Configuration.GetConnectionString("Portfolio"));

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                   .HasIndex(a => new { a.Login, a.Email })
                   .IsUnique();

            builder.Entity<User>()
                   .HasKey(m => m.Id);

            builder.Entity<Culture>()
                   .Property(a => a.Id)
                   .ValueGeneratedNever();

            base.OnModelCreating(builder);
        }
    }
}
