using Inferno.src.Core.Domain.Enums;
using Inferno.src.Core.Domain.Interfaces.Repository.Sin;
using Microsoft.EntityFrameworkCore;
using Entity = Inferno.src.Core.Domain.Entities;

namespace Inferno.src.Adapters.Outbound.Persistence.Repositories.Sin
{
    public class SinRepository : ISinRepository
    {
        private readonly HellDbContext _context;

        public SinRepository(HellDbContext context)
        {
            _context = context;
        }

        public async Task<Entity.Sin> Create(Entity.Sin sin)
        {
            await _context.Sins.AddAsync(sin);
            await _context.SaveChangesAsync();
            return sin;
        }

        public async Task<Entity.Sin> Delete(Guid idSin)
        {
            var sin = await _context.Sins.FirstOrDefaultAsync(s => s.IdSin == idSin);
            await _context.Sins.Where(s => s.IdSin == idSin).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
            return sin ?? null!;
        }

        public async Task<List<Entity.Sin>> GetAll()
        {
            var sins = await _context.Sins.AsNoTracking().ToListAsync();
            return sins;
        }

        public async Task<List<Entity.Sin>> GetAllWithFilters(Guid? IdSIn, Severity? severity)
        {
            var query = _context.Sins.AsNoTracking();
            if (IdSIn.HasValue)
                query = query.Where(s => s.IdSin == IdSIn);
            if (severity.HasValue)
                query = query.Where(s => s.SinSeverity == severity);
            return await query.ToListAsync();
        }

        public async Task<Entity.Sin> GetById(Guid idSin)
        {
            var sin = await _context.Sins.AsNoTracking().FirstOrDefaultAsync(s => s.IdSin == idSin);
            return sin ?? null!;
        }

        public async Task<Entity.Sin> Update(Guid idSin, Core.Domain.Entities.Sin sin)
        {
            // var si = await _context.Sins.ExecuteUpdateAsync(s => s.SetProperty(e => e.IdSin = sin.IdSin));
            throw new NotImplementedException();
        }

        public async Task<List<Entity.Sin>> CreateMany(List<Entity.Sin> sins)
        {
            await _context.Sins.AddRangeAsync(sins);
            await _context.SaveChangesAsync();
            return sins;
        }
    }
}
