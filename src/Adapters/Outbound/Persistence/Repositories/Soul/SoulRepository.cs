using Inferno.src.Core.Domain.Interfaces.Repository.Souls;
using Microsoft.EntityFrameworkCore;
using Entity = Inferno.src.Core.Domain.Entities;

namespace Inferno.src.Adapters.Outbound.Persistence.Repositories.Soul;

public class SoulRepository : ISoulRepository
{
    private readonly HellDbContext _context;

    public SoulRepository(HellDbContext context)
    {
        _context = context;
    }

    public async Task<Entity.Soul> CreateAsync(Entity.Soul soul)
    {
        await _context.Souls.AddAsync(soul);
        await _context.SaveChangesAsync();
        return soul;
    }

    public async Task<List<Entity.Soul>> CreateManyAsync(List<Entity.Soul> souls)
    {
        await _context.Souls.AddRangeAsync(souls);
        await _context.SaveChangesAsync();
        return souls;
    }

    public async Task<List<Entity.Soul>> GetAllAsync()
    {
        var souls = await _context.Souls.AsNoTracking().ToListAsync();
        return souls;
    }

    public async Task<Entity.Soul> GetByIdAsync(Guid id)
    {
        var soul = await _context.Souls.AsNoTracking().FirstOrDefaultAsync(s => s.IdSoul == id);
        return soul;
    }

    public async Task<List<Entity.Soul>> GetAllWithFilterAsync(
        Guid? cavernId,
        HellEnum? level,
        string? description
    )
    {
        var query = _context.Souls.AsNoTracking();

        if (cavernId.HasValue)
            query = query.Where(d => d.CavernId == cavernId);
        if (level != null)
            query = query.Where(d => d.Level == level);
        if (!string.IsNullOrEmpty(description))
            query = query.Where(d => d.Description.Contains(description));

        return await query.ToListAsync();
    }

    public async Task<List<Entity.Soul>> GetAllWithSins()
    {
        var souls = await _context
            .Souls.Include(s => s.Realizes)
            .Include(s => s.Cavern)
            .Include(s => s.Persecutions)
                .ThenInclude(p => p.Demon)
            .ToListAsync();
        return souls;
    }
}
