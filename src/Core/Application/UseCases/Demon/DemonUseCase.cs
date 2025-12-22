using Inferno.src.Adapters.Outbound.Persistence;
using Inferno.src.Core.Application.DTOs;
using Inferno.src.Core.Application.DTOs.Request.Demon;
using Inferno.src.Core.Domain.Interfaces;
using Inferno.src.Core.Domain.Interfaces.UseCases.Demon;
using Microsoft.EntityFrameworkCore;
using Entity = Inferno.src.Core.Domain.Entities;

namespace Inferno.src.Core.Application.UseCases.Demon
{
    public class DemonUseCase : IDemonUseCase
    {
        private readonly IDemonRepository _context;
        private readonly HellDbContext _dbContext;

        public DemonUseCase(IDemonRepository context, HellDbContext dbContext)
        {
            _context = context;
            _dbContext = dbContext;
        }

        public async Task<(DemonResponse, string message)> CreateAsync(DemonInput input)
        {
            // Validar se a Category existe
            var categoryExists = await _dbContext.Categories.AnyAsync(c =>
                c.IdCategoria == input.CategoryId
            );
            if (!categoryExists)
            {
                return (null, $"Category with ID {input.CategoryId} does not exist");
            }

            var demon = new Entity.Demon(input.DemonName, input.CategoryId);
            await _context.CreateAsync(demon);
            return (new DemonResponse(demon.DemonName, demon.Category), "Demon created sucessfuly");
        }

        public async Task<(List<DemonResponse>, string message)> CreateManyAsync(
            DemonInput[] inputs
        )
        {
            if (inputs == null || inputs.Length == 0)
            {
                return (null, "No demons provided");
            }
            var demon = inputs.Select(d => new Entity.Demon(d.DemonName, d.CategoryId)).ToList();
            await _context.CreateManyAsync(demon);
            List<DemonResponse> responses = demon
                .Select(d => new DemonResponse(d.DemonName, d.Category))
                .ToList();
            return (responses, "Criado com sucesso");
        }

        public async Task<(DemonResponse, string message)> GetByIdAsync(Guid id)
        {
            var demon = await _context.GetByIdAsync(id);
            if (demon == null)
            {
                return (null, $"Demon with id {id} not found");
            }
            DemonResponse response = new(demon.DemonName, demon.Category);
            return (response, "Demon found sucessfuly");
        }
    }
}
