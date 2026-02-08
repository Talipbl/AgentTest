using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OrderManagementSystem.Modules.Inventory.Core.Entities;

namespace OrderManagementSystem.Modules.Inventory.Core.Services;

public interface IInventoryService
{
    Task DecreaseStockAsync(Guid productId, int quantity, string reason);
    Task<IReadOnlyList<InventoryItem>> GetInventoryAsync();
}
