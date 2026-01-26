using Inferno.src.Core.Domain.Event;

namespace Inferno.src.Core.Application.UseCases.Services;

public interface IEventPublisher
{
    Task PublishAsync<TEvent>(TEvent domainEvent)
        where TEvent : IDomainEvent;
}
