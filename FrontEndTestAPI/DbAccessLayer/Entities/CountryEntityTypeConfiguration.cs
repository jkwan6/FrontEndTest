using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FrontEndTestAPI.Data.Models
{
    public class CountryEntityTypeConfiguration: IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            // Setting up a new Table for Country Class Entity with name Countries
            builder.ToTable("Countries");

            // Setting up the primary key of the Table
            builder.HasKey(x => x.Id);

            // Making the Id Column to be required
            builder.Property(x => x.Id).IsRequired();
        }
    }
}
