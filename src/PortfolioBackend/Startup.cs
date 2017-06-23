using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Autofac;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Autofac.Extensions.DependencyInjection;
using PortfolioBackend.Core.DAL;
using PortfolioBackend.Configurations;
using PortfolioBackend.Core.BLL.Base;

namespace PortfolioBackend
{
    public class Startup
    {
        private static IConfigurationRoot _configuration;
        private static IContainer _currentContainer = null;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            _configuration = builder.Build();
        }

        public static IContainer ApplicationContainer
        {
            get
            {
                return _currentContainer;
            }
            private set
            {
                _currentContainer = value;
            }
        }

        public static IConfigurationRoot Configuration
        {
            get
            {
                return _configuration;
            }
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("PortfolioDatabase")));

            services.AddDbContext<PostgresDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("Portfolio")));

            services.AddScoped<IDataAccessProvider, DataAccessPostgreSqlProvider>();

            EntityProperties.UseDefault = Configuration.GetValue<bool>("AppConfiguration:EntityProperties:UseDefault");
            EntityProperties.CreatedDate = Configuration.GetValue<bool>("AppConfiguration:EntityProperties:CreatedDate");
            EntityProperties.ModifiedDate = Configuration.GetValue<bool>("AppConfiguration:EntityProperties:ModifiedDate");
            EntityProperties.IsDeleted = Configuration.GetValue<bool>("AppConfiguration:EntityProperties:IsDeleted");
            EntityProperties.OwnerId = Configuration.GetValue<bool>("AppConfiguration:EntityProperties:OwnerId");
            //services.Configure<EntityProperties>(Configuration.GetSection("AppConfiguration:EntityProperties"));

            #region addMvc settings

            services.AddCors(option => option.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

            services.AddMvc()
                    .AddJsonOptions(options =>
                    {
                        options.SerializerSettings.ReferenceLoopHandling =
                            Newtonsoft.Json.ReferenceLoopHandling.Ignore;

                        options.SerializerSettings.ContractResolver =
                            new CamelCasePropertyNamesContractResolver();

                        options.SerializerSettings.PreserveReferencesHandling =
                            Newtonsoft.Json.PreserveReferencesHandling.Objects;
                    });

            #endregion

            #region Autofac registrations

            var builder = new ContainerBuilder();

            builder.RegisterModule(new AutofacModule());
            builder.Populate(services);

            _currentContainer = builder.Build();

            #endregion

            var applicationServices = new AutofacServiceProvider(_currentContainer);
            AppDependencyResolver.Init(applicationServices);

            return applicationServices;
        }

        public void Configure(IApplicationBuilder app, IApplicationLifetime appLifetime, ILoggerFactory loggerFactory, PostgresDbContext dbContext)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseCors("AllowAll");
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "api/{controller=Home}/{action=Index}/{id?}"
                    );
            });

            DbInitializer.InitializePG(dbContext);

            appLifetime.ApplicationStopped.Register(() => _currentContainer.Dispose());
        }
    }
}
