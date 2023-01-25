using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FrontEndTestAPI.DbAccessLayer.Entities
{
    public class AppSessionsTypeConfiguration : IEntityTypeConfiguration<AppSession>
    {
        public void Configure(EntityTypeBuilder<AppSession> builder)
        {
            // Naming the Table
            builder.ToTable("AppSessions");

            // Key Configuration
            builder.HasKey(x => x.AppSessionId);
            builder.Property(x => x.AppSessionId).IsRequired();

            // Relationships
            builder
                .HasOne(x => x.User)
                .WithMany(x => x.AppSessions)
                .HasForeignKey(x => x.ApplicationUserId);
        }
    }
}
