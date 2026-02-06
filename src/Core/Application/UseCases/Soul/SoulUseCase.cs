using Inferno.src.Core.Application.DTOs.Request.Soul;
using Inferno.src.Core.Application.DTOs.Response.Soul;
using Inferno.src.Core.Domain.Interfaces.Repository.Souls;
using Inferno.src.Core.Domain.Interfaces.UseCases.Soul;
using Entity = Inferno.src.Core.Domain.Entities;

namespace Inferno.src.Core.Application.UseCases.Soul
{
    public class SoulUseCase : ISoulUseCase
    {
        private readonly ISoulRepository _context;
        private readonly ILogger<SoulUseCase> _logger;

        public SoulUseCase(ILogger<SoulUseCase> logger, ISoulRepository context)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<(List<SoulResponse>? response, string message)> CreateManySoulsAsync(
            List<SoulInput> souls
        )
        {
            if (souls == null || souls.Count == 0)
                return ([], "Empty souls");

            var soulsToCreate = souls
                .Select(s => new Entity.Soul(s.Name, s.Description, s.CavernId))
                .ToList();

            _logger.LogInformation($"Creating {soulsToCreate.Count} souls");

            await _context.CreateManyAsync(soulsToCreate);
            var response = soulsToCreate
                .Select(s => new SoulResponse(
                    s.IdSoul,
                    s.CavernId ?? Guid.Empty,
                    s.SoulName,
                    s.Description,
                    s.Level
                ))
                .ToList();

            _logger.LogInformation($"Successfully created {response.Count} souls");
            return (response, "Souls created successfully");
        }

        public async Task<(List<SoulResponse>? response, string message)> GetAllSoulsAsync()
        {
            var souls = await _context.GetAllAsync();
            _logger.LogInformation($"Found: {souls}");
            if (souls == null || souls.Count == 0)
                return ([], "No souls found");

            var response = souls
                .Select(s => new SoulResponse(
                    s.IdSoul,
                    s.CavernId ?? Guid.Empty,
                    s.SoulName,
                    s.Description,
                    s.Level
                ))
                .ToList();
            _logger.LogInformation($"Successfully found {response.Count} souls");
            return (response, "Souls found successfully");
        }

        public async Task<(SoulResponse? response, string message)> GetSoulByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                return (null, "Invalid GUID received");

            _logger.LogInformation($"Fetching soul with id: {id}");

            var soul = await _context.GetByIdAsync(id);

            _logger.LogInformation($"Successfully found soul with id {id}");
            return (
                new SoulResponse(
                    soul.IdSoul,
                    soul.CavernId ?? Guid.Empty,
                    soul.SoulName,
                    soul.Description,
                    soul.Level
                ),
                "Soul found successfully"
            );
        }

        public async Task<(SoulResponse? response, string message)> CreateSoul(SoulInput request)
        {
            _logger.LogInformation("Received request to create soul");
            if (request == null)
                return (null, "Invalid input received");

            Entity.Soul soul = new Entity.Soul(request.Name, request.Description, request.CavernId);
            await _context.CreateAsync(soul);
            _logger.LogInformation($"Created successfully soul with id: {soul.IdSoul}");
            return (
                new SoulResponse(
                    soul.IdSoul,
                    soul.CavernId,
                    soul.SoulName,
                    soul.Description,
                    soul.Level
                ),
                "Soul created successfully"
            );
        }

        public async Task<(List<SoulResponse>? responses, string message)> GetAllSoulsWithFilters(
            Guid? cavernId,
            HellEnum? level,
            string? description
        )
        {
            _logger.LogInformation(
                $"receveid request to get all souls with filters:{cavernId ?? null},{level ?? null},{description ?? ""}"
            );
            var souls = await _context.GetAllWithFilterAsync(cavernId, level, description);

            var responses = souls
                .Select(s => new SoulResponse(s.IdSoul, s.CavernId, s.SoulName, s.Description, s.Level))
                .ToList();

            _logger.LogInformation($"successfully  found {responses.Count} souls for this filter");
            if (responses.Count == 0)
                return (null, "No souls found for this filter");

            return (responses, $"Successfully found {responses.Count} souls for this filter");
        }

    }
}
