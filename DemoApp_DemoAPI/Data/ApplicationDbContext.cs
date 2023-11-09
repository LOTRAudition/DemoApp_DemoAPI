using DemoApp_DemoAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DemoApp_DemoAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<LocalUser> LocalUsers { get; set; }
        public DbSet<Demo> Demos { get; set; }
        public DbSet<DemoNumber> DemoNumbers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Demo>().HasData(
                new Demo
                {
                    Id = 1,
                    Name = "Royal Demo",
                    Details = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                    ImageUrl = "https://www.linkedin.com/in/alpereen/bluedemoimages/demo3.jpg",
                    Occupancy = 4,
                    Rate = 200,
                    Sqft = 550,
                    Amenity = "",
                    CreatedDate = DateTime.Now
                },
              new Demo
              {
                  Id = 2,
                  Name = "Premium Pool Demo",
                  Details = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                  ImageUrl = "https://www.linkedin.com/in/alpereen/bluedemoimages/demo1.jpg",
                  Occupancy = 4,
                  Rate = 300,
                  Sqft = 550,
                  Amenity = "",
                  CreatedDate = DateTime.Now
              },
              new Demo
              {
                  Id = 3,
                  Name = "Luxury Pool Demo",
                  Details = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                  ImageUrl = "https://www.linkedin.com/in/alpereen/bluedemoimages/demo4.jpg",
                  Occupancy = 4,
                  Rate = 400,
                  Sqft = 750,
                  Amenity = "",
                  CreatedDate = DateTime.Now
              },
              new Demo
              {
                  Id = 4,
                  Name = "Diamond Demo",
                  Details = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                  ImageUrl = "https://www.linkedin.com/in/alpereen/bluedemoimages/demo5.jpg",
                  Occupancy = 4,
                  Rate = 550,
                  Sqft = 900,
                  Amenity = "",
                  CreatedDate = DateTime.Now
              },
              new Demo
              {
                  Id = 5,
                  Name = "Diamond Pool Demo",
                  Details = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                  ImageUrl = "https://www.linkedin.com/in/alpereen/bluedemoimages/demo2.jpg",
                  Occupancy = 4,
                  Rate = 600,
                  Sqft = 1100,
                  Amenity = "",
                  CreatedDate = DateTime.Now
              });
        }
    }
}
