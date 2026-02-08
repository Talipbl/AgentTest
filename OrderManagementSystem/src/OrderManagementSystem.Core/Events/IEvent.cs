using System;

namespace OrderManagementSystem.Core.Events;

public interface IEvent
{
    Guid EventId { get; }
    DateTime OccurredAt { get; }
}
