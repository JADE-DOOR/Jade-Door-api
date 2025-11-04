using JADE_DOOR.Domain.Catalog;
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
    }
}

