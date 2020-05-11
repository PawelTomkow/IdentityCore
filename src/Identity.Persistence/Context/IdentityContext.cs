﻿using System.Threading.Tasks;
using Identity.Core.Models;
using Identity.Persistence.TableConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Identity.Persistence.Context
{
    public class IdentityContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<Role> Roles { get; set; }

        public IdentityContext(DbContextOptions options) : base(options)
        { }

        public void RunMigration()
        {
            Database.Migrate();
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration(new UserConfiguration())
                .ApplyConfiguration(new RoleConfiguration())
                .ApplyConfiguration(new TokenConfiguration());
            // .ApplyConfiguration(new UserRoleConfiguration());
        }
    }
}