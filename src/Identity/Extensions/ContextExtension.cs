using Identity.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Extensions
{
    public static class ContextExtension
    {
        public static IServiceCollection AddCustomContext(this IServiceCollection services,
            IConfiguration configuration, 
            string projectName)
        {
            
            services.AddDbContext<IdentityContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("IdentityDatabase"), x
                    => x.MigrationsAssembly(projectName)));

            return services;
        }
    }
}