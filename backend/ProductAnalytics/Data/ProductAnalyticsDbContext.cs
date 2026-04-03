using Microsoft.EntityFrameworkCore;
using ProductAnalytics.Models;

namespace ProductAnalytics.Data
{
    public sealed class ProductAnalyticsDbContext : DbContext
    {
        /*/ <----- DbSets -----> /*/
        public DbSet<User> Users => Set<User>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Logs> Logs => Set<Logs>();

        // Constructor
        public ProductAnalyticsDbContext(DbContextOptions<ProductAnalyticsDbContext> options) : base(options) { }

        // Model Building
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductAnalyticsDbContext).Assembly);
        }
    }
}
