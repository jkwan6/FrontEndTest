﻿using FrontEndTestAPI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FrontEndTestAPI.Data.AppDbContext
{

    // The Application Db Context is the class that is in charge of all the entities during runtime
    // Gotta Register our Entities in the AppDbContext
    public class ApplicationDbContext : DbContext
    {
        // Will use the Ctor of the base class with no parameters
        public ApplicationDbContext() : base()
        {
        }

        //Will use the Ctor of the base class with parameters = DbContexOptions
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
    }
}