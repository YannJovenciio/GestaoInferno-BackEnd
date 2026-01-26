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
        if (domainEvent.Severity < Severity.high)
        {
            _logger.LogInformation($"Severity:{domainEvent.Severity} doenst match requirements");
            return;
        }

        var soul = (await _soulRepository.GetAllAsync()).FirstOrDefault();
        var demon = (await _demonRepository.GetAllAsync()).FirstOrDefault();
        if (soul == null || demon == null)
        {
            _logger.LogWarning("No soul or demon found to assign persecution");
            return;
        }
        bool alreadyExits = (
            await _persecutionRepository.GetAllPersecutionWithFilter(demon.IdDemon, soul.IdSoul)
        ).Any();

        if (!alreadyExits)
        {
            var persecution = await _persecutionRepository.CreatePersecution(
                demon.IdDemon,
                soul.IdSoul
            );
            _logger.LogInformation(
                "Persecution created with ids {idDemon},{idSoul}",
                demon.IdDemon,
                soul.IdSoul
            );
        }
    }
}
