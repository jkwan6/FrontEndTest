using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FrontEndTestAPI.Data.Models
{
    public class CityEntityTypeConfiguration : IEntityTypeConfiguration<City>
    {
        // Settin up the Configuration for the City Entity
        public void Configure(EntityTypeBuilder<City> builder)
        {
            // Putting the City Entity Class to a Table names "Cities"
            builder.ToTable("Cities");
            
            // Setting the Primary Key of the Entity to the Id
            builder.HasKey(x => x.Id);

            // Telling the Config which column is required
            builder.Property(x => x.Id).IsRequired();
            
            // Setting the Relationship of the Entity
            builder
                .HasOne(x => x.Country)
                .WithMany(x => x.Cities)
                .HasForeignKey(x => x.CountryId);
            
            // Setting the Value Type of the Column of Lat and Long
            builder.Property(x => x.Lat).HasColumnType("decimal(7,4)");
            builder.Property(x => x.Lon).HasColumnType("decimal(7,4)");
        }
    }
}
