using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Shared.Services
{
    public static class ConfigureServicesExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register services from different files
            services.AddDbContextServices(configuration);
            services.AddRepositoryServices();
            services.AddDomainServices();
            services.AddIdentityServices();

            return services;
        }
    }
}
