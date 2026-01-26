using Inferno.src.Core.Domain.Enums;

namespace Inferno.src.Core.Domain.Event
{
    public record SinCreatedEvent(
        Guid SinId,
        string SinName,
        Severity Severity,
        DateTime OccurredAt,
        Guid EventId = default
    ) : IDomainEvent
    {
        public Guid EventId { get; init; } = EventId == default ? Guid.NewGuid() : EventId;
    }
}
