using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Core.Events;
using OrderManagementSystem.Modules.Orders.Core.Entities;
using OrderManagementSystem.Modules.Orders.Core.Events;
using OrderManagementSystem.Modules.Orders.Core.Models;
using OrderManagementSystem.Modules.Orders.Core.Services;
using OrderManagementSystem.Modules.Orders.Infrastructure.Data;

namespace OrderManagementSystem.Modules.Orders.Infrastructure.Services;

public class OrderService : IOrderService
{
    private readonly OrdersDbContext _dbContext;
    private readonly IEventBus _eventBus;

    public OrderService(OrdersDbContext dbContext, IEventBus eventBus)
    {
        _dbContext = dbContext;
        _eventBus = eventBus;
    }

    public async Task<Order> CreateOrderAsync(string customerName, string customerEmail, IReadOnlyList<OrderItem> items)
    {
        var orderId = Guid.NewGuid();
        var orderItems = items.Select(item => new OrderItem
        {
            Id = Guid.NewGuid(),
            OrderId = orderId,
            ProductId = item.ProductId,
            ProductName = item.ProductName,
            Quantity = item.Quantity,
            UnitPrice = item.UnitPrice,
            TotalPrice = item.UnitPrice * item.Quantity
        }).ToList();

        var order = new Order
        {
            Id = orderId,
            OrderNumber = $"ORD-{DateTime.UtcNow:yyyyMMddHHmmss}",
            OrderDate = DateTime.UtcNow,
            Status = OrderStatus.Pending,
            CustomerName = customerName,
            CustomerEmail = customerEmail,
            OrderItems = orderItems,
            TotalAmount = orderItems.Sum(item => item.TotalPrice)
        };

        _dbContext.Orders.Add(order);
        await _dbContext.SaveChangesAsync();

        var orderPlacedEvent = new OrderPlacedEvent
        {
            OrderId = order.Id,
            PlacedAt = order.OrderDate,
            Items = order.OrderItems.Select(item => new OrderItemDto
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity
            }).ToList()
        };

        await _eventBus.PublishAsync(orderPlacedEvent);

        return order;
    }

    public async Task<IReadOnlyList<Order>> GetOrdersForCustomerAsync(string customerEmail)
    {
        return await _dbContext.Orders
            .AsNoTracking()
            .Include(order => order.OrderItems)
            .Where(order => order.CustomerEmail == customerEmail)
            .ToListAsync();
    }

    public async Task<Order?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Orders
            .AsNoTracking()
            .Include(order => order.OrderItems)
            .FirstOrDefaultAsync(order => order.Id == id);
    }
}
