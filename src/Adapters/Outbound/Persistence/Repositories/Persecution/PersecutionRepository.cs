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

        public async Task<Entity.Persecution> CreatePersecution(Guid IdDemo, Guid IdSoul)
        {
            var demon = await _context.Demons.FirstOrDefaultAsync(d => d.IdDemon == IdDemo);
            var soul = await _context.Souls.FirstOrDefaultAsync(s => s.IdSoul == IdSoul);

            if (demon == null || soul == null)
                throw new ArgumentException("Demon or Soul not found.");

            var persecution = new Entity.Persecution { Demon = demon, Soul = soul };
            await _context.Persecution.AddAsync(persecution);
            await _context.SaveChangesAsync();
            return persecution;
        }

        public async Task<List<Entity.Persecution>> GetAllPersecutions()
        {
            var persecutions = await _context
                .Persecution.Include(p => p.Demon)
                .Include(p => p.Soul)
                .AsNoTracking()
                .ToListAsync();
            return persecutions;
        }

        public async Task<List<Entity.Persecution>> GetAllPersecutionWithFilter(
            Guid? idDemon,
            Guid? idSoul
        )
        {
            var query = _context.Persecution.AsNoTracking();
            if (idDemon.HasValue)
                query = query.Where(p => p.IdDemon == idDemon);
            if (idSoul.HasValue)
                query = query.Where(p => p.IdSoul == idSoul);
            return await query.ToListAsync();
        }
    }
}
