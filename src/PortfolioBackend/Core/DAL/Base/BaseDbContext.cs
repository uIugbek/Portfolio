using PortfolioBackend.Configurations;
using PortfolioBackend.Core.Security;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioBackend.Core.DAL
{
    public class BaseDbContext : DbContext
    {
        public BaseDbContext()
        {
        }

        public BaseDbContext(DbContextOptions options)
            : base(options)
        {
        }

        private IMembershipService _membershipService;
        public IMembershipService MembershipService
        {
            get
            {
                if (_membershipService == null)
                    _membershipService = AppDependencyResolver.Current.GetService<IMembershipService>();
                return _membershipService;
            }
        }

        public virtual object CurrentUserId
        {
            get
            {
                if (MembershipService != null && MembershipService.IsAutenticated)
                    return MembershipService.UserId;
                return 1;
            }
        }

        public override int SaveChanges()
        {
            if (EntityProperties.UseDefault)
                SetDafaultProperties();

            return base.SaveChanges();
        }

        private void SetDafaultProperties()
        {
            DateTime now = DateTime.Now;
            var userId = CurrentUserId;
            var defaultPropertiesAutoTracker = DefaultPropertiesAutoTracker.Instance;

            foreach (var entityState in ChangeTracker.Entries()
                                                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
            {
                if (entityState.State == EntityState.Added)
                {
                    var id = entityState.Property("Id");
                    if (id.CurrentValue is Guid)
                        id.CurrentValue = Guid.NewGuid();
                }

                defaultPropertiesAutoTracker.Track(entityState.Entity, entityState.State, now, userId);
            }
        }
    }
}
