using Inferno.src.Adapters.Inbound.Controllers.Sin;
using Inferno.src.Core.Application.UseCases.Services;
using Inferno.src.Core.Domain.Enums;
using Inferno.src.Core.Domain.Event;
using Inferno.src.Core.Domain.Interfaces.Repository.Sin;
using Entity = Inferno.src.Core.Domain.Entities;

namespace Inferno.src.Core.Application.UseCases.Sin
{
    public class SinUseCase : ISinUseCase
    {
        private readonly ISinRepository _context;
        private readonly ILogger<SinUseCase> _logger;

        private readonly IEventPublisher _eventPublisher;

        public SinUseCase(
            ISinRepository context,
            ILogger<SinUseCase> logger,
            IEventPublisher eventPublisher
        )
        {
            _context = context;
            _logger = logger;
            _eventPublisher = eventPublisher;
        }

        public async Task<(SinResponse response, string message)> CreateSin(SinInput input)
        {
            _logger.LogInformation(
                "Starting CreateSin operation with SinName: {SinName}, Severity: {Severity}",
                input.SinName,
                input.SinSeverity
            );

            var sin = new Entity.Sin(input.SinName, input.SinSeverity);
            await _context.Create(sin);
            await _eventPublisher.PublishAsync(
                new SinCreatedEvent(sin.IdSin, sin.SinName, sin.SinSeverity, DateTime.UtcNow)
            );

            var message =
                $"Sucessful created sin with properties {sin.IdSin},{sin.SinName},{sin.SinSeverity}";
            _logger.LogInformation(
                "Sin created successfully with ID: {SinId}, Name: {SinName}",
                sin.IdSin,
                sin.SinName
            );

            return (new SinResponse(sin.IdSin, sin.SinName, sin.SinSeverity), message);
        }

        public async Task<(SinResponse? response, string message)> Delete(Guid idSin)
        {
            _logger.LogInformation("Starting Delete operation for Sin ID: {SinId}", idSin);

            var sin = await _context.Delete(idSin);
            var response = new SinResponse(sin.IdSin, sin.SinName, sin.SinSeverity);

            if (response == null)
            {
                _logger.LogWarning("Sin not found for deletion with ID: {SinId}", idSin);
                return (response, "No sin found for this id");
            }

            var message = $"Sucessful deleted sin with id ${idSin}";
            _logger.LogInformation(
                "Sin deleted successfully with ID: {SinId}, Name: {SinName}",
                sin.IdSin,
                sin.SinName
            );

            return (response, message);
        }

        public async Task<(List<SinResponse> responses, string message)> GetAllSins()
        {
            _logger.LogInformation("Starting GetAllSins operation");

            var sins = await _context.GetAll();
            var responses = sins.Select(s => new SinResponse(s.IdSin, s.SinName, s.SinSeverity))
                .ToList();

            if (responses.Count == 0)
            {
                _logger.LogInformation("No sins found in GetAllSins operation");
                return (responses, "No sins founded");
            }

            var message = $"Sucessful found ${responses.Count} sins";
            _logger.LogInformation("Successfully retrieved {SinCount} sins", responses.Count);

            return (responses, message);
        }

        public async Task<(List<SinResponse> responses, string message)> GetAllWithFilters(
            Guid? IdSIn,
            Severity? severity
        )
        {
            _logger.LogInformation(
                "Starting GetAllWithFilters operation with filters - SinID: {SinId}, Severity: {Severity}",
                IdSIn ?? Guid.Empty,
                severity
            );

            var sins = await _context.GetAllWithFilters(IdSIn, severity);
            var responses = sins.Select(s => new SinResponse(s.IdSin, s.SinName, s.SinSeverity))
                .ToList();

            if (responses.Count == 0)
            {
                _logger.LogInformation("No sins found for the applied filters");
                return (responses, "No sins found for this filter");
            }

            var message = $"Sucessful found ${responses.Count} sins for this filter";
            _logger.LogInformation(
                "Successfully retrieved {SinCount} sins matching filters",
                responses.Count
            );

            return (responses, message);
        }

        public async Task<(SinResponse? response, string message)> GetById(Guid idSin)
        {
            _logger.LogInformation("Starting GetById operation for Sin ID: {SinId}", idSin);

            var sin = await _context.GetById(idSin);
            var response = new SinResponse(sin.IdSin, sin.SinName, sin.SinSeverity);

            if (response == null)
            {
                _logger.LogWarning("Sin not found with ID: {SinId}", idSin);
                return (response, "No sin found for this id");
            }

            var message = $"Sucessful found sin: ${response.SinName} for this id";
            _logger.LogInformation(
                "Successfully retrieved Sin with ID: {SinId}, Name: {SinName}",
                response.SinName,
                idSin
            );

            return (response, message);
        }

        public Task<(SinResponse response, string message)> Update(Guid idSin, SinResponse sin)
        {
            _logger.LogWarning(
                "Update method called but not yet implemented for Sin ID: {SinId}",
                idSin
            );
            throw new NotImplementedException();
        }

        public Task<(List<SinResponse> responses, string message)> CreateMany(List<SinInput> input)
        {
            throw new NotImplementedException();
        }

        public async Task<(List<SinOrderedBySeverity>? response, string message)> GetAllOrdered()
        {
            var sins = await _context.GetAll();
            if (sins.Count == 0)
            {
                return (null, "No sins found");
            }
            var sinsCount = sins.Count();
            var response = sins.GroupBy(s => s.SinSeverity)
                .Select(x => new SinOrderedBySeverity(
                    x.Key,
                    x.Count(),
                    x.Select(x => x.SinName).ToList(),
                    (x.Count()) / ((double)sinsCount) * 100
                ))
                .ToList();

            return (response, "Sucessfull retrivied sins ordered by severity");
        }
    }
}
