using FreakyFashionServices.Catalog.Models.Domain;
using FreakyFashionServices.Catalog.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace FreakyFashionServices.Catalog.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Items> Items { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Items>().HasData(new { Id = 1, Name = "Black T-Shirt", Description = "Lorem ipsum dolor", Price = 299, AvailableStock = 12 });

        }
    }
}
