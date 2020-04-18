using Identity.Persistence.Context;
using Identity.Persistence.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Persistence.Extensions
{
    public static class DatabaseExtension
    {
        public static IApplicationBuilder UseMigrationIdentity(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<IdentityContext>();
                var dbConfig = app.ApplicationServices.GetService<DatabaseConfig>();
            
                if (dbConfig?.Migrations == true)
                {
                    db?.RunMigration();
                }
            }
            
            return app;
        }
    }
    
}