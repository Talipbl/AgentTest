using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OrderManagementSystem.Modules.Orders.Core.Entities;

namespace OrderManagementSystem.Modules.Orders.Core.Services;

public interface IOrderService
{
    Task<Order> CreateOrderAsync(string customerName, string customerEmail, IReadOnlyList<OrderItem> items);
    Task<IReadOnlyList<Order>> GetOrdersForCustomerAsync(string customerEmail);
    Task<Order?> GetByIdAsync(Guid id);
}
