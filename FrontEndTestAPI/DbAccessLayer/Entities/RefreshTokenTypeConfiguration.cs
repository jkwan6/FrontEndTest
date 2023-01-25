using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FrontEndTestAPI.DbAccessLayer.Entities
{
    public class RefreshTokenTypeConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("RefreshToken");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).IsRequired();

            // Relationships

            builder.HasOne(x => x.User).WithMany(x => x.RefreshTokens).HasForeignKey(x => x.ApplicationUserId);

            builder.HasOne(x => x.AppSession).WithMany(x => x.RefreshTokens).HasForeignKey(x => x.AppSessionId);
        }
    }
}
