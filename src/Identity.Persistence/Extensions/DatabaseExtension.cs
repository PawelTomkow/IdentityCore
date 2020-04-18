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
            var db = app.ApplicationServices.GetService<IdentityContext>();
            var dbConfig = app.ApplicationServices.GetService<DatabaseConfig>();
            
            if (dbConfig?.Migrations == true)
            {
                db?.RunMigration();
            }

            return app;
        }
    }
    
}