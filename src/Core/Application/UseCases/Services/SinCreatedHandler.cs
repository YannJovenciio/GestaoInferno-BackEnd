using Inferno.src.Core.Domain.Enums;
using Inferno.src.Core.Domain.Event;
using Inferno.src.Core.Domain.Interfaces;
using Inferno.src.Core.Domain.Interfaces.Persecution;
using Inferno.src.Core.Domain.Interfaces.Repository.Souls;

namespace Inferno.src.Core.Application.UseCases.Services;

public class SinCreatedHandler : IEventHandler<SinCreatedEvent>
{
    private readonly IDemonRepository _demonRepository;
    private readonly ISoulRepository _soulRepository;
    private readonly IPersecutionRepository _persecutionRepository;
    private readonly ILogger<SinCreatedHandler> _logger;

    public SinCreatedHandler(
        IDemonRepository demonRepository,
        ISoulRepository soulRepository,
        IPersecutionRepository persecutionRepository,
        ILogger<SinCreatedHandler> logger
    )
    {
        _demonRepository = demonRepository;
        _soulRepository = soulRepository;
        _persecutionRepository = persecutionRepository;
        _logger = logger;
    }

    public async Task HandleAsync(SinCreatedEvent domainEvent, CancellationToken ct)
    {
        _logger.LogInformation(
            "SinCreatedHandler.HandleAsync called for Sin {SinId}",
            domainEvent.SinId
        );

        if (domainEvent.Severity < Severity.high)
        {
            _logger.LogInformation($"Severity:{domainEvent.Severity} doesn't match requirements");
            return;
        }

        _logger.LogInformation("Fetching all demons, souls, and persecutions...");
        var demons = await _demonRepository.GetAllAsync();
        var souls = await _soulRepository.GetAllAsync();
        var persecutions = await _persecutionRepository.GetAllPersecutions();

        _logger.LogInformation(
            "Found {DemonCount} demons, {SoulCount} souls, {PersecutionCount} persecutions",
            demons.Count,
            souls.Count,
            persecutions.Count
        );

        if (!demons.Any() || !souls.Any())
        {
            _logger.LogWarning("No demons or souls available to assign persecution");
            return;
        }

        // pick the first Demon/Soul pair not already used
        var pair = (
            from d in demons
            from s in souls
            where !persecutions.Any(p => p.IdDemon == d.IdDemon && p.IdSoul == s.IdSoul)
            select new { Demon = d, Soul = s }
        ).FirstOrDefault();

        if (pair == null)
        {
            _logger.LogInformation(
                "No available Demon/Soul pair (all pairs already have persecution)"
            );
            return;
        }

        _logger.LogInformation(
            "Creating persecution for Demon {DemonId} and Soul {SoulId}",
            pair.Demon.IdDemon,
            pair.Soul.IdSoul
        );

        var persecutionCreated = await _persecutionRepository.CreatePersecution(
            pair.Demon.IdDemon,
            pair.Soul.IdSoul
        );

        _logger.LogInformation(
            "Persecution created successfully with Demon {DemonId} and Soul {SoulId}",
            pair.Demon.IdDemon,
            pair.Soul.IdSoul
        );
    }
}
