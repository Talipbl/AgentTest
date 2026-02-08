using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Modules.Inventory.Core.Entities;
using OrderManagementSystem.Modules.Inventory.Core.Services;
using OrderManagementSystem.Modules.Inventory.Infrastructure.Data;

namespace OrderManagementSystem.Modules.Inventory.Infrastructure.Services;

public class InventoryService : IInventoryService
{
    private readonly InventoryDbContext _dbContext;

    public InventoryService(InventoryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task DecreaseStockAsync(Guid productId, int quantity, string reason)
    {
        var inventoryItem = await _dbContext.InventoryItems.FirstOrDefaultAsync(item => item.ProductId == productId);
        if (inventoryItem is null)
        {
            inventoryItem = new InventoryItem
            {
                Id = Guid.NewGuid(),
                ProductId = productId,
                QuantityInStock = 0,
                ReorderLevel = 10,
                LastUpdated = DateTime.UtcNow
            };

            _dbContext.InventoryItems.Add(inventoryItem);
        }

        inventoryItem.QuantityInStock -= quantity;
        inventoryItem.LastUpdated = DateTime.UtcNow;

        _dbContext.StockMovements.Add(new StockMovement
        {
            Id = Guid.NewGuid(),
            ProductId = productId,
            Quantity = quantity,
            Type = MovementType.Out,
            Reason = reason,
            MovementDate = DateTime.UtcNow
        });

        await _dbContext.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<InventoryItem>> GetInventoryAsync()
    {
        return await _dbContext.InventoryItems.AsNoTracking().ToListAsync();
    }
}
