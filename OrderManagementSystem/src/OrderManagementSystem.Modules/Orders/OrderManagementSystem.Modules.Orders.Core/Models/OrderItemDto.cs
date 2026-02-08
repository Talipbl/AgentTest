using System;

namespace OrderManagementSystem.Modules.Orders.Core.Models;

public class OrderItemDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}
