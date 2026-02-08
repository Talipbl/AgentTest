using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using OrderManagementSystem.Core.Events;

namespace OrderManagementSystem.Infrastructure.Events;

public sealed class InMemoryEventBus : IEventBus
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ConcurrentDictionary<Type, List<Type>> _subscriptions = new();

    public InMemoryEventBus(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void Subscribe<TEvent, TEventHandler>()
        where TEvent : IEvent
        where TEventHandler : IEventHandler<TEvent>
    {
        var handlers = _subscriptions.GetOrAdd(typeof(TEvent), _ => new List<Type>());
        var handlerType = typeof(TEventHandler);

        lock (handlers)
        {
            if (!handlers.Contains(handlerType))
            {
                handlers.Add(handlerType);
            }
        }
    }

    public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : IEvent
    {
        if (!_subscriptions.TryGetValue(typeof(TEvent), out var handlers))
        {
            return;
        }

        using var scope = _serviceProvider.CreateScope();
        List<Type> snapshot;

        lock (handlers)
        {
            snapshot = new List<Type>(handlers);
        }

        foreach (var handlerType in snapshot)
        {
            var handler = scope.ServiceProvider.GetRequiredService(handlerType) as IEventHandler<TEvent>;
            if (handler is null)
            {
                continue;
            }

            await handler.Handle(@event);
        }
    }
}
