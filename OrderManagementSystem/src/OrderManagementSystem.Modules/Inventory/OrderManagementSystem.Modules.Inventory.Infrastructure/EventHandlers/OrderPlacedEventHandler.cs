using System.Threading.Tasks;
using OrderManagementSystem.Core.Events;
using OrderManagementSystem.Modules.Inventory.Core.Services;
using OrderManagementSystem.Modules.Orders.Core.Events;

namespace OrderManagementSystem.Modules.Inventory.Infrastructure.EventHandlers;

public class OrderPlacedEventHandler : IEventHandler<OrderPlacedEvent>
{
    private readonly IInventoryService _inventoryService;

    public OrderPlacedEventHandler(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }

    public async Task Handle(OrderPlacedEvent @event)
    {
        foreach (var item in @event.Items)
        {
            await _inventoryService.DecreaseStockAsync(
                item.ProductId,
                item.Quantity,
                $"Order {@event.OrderId}");
        }
    }
}
