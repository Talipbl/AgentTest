using System;

namespace OrderManagementSystem.Modules.Inventory.Core.Entities;

public class StockMovement
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public MovementType Type { get; set; }
    public string Reason { get; set; } = string.Empty;
    public DateTime MovementDate { get; set; }
}
