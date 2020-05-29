using Identity.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Persistence.TableConfiguration
{
    public class UserRoleConfiguration: IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(ru => new {ru.RoleId, ru.UserId});

            builder.HasOne<User>(ru => ru.User)
                .WithMany(u => u.UserRole)
                .HasForeignKey(ru => ru.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.HasOne(ru => ru.Role)
                .WithMany(r => r.UserRole)
                .HasForeignKey(ru => ru.RoleId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}