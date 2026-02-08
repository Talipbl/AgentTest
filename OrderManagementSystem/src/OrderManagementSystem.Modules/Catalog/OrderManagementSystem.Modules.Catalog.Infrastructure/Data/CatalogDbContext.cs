using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Modules.Catalog.Core.Entities;

namespace OrderManagementSystem.Modules.Catalog.Infrastructure.Data;

public class CatalogDbContext : DbContext
{
    public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();
}
