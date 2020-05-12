using System.Collections.Generic;
using Identity.Core.Models;
using Identity.Persistence.Settings;
using Microsoft.EntityFrameworkCore;

namespace Identity.Persistence.Exceptions
{
    public static class SeedExtension
    {
        public static ModelBuilder Seed(this ModelBuilder modelBuilder, DatabaseConfig config)
        {
            if (!config.Seeder)
            {
                return modelBuilder;
            }
            
            modelBuilder.Entity<Role>()
                .HasData(new Role
                    {
                        IdRole = 1,
                        Name = "superuser",
                        Value = 100
                    },
                    new Role
                    {
                        IdRole = 2,
                        Name = "user",
                        Value = 1
                    });

            return modelBuilder;
        }
    }
}