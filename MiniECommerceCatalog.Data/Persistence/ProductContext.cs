using Microsoft.EntityFrameworkCore;
using MiniECommerceCatalog.Data.Models;

namespace MiniECommerceCatalog.Data.Persistence
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
