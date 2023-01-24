using FrontEndTestAPI.Data.Models;
using FrontEndTestAPI.DbAccessLayer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FrontEndTestAPI.Data.AppDbContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> // To configure the App to Use Identity, need to Derive from this Class
    {
        // Will use the Ctor of the base class with no parameters
        public ApplicationDbContext() : base()
        {
        }

        // Will use the Ctor of the base class with parameters = DbContexOptions
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // add the EntityTypeConfiguration classes
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(ApplicationDbContext).Assembly
                );
        }

        // We adding a DbSet to each of the property to access them later on.
        // Kinda like registering each entities in the AppDbContext
        // Those are like properties
        public DbSet<City> Cities => Set<City>();
        public DbSet<Country> Countries => Set<Country>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
        public DbSet<AppSession> AppSessions => Set<AppSession>();
    }
}
