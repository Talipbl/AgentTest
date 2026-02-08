using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderManagementSystem.Modules.Inventory.Core.Services;
using OrderManagementSystem.Modules.Inventory.Infrastructure.Data;
using OrderManagementSystem.Modules.Inventory.Infrastructure.EventHandlers;
using OrderManagementSystem.Modules.Inventory.Infrastructure.Services;

namespace OrderManagementSystem.Modules.Inventory.Api.DependencyInjection;

public static class InventoryModuleExtensions
{
    public static IServiceCollection AddInventoryModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<InventoryDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IInventoryService, InventoryService>();
        services.AddScoped<OrderPlacedEventHandler>();

        return services;
    }
}
