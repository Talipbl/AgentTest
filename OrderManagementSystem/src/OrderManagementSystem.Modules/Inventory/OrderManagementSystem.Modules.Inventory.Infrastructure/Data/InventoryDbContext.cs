using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Modules.Inventory.Core.Entities;

namespace OrderManagementSystem.Modules.Inventory.Infrastructure.Data;

public class InventoryDbContext : DbContext
{
    public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options)
    {
    }

    public DbSet<InventoryItem> InventoryItems => Set<InventoryItem>();
    public DbSet<StockMovement> StockMovements => Set<StockMovement>();
}
