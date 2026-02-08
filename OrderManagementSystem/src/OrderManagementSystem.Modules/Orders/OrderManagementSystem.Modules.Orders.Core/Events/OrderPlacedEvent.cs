using System;
using System.Collections.Generic;
using OrderManagementSystem.Core.Events;
using OrderManagementSystem.Modules.Orders.Core.Models;

namespace OrderManagementSystem.Modules.Orders.Core.Events;

public class OrderPlacedEvent : IEvent
{
    public Guid EventId { get; init; } = Guid.NewGuid();
    public DateTime OccurredAt { get; init; } = DateTime.UtcNow;
    public Guid OrderId { get; init; }
    public List<OrderItemDto> Items { get; init; } = new();
    public DateTime PlacedAt { get; init; } = DateTime.UtcNow;
}
