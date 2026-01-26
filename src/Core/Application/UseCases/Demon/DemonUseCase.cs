using Inferno.src.Adapters.Inbound.Controllers.Demon;
using Inferno.src.Core.Application.DTOs;
using Inferno.src.Core.Application.DTOs.Request.Demon;
using Inferno.src.Core.Domain.Interfaces;
using Inferno.src.Core.Domain.Interfaces.UseCases.Demon;
using Entity = Inferno.src.Core.Domain.Entities;

namespace Inferno.src.Core.Application.UseCases.Demon
{
    public class DemonUseCase : IDemonUseCase
    {
        private readonly IDemonRepository _context;
        private readonly ILogger<DemonUseCase> _logger;

        public DemonUseCase(IDemonRepository context, ILogger<DemonUseCase> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<(DemonResponse? response, string message)> CreateAsync(DemonInput input)
        {
            var demon = new Entity.Demon(input.DemonName, input.CategoryId);
            _logger.LogInformation($"Creating demon entity: {demon}");
            await _context.CreateAsync(demon);
            return (
                new DemonResponse(demon.IdDemon, demon.DemonName, demon.CategoryId.Value),
                "Demon created sucessfuly"
            );
        }

        public async Task<(List<DemonResponse>? responses, string message)> CreateManyAsync(
            List<DemonInput> inputs
        )
        {
            if (inputs == null || inputs.Count == 0)
            {
                return (null, "No demons provided");
            }
            var demon = inputs.Select(d => new Entity.Demon(d.DemonName, d.CategoryId)).ToList();
            await _context.CreateManyAsync(demon);
            List<DemonResponse> responses = demon
                .Select(d => new DemonResponse(d.IdDemon, d.DemonName, d.CategoryId.Value))
                .ToList();
            return (responses, "Criado com sucesso");
        }

        public async Task<(DemonResponse? response, string message)> GetByIdAsync(Guid id)
        {
            var demon = await _context.GetByIdAsync(id);
            if (demon == null)
            {
                return (null, $"Demon with id {id} not found");
            }
            DemonResponse response = new(demon.IdDemon, demon.DemonName, demon.CategoryId.Value);
            return (response, "Demon found sucessfuly");
        }

        public async Task<(List<DemonResponse>? responses, string message)> GetAllAsync(
            int? pageSize,
            int? pageNumber
        )
        {
            var demons = await _context.GetAllAsync(pageSize, pageNumber);
            var response = demons
                .Select(d => new DemonResponse(d.IdDemon, d.DemonName, d.CategoryId.Value))
                .ToList();
            if (response.Count == 0)
            {
                return ([], "Empty list");
            }
            return (response, "Demons retrieved successfully");
        }

        public async Task<(List<DemonResponse>? responses, string message)> GetAllWithFiltersAsync(
            Guid? categoryId,
            string? name,
            DateTime? createdAt
        )
        {
            if (categoryId == null && string.IsNullOrWhiteSpace(name) && createdAt == null)
            {
                _logger.LogWarning("GetAllWithFiltersAsync called with no filters provided");
                return ([], "At least one filter must be provided");
            }

            var demons = await _context.GetAllWithFiltersAsync(categoryId, name, createdAt);
            var responses = demons
                .Select(d => new DemonResponse(d.IdDemon, d.DemonName, d.CategoryId.Value))
                .ToList();

            if (responses.Count == 0)
            {
                return ([], "No demons found for the provided filters");
            }

            return (responses, $"Successfully found {responses.Count} demon(s)");
        }

        public async Task<(
            List<DemonOrderedByCategory> responses,
            string message
        )> GetDemonByCategory()
        {
            var demons = await _context.GetAllAsync();
            var demonsGroupedByCategory = demons
                .GroupBy(d => d.CategoryId)
                .Select(g => new DemonOrderedByCategory(
                    g.Key,
                    g.Count(),
                    (g.Count() / (double)demons.Count) * 100,
                    g.Select(S => S.Category).ToList()
                ))
                .OrderBy(x => x.DemonCount)
                .ToList();
            return (demonsGroupedByCategory, "sucessfuly retrivied demons grouped by category");
        }
    }
}
