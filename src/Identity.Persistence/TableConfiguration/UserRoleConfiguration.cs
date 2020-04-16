using Identity.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Persistence.TableConfiguration
{
    // public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    // {
    //     public void Configure(EntityTypeBuilder<UserRole> builder)
    //     {
    //         builder
    //             .HasKey(x => new {x.IdUser, x.RoleId});
    //
    //         builder
    //             .HasOne(key => key.Role)
    //             .WithMany(u => u.Users)
    //             .HasForeignKey(fk => fk.RoleId);
    //
    //         builder
    //             .HasOne(key => key.User)
    //             .WithMany(x => x.Roles)
    //             .HasForeignKey(fk => fk.IdUser);
    //     }
    // }
}