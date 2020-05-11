using System;
using Identity.Persistence.Context;
using Identity.Persistence.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Extensions
{
    public static class ContextExtension
    {
        public static IServiceCollection AddCustomContext(this IServiceCollection services,
            string projectName)
        {
            var config = services.BuildServiceProvider().GetService<DatabaseConfig>();

            switch (config.Type)
            {
                case "Mssql":
                    services.AddDbContext<IdentityContext>(options =>
                        options.UseSqlServer(config.Connection, x
                            => x.MigrationsAssembly(projectName)));
                    break;
                case "Npgsql":
                    services.AddDbContext<IdentityContext>(options =>
                        options.UseNpgsql(config.Connection, x
                            => x.MigrationsAssembly(projectName)));
                    break;
                default:
                    throw new ArgumentException();
            }

            return services;
        }
        
    }
}