using System.Text.Json;
using Inferno.src.Adapters.Outbound.Persistence;
using Inferno.src.Core.Domain.Entities;
using Inferno.src.Core.Domain.Event;

namespace Inferno.src.Core.Application.UseCases.Services;

public class OutBoxEventPublisher : IEventPublisher
{
    private readonly HellDbContext _context;
    private readonly ILogger<OutBoxEventPublisher> _logger;

    public OutBoxEventPublisher(HellDbContext context, ILogger<OutBoxEventPublisher> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task PublishAsync<TEvent>(TEvent domainEvent)
        where TEvent : IDomainEvent
    {
        _logger.LogInformation($"Receveid attempt to publish event{domainEvent.EventId}");
        try
        {
            var json = JsonSerializer.Serialize(domainEvent);
            var type =
                typeof(TEvent).AssemblyQualifiedName
                ?? typeof(TEvent).FullName
                ?? typeof(TEvent).Name;
            var outBoxEvent = new OutBoxEvent(type, json);
            _logger.LogInformation("Publishing outbox event {EventType}", type);

            await _context.OutBoxEvent.AddAsync(outBoxEvent);
            await _context.SaveChangesAsync();
            _logger.LogInformation(
                "Outbox event persisted {EventType} ({EventId})",
                outBoxEvent.Type,
                outBoxEvent.OutBoxEventId
            );
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex, "Failed to publish outbox event");
            throw new InvalidOperationException($"Exception:{ex}");
        }
    }
}
