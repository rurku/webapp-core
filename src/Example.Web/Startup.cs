using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Example.Data;
using WaCore.Bl;
using WaCore.Contracts.Data.Repositories;
using WaCore.Contracts.Data.Repositories.Base;
using WaCore.Data;
using WaCore.Data.Repositories;
using WaCore.Entities.Core;
using WaCore.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

namespace Example.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, IHostingEnvironment hostingEnvironment)
        {
            // Setup configuration sources.
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);


            if (env.IsEnvironment("Development"))
            {
                // This reads the configuration keys from the secret store.
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                //builder.AddUserSecrets();
            }


            // most common use of environment variables would be in azure hosting
            // since it is added last anything in env vars would trump the same setting in previous config sources
            // so no risk of messing up settings if deploying a new version to azure
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Add Entity Framework services to the services container.
            services.AddEntityFramework()
                .AddDbContext<ExampleDbContext>(options =>
                    options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));

            services.ConfigureWaCoreWeb(Configuration);
            services.ConfigureWaCoreBl(Configuration);

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<ExampleDbContext, Guid>()
                .AddDefaultTokenProviders();


            services.TryAddScoped<IUserRepository, UserRepository>();
            services.TryAddScoped<IDbContextFactory, ExampleContextFactory>();

        }

        public IConfigurationRoot Configuration { get; set; }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsEnvironment("Development"))
            {
                //app.UseBrowserLink();

                app.UseDeveloperExceptionPage();

                //app.UseDatabaseErrorPage(DatabaseErrorPageOptions.ShowAll);
                //app.UseStatusCodePagesWithReExecute("/error/{0}");
                app.UseStatusCodePagesWithReExecute("/error/{0}");
            }

            // Add static files to the request pipeline.
            app.UseStaticFiles();

            // Add cookie-based authentication to the request pipeline.
            app.UseIdentity();

            // Add MVC to the request pipeline.
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                // Uncomment the following line to add a route for porting Web API 2 controllers.
                // routes.MapWebApiRoute("DefaultApi", "api/{controller}/{id?}");
            });
        }
    }
}
