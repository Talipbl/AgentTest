using Microsoft.Extensions.DependencyInjection;
using OrderManagementSystem.Core.Events;
using OrderManagementSystem.Infrastructure.Events;

namespace OrderManagementSystem.Infrastructure.DependencyInjection;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IEventBus, InMemoryEventBus>();
        return services;
    }
}
