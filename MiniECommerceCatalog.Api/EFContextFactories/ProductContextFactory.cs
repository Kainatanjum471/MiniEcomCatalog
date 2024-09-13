using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MiniECommerceCatalog.Data.Persistence;

namespace MiniECommerceCatalog.Api.Persistence
{
    public class ProductContextFactory : IDesignTimeDbContextFactory<ProductContext>
    {
        public ProductContext CreateDbContext(string[] args)
        {
            // Build configuration using Host.CreateDefaultBuilder, which works with .NET 8.0

            var host = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    // Ensure the appsettings.json file is used in the design-time configuration
                    config.SetBasePath(Directory.GetCurrentDirectory());
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .Build();

            // Get the configuration and connection string
            var configuration = host.Services.GetRequiredService<IConfiguration>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Build the DbContextOptions
            var optionsBuilder = new DbContextOptionsBuilder<ProductContext>();
            optionsBuilder.UseSqlite(connectionString);

            return new ProductContext(optionsBuilder.Options);
        }
    }
}