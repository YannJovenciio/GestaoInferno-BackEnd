using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inferno.src.Core.Application.DTOs.Response;
using Inferno.src.Core.Domain.Entities;
using Inferno.src.Core.Domain.Interfaces.Persecution;
using Microsoft.EntityFrameworkCore;
using Entity = Inferno.src.Core.Domain.Entities.ManyToMany;

namespace Inferno.src.Adapters.Outbound.Persistence.Repositories.Persecution
{
    public class PersecutionRepository : IPersecutionRepository
    {
        private readonly ILogger<PersecutionRepository> _logger;
        private readonly HellDbContext _context;

        public PersecutionRepository(ILogger<PersecutionRepository> logger, HellDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<(Entity.Persecution, string message)> CreatePersecution(
            Guid IdDemo,
            Guid IdSoul
        )
        {
            _logger.LogInformation(
                $"Received request to create Persecution for demonGuid:{IdDemo} and Soul:{IdSoul}"
            );
            var demon = await _context.Demons.FirstOrDefaultAsync(d => d.IdDemon == IdDemo);
            var soul = await _context.Souls.FirstOrDefaultAsync(s => s.IdSoul == IdSoul);

            var shouldCreatePersecution = demon != null && soul != null;
            if (!shouldCreatePersecution)
            {
                return (
                    new Entity.Persecution(),
                    $"No demon and soul found for the provided ids:{IdDemo},{IdSoul}"
                );
            }

            var persecution = new Entity.Persecution(demon, soul);
            _logger.LogInformation($"Created new Persecution entity {persecution}");
            return (persecution, "Persecution created sucessfuly");
        }

        public async Task<(
            List<Entity.Persecution> persecutions,
            string message
        )> GetAllPersecutions()
        {
            var persecutions = await _context.Persecution.ToListAsync();

            if (persecutions == null || persecutions.Count == 0)
            {
                _logger.LogInformation("No persecutions found in database");
                return (new List<Entity.Persecution>(), "No persecutions found");
            }

            _logger.LogInformation($"Retrieved {persecutions.Count} persecutions from database");
            return (persecutions, "Persecutions found successfully");
        }
    }
}
