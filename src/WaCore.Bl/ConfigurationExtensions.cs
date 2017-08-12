using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaCore.Bl.Services.Account;
using WaCore.Contracts.Bl.Services.Account;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace WaCore.Bl
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection ConfigureWaCoreBl(this IServiceCollection services,
            IConfigurationRoot configuration)
        {
            
            services.TryAddTransient<IAccountService, AccountService>();

            return services;
        }
    }
}
