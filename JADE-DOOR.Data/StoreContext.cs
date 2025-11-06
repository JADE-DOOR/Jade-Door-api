using JADE_DOOR.Domain.Catalog;
using JADE_DOOR.Domain.Orders;
using Microsoft.EntityFrameworkCore;

namespace JADE_DOOR.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options)
            : base(options)
        {
        }

        public DbSet<Item> Items { get; set; }
    public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure keys and seed initial data (inlined from DbInitializer)
            // Configure Rating primary key and shadow Id property (ensures migrations can be created even if CLR property isn't detected)
            builder.Entity<Rating>().Property<int>("Id");
            builder.Entity<Rating>().HasKey("Id");

            // Seed initial data
            builder.Entity<Item>().HasData(
                new Item("Shirt", "Ohio State shirt", "Nike", 29.99m)
                {
                    Id = 1
                },
                new Item("Shorts", "Ohio State shorts", "Nike", 44.99m)
                {
                    Id = 2
                }
            );
        }
    }
}

