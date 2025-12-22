using Inferno.src.Adapters.Outbound.Persistence;
using Inferno.src.Core.Application.DTOs;
using Inferno.src.Core.Application.DTOs.Response;
using Inferno.src.Core.Domain.Entities.ManyToMany;
using Inferno.src.Core.Domain.Interfaces.UseCases;
using Microsoft.EntityFrameworkCore;

namespace Inferno.src.Core.Application.UseCases.Demon
{
    public class PersecutionUseCase : IPersecutionUseCase
    {
        private readonly ILogger<PersecutionUseCase> _logger;
        private readonly HellDbContext _context;

        public PersecutionUseCase(HellDbContext context)
        {
            _context = context;
        }

        public async Task<(CreatePersecutionResponse, string message)> CreatePersecution(
            Guid IdDemo,
            Guid IdSoul
        )
        {
            _logger.LogInformation(
                $"Received request to create Persecution for demonGuid:{IdDemo} and Soul:{IdSoul}"
            );
            var demon = await _context.Demons.FirstOrDefaultAsync(d => d.IdDemon == IdDemo);
            var soul = await _context.Souls.FirstOrDefaultAsync(s => s.IdSoul == IdSoul);

            var shouldCreatePersecution = demon != null && soul != null ? true : false;
            if (!shouldCreatePersecution)
            {
                return (null, $"No demon and soul found for the provided ids:{IdDemo},{IdSoul}");
            }

            var persecution = new Persecution(demon, soul);
            _logger.LogInformation($"Created new Persecution entity {persecution}");

            var response = new CreatePersecutionResponse(demon, soul);
            return (response, "Persecution created sucessfuly");
        }

        public Task<(List<DemonResponse> responses, string message)> GetManyPersecutions()
        {
            throw new NotImplementedException();
        }
    }
}
