using Inferno.src.Core.Domain.Event;

namespace Inferno.src.Core.Application.Services.OutBox;

public interface IEventPublisher
{
    Task PublishAsync<TEvent>(TEvent domainEvent)
        where TEvent : IDomainEvent;
}
