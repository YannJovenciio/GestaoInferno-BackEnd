using Inferno.src.Core.Domain.Event;

namespace Inferno.src.Core.Application.UseCases.Services
{
    public interface IEventHandler<TEvent>
        where TEvent : IDomainEvent
    {
        Task HandleAsync(TEvent domainEvent, CancellationToken ct);
    }
}
