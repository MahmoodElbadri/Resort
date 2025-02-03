using Microsoft.EntityFrameworkCore;
using Resort.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resort.Infrastructure.Data
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<Villa> Villas { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
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
                    ImageUrl = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.independent.co.uk%2Ftravel%2Fvilla-holidays-europe-spain-greece-portugal-cyprus-b2472690.html&psig=AOvVaw25JOKvkOKym5g2kAZYqZ9O&ust=1737838204750000&source=images&cd=vfe&opi=89978449&ved=0CBEQjRxqFwoTCOCdp_mdj4sDFQAAAAAdAAAAABAE"
                },
                new Villa()
                {
                    Id = 2,
                    Name = "Palace Villa",
                    Occupancy = 4,
                    Price = 150,
                    ImageUrl = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.villaplus.com%2Fbest-villa-holidays&psig=AOvVaw25JOKvkOKym5g2kAZYqZ9O&ust=1737838204750000&source=images&cd=vfe&opi=89978449&ved=0CBEQjRxqFwoTCOCdp_mdj4sDFQAAAAAdAAAAABAJ",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    Sqft = 200,
                    Description = "This is details of Villa 2"
                },
                new Villa()
                {
                    Id = 3,
                    Name = "Paradise Villa",
                    Description = "This is details of Villa 3",
                    Sqft = 100,
                    UpdatedDate = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    ImageUrl = "https://www.google.com/imgres?imgurl=https%3A%2F%2Fwww.rentvillasalgarve.co.uk%2Fmedia%2F2418546%2FLuxury-holiday-villa-rental-Quinta-do-Lago.jpg%3Fwidth%3D600%26height%3D420%26scale%3Dboth%26mode%3Dcrop%26quality%3D75&tbnid=2pgayn9pyvB0yM&vet=10CBIQxiAoAmoXChMI4J2n-Z2PiwMVAAAAAB0AAAAAEA8..i&imgrefurl=https%3A%2F%2Fwww.rentvillasalgarve.co.uk%2F&docid=TF4EoHH-oNiY2M&w=600&h=420&itg=1&q=villas&ved=0CBIQxiAoAmoXChMI4J2n-Z2PiwMVAAAAAB0AAAAAEA8",
                    Occupancy = 4,
                    Price = 600
                },
                new Villa()
                {
                    Id = 4,
                    Name = "Luxury Villa",
                    Description = "This is details of Villa 4",
                    Sqft = 120,
                    UpdatedDate = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    Occupancy = 4,
                    ImageUrl = "https://www.google.com/imgres?imgurl=https%3A%2F%2Fvillaimages.villaplus.com%2Fimages%2Fvillas%2Fphotos%2F8bb14538-2835-41ba-9c8c-e739fd8069a5_725.jpg&tbnid=H7579zpt_96g-M&vet=10CBYQxiAoCGoXChMI4J2n-Z2PiwMVAAAAAB0AAAAAEA8..i&imgrefurl=https%3A%2F%2Fwww.villaplus.com%2Fdestinations%2Fvillas-in-spain%2Fvillas-in-balearic-islands%2Fmenorca%2Fcalan-porter&docid=t6qGL44sGUmn4M&w=725&h=482&itg=1&q=villas&ved=0CBYQxiAoCGoXChMI4J2n-Z2PiwMVAAAAAB0AAAAAEA8",
                    Price = 700
                }
                );
        }
    }
}
