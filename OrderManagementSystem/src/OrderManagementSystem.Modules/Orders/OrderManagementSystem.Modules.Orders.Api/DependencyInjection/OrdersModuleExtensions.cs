using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderManagementSystem.Modules.Orders.Core.Services;
using OrderManagementSystem.Modules.Orders.Infrastructure.Data;
using OrderManagementSystem.Modules.Orders.Infrastructure.Services;

namespace OrderManagementSystem.Modules.Orders.Api.DependencyInjection;

public static class OrdersModuleExtensions
{
    public static IServiceCollection AddOrdersModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<OrdersDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IOrderService, OrderService>();

        return services;
    }
}
