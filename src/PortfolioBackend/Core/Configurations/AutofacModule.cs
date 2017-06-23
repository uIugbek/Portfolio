using PortfolioBackend.Core.BLL.Services;
using PortfolioBackend.Core.Cultures;
using PortfolioBackend.Core.DAL;
using Autofac;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PortfolioBackend.Configurations
{
    public class AutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var currentAssembly = Assembly.GetEntryAssembly();

            builder.RegisterType<PostgresDbContext>()//ApplicationDbContext
                    .As<DbContext>()
                    .InstancePerRequest()
                    .InstancePerLifetimeScope();

            builder.RegisterType<DefaultPropertiesAutoTracker>()
                .As<DefaultPropertiesAutoTracker>()
                .InstancePerRequest()
                .InstancePerLifetimeScope();

            builder.Register(a => new UnitOfWork<DbContext>(a.Resolve<PostgresDbContext>()))//ApplicationDbContext
                   .As<IUnitOfWork<DbContext>>()
                   .InstancePerRequest()
                   .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(BaseRepository<>))
                    .As(typeof(IBaseRepository<>))
                    .InstancePerRequest()
                    .InstancePerLifetimeScope();

            //builder.RegisterType<MembershipService>()
            //        .As<IMembershipService>()
            //        .InstancePerRequest()
            //        .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(currentAssembly)
                 .InNamespace("PortfolioBackend.Core.BLL.Services")
                 .Where(t => t.Name.EndsWith("Service"))
                 .AsSelf()
                 .InstancePerRequest()
                 .InstancePerLifetimeScope();

            builder.RegisterType<LocalizationDictionary>()
                .As<ILocalizationDictionary>()
                .InstancePerRequest()
                .InstancePerLifetimeScope();
        }
    }
}
