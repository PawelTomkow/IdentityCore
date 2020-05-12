using System.Threading.Tasks;
using Identity.Core.Models;
using Identity.Persistence.Exceptions;
using Identity.Persistence.Settings;
using Identity.Persistence.TableConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Identity.Persistence.Context
{
    public class IdentityContext : DbContext
    {
        private readonly DatabaseConfig _config;
        public DbSet<User> Users { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<Role> Roles { get; set; }

        public IdentityContext(DbContextOptions options, DatabaseConfig config) : base(options)
        {
            _config = config;
        }

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

            modelBuilder.Seed(_config);
        }
    }
}