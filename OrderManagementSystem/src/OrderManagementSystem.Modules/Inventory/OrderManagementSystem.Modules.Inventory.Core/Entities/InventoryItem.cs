using System;

namespace OrderManagementSystem.Modules.Inventory.Core.Entities;

public class InventoryItem
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public int QuantityInStock { get; set; }
    public int ReorderLevel { get; set; }
    public DateTime LastUpdated { get; set; }
}
