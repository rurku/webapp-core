using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaCore.Web.Infrastructure;
using WaCore.Contracts.Bl.Services.Account;
using WaCore.Web.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Mvc.Razor;

namespace WaCore.Web
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection ConfigureWaCoreWeb(this IServiceCollection services,
            IConfigurationRoot configuration)
        {
            services.TryAddSingleton<IRazorViewEngine, WaCoreRazorViewEngine>();
            // WaCoreRazorViewEngine adds /Views/WaCore as the last place to search for views
            // WaCore views are all under Views/WaCore
            // to modify a view just copy it to a higher priority location
            // ie copy /Views/WaCore/Manage/*.cshtml up to /Views/Manage/ and that one will have higher priority
            // and you can modify it however you like
            // upgrading to newer versions of WaCore could modify or add views below /Views/WaCore
            // so you may need to compare your custom views to the originals again after upgrades


            services.AddMvcCore();
            return services;
        }
    }
}
