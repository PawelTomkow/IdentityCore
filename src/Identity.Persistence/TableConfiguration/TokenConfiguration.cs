using Identity.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Persistence.TableConfiguration
{
    public class TokenConfiguration : IEntityTypeConfiguration<Token>
    {
        public void Configure(EntityTypeBuilder<Token> builder)
        {
            builder
                .HasKey(key => key.TokenId)
                .HasOne(key => key.User)
                .WithOne()
                .HasForeignKey<Token>(fk => fk.UserId);

        }
    }
}