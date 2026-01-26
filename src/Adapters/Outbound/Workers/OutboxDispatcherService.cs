using System.Text.Json;
using Inferno.src.Adapters.Outbound.Persistence;
using Inferno.src.Core.Application.UseCases.Services;
using Inferno.src.Core.Domain.Event;

namespace Inferno.src.Adapters.Outbound.Workers;

public class OutboxDispatcherService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<OutboxDispatcherService> _logger;

    public OutboxDispatcherService(
        IServiceProvider serviceProvider,
        ILogger<OutboxDispatcherService> logger
    )
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("OutboxDispatcherService started");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _serviceProvider.CreateAsyncScope();
                var db = scope.ServiceProvider.GetRequiredService<HellDbContext>();
                var MaxAttempts = 5;
                var events = db
                    .OutBoxEvent.Where(x => x.ProcessedAt == null && x.Attempts < MaxAttempts)
                    .Take(20);
                foreach (var domainEvent in events)
                {
                    try
                    {
                        var eventType = domainEvent.Type;
                        if (eventType == typeof(SinCreatedEvent).AssemblyQualifiedName)
                        {
                            var payload = JsonSerializer.Deserialize<SinCreatedEvent>(
                                domainEvent.Content
                            );
                            if (payload == null)
                            {
                                throw new InvalidOperationException();
                            }
                            var handler = scope.ServiceProvider.GetRequiredService<
                                IEventHandler<SinCreatedEvent>
                            >();
                            await handler.HandleAsync(payload, stoppingToken);
                            domainEvent.ProcessedAt = DateTime.UtcNow;
                            domainEvent.Attempts++;
                            domainEvent.Error = null;
                            await db.SaveChangesAsync();
                            _logger.LogInformation(
                                "Event {EventId} processed successfully",
                                domainEvent.OutBoxEventId
                            );
                        }
                        else
                        {
                            domainEvent.Attempts++;
                            domainEvent.Error = $"Unknown event type: {eventType}";
                            _logger.LogWarning("Unknown event type: {Type}", eventType);
                            await db.SaveChangesAsync();
                        }
                    }
                    catch (System.Exception ex)
                    {
                        domainEvent.Attempts++;
                        domainEvent.Error = ex.Message;
                        _logger.LogError(
                            ex,
                            "Error processing event {EventId}. Attempt {Attempts}",
                            domainEvent.OutBoxEventId,
                            domainEvent.Attempts
                        );
                        await db.SaveChangesAsync();
                    }
                }
            }
            catch (System.Exception)
            {
                throw;
            }
            await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);
        }

        _logger.LogInformation("OutboxDispatcherService stopped");
    }
}
