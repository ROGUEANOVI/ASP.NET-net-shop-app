using Microsoft.EntityFrameworkCore;
using net_shop.Models;

namespace net_shop.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
            
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<AppType> AppType { get; set; }
        public DbSet<Product> Product { get; set; }
    }
}
