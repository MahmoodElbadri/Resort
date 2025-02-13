using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Resort.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resort.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public DbSet<Villa> Villas { get; set; }
    public DbSet<VillaNumber> VillaNumbers { get; set; }
    public DbSet<Amenity> Amenities { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seed data for Villa
        modelBuilder.Entity<Villa>().HasData(
            new Villa
            {
                Id = 1,
                Name = "Royal Villa",
                Description = "This is details of Villa 1",
                Price = 200,
                Sqft = 300,
                Occupancy = 4,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                ImageUrl = "https://example.com/images/royal-villa.jpg" // Replace with valid image URL
            },
            new Villa
            {
                Id = 2,
                Name = "Palace Villa",
                Description = "This is details of Villa 2",
                Price = 150,
                Sqft = 200,
                Occupancy = 4,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                ImageUrl = "https://example.com/images/palace-villa.jpg" // Replace with valid image URL
            },
            new Villa
            {
                Id = 3,
                Name = "Paradise Villa",
                Description = "This is details of Villa 3",
                Price = 600,
                Sqft = 100,
                Occupancy = 4,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                ImageUrl = "https://example.com/images/paradise-villa.jpg" // Replace with valid image URL
            },
            new Villa
            {
                Id = 4,
                Name = "Luxury Villa",
                Description = "This is details of Villa 4",
                Price = 700,
                Sqft = 120,
                Occupancy = 4,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                ImageUrl = "https://example.com/images/luxury-villa.jpg" // Replace with valid image URL
            }
        );

        // Seed data for VillaNumber
        modelBuilder.Entity<VillaNumber>().HasData(
            new VillaNumber { Villa_Number = 101, VillaId = 1 },
            new VillaNumber { Villa_Number = 102, VillaId = 1 },
            new VillaNumber { Villa_Number = 103, VillaId = 1 },
            new VillaNumber { Villa_Number = 201, VillaId = 2 },
            new VillaNumber { Villa_Number = 202, VillaId = 2 },
            new VillaNumber { Villa_Number = 203, VillaId = 2 }
        );

        // Seed data for Amenity
        modelBuilder.Entity<Amenity>().HasData(
            new Amenity { Id = 1, VillaId = 1, Name = "Private Pool" },
            new Amenity { Id = 2, VillaId = 1, Name = "Microwave" },
            new Amenity { Id = 3, VillaId = 1, Name = "Private Balcony" },
            new Amenity { Id = 4, VillaId = 1, Name = "1 king bed and 1 sofa bed" },
            new Amenity { Id = 5, VillaId = 2, Name = "Private Plunge Pool" },
            new Amenity { Id = 6, VillaId = 2, Name = "Microwave and Mini Refrigerator" },
            new Amenity { Id = 7, VillaId = 2, Name = "Private Balcony" },
            new Amenity { Id = 8, VillaId = 2, Name = "King bed or 2 double beds" },
            new Amenity { Id = 9, VillaId = 3, Name = "Private Pool" },
            new Amenity { Id = 10, VillaId = 3, Name = "Jacuzzi" },
            new Amenity { Id = 11, VillaId = 3, Name = "Private Balcony" }
        );
    }
}

