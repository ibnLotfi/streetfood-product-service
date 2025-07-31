using Product.Domain.Common;

namespace Product.Domain.Events;

public record ProductCreatedEvent(
    Guid ProductId,
    string ProductName
) : IDomainEvent
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}