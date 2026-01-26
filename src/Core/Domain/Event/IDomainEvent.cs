namespace Inferno.src.Core.Domain.Event
{
    public interface IDomainEvent
    {
        Guid EventId { get; }
        DateTime OccurredAt { get; }
    }
}
