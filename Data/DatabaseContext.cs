using Microsoft.EntityFrameworkCore;

namespace HotelListing.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Examples records
            modelBuilder.Entity<Country>().HasData(
                    new Country
                    {
                        Id = 1,
                        Name = "Colombia",
                        ShortName = "CO"
                    },
                    new Country
                    {
                        Id = 2,
                        Name = "Germany",
                        ShortName = "GE"
                    },
                    new Country
                    {
                        Id = 3,
                        Name = "Spain",
                        ShortName = "SP"
                    }
                );

            modelBuilder.Entity<Hotel>().HasData(
                new Hotel
                {
                    Id = 1,
                    Address = "Address 1",
                    Name = "Name 1",
                    Rating = 4,
                    CountryId = 1
                },
                new Hotel
                {
                    Id = 2,
                    Address = "Address 2",
                    Name = "Name 2",
                    Rating = 4,
                    CountryId = 1
                },
                new Hotel
                {
                    Id = 3,
                    Address = "Address 3",
                    Name = "Name 3",
                    Rating = 4,
                    CountryId = 1
                },
                new Hotel
                {
                    Id = 4,
                    Address = "Address 4",
                    Name = "Name 4",
                    Rating = 4,
                    CountryId = 2
                },
                new Hotel
                {
                    Id = 5,
                    Address = "Address 5",
                    Name = "Name 5",
                    Rating = 4,
                    CountryId = 2
                },
                new Hotel
                {
                    Id = 6,
                    Address = "Address 6",
                    Name = "Name 6",
                    Rating = 5,
                    CountryId = 3
                }
                );
        }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Hotel> Hotels { get; set; }
    }
}
