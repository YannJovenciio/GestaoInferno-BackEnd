using Microsoft.EntityFrameworkCore;
using Entity = Inferno.src.Core.Domain.Entities;

namespace Inferno.src.Adapters.Outbound.Persistence.Repositories.Demon;

public class DemonRepository : IDemonRepository
{
    private readonly HellDbContext _context;

    public DemonRepository(HellDbContext context)
    {
        _context = context;
    }

    public async Task<Entity.Demon> GetByIdAsync(Guid id)
    {
        var demon = await _context.Demons.AsNoTracking().FirstOrDefaultAsync(d => d.IdDemon == id);
        return demon;
    }

    public async Task<List<Entity.Demon>> GetAllAsync(int? pageSize, int? pageNumber)
    {
        var demons = await _context
            .Demons.AsNoTracking()
            .OrderBy(d => d.DemonName)
            .Skip(((pageNumber ?? 1) - 1) * (pageSize ?? 10))
            .Take(pageSize ?? 10)
            .ToListAsync();

        return demons;
    }

    public async Task<List<Entity.Demon>> GetAllAsync()
    {
        var demons = await _context.Demons.AsNoTracking().OrderBy(d => d.DemonName).ToListAsync();
        return demons;
    }

    public async Task<List<Entity.Demon>> GetAllWithFiltersAsync(
        Guid? categoryId,
        string? name,
        DateTime? createdAt
    )
    {
        var query = _context.Demons.AsNoTracking();

        if (categoryId.HasValue)
            query = query.Where(d => d.CategoryId == categoryId);
        if (!string.IsNullOrEmpty(name))
            query = query.Where(d => d.DemonName.Contains(name));
        if (createdAt.HasValue)
            query = query.Where(d => d.CreatedAt.Date == createdAt.Value.Date);

        return await query.ToListAsync();
    }

    public async Task<(List<Entity.Demon> demons, int totalItems)> GetRecomendations(
        int? pageSize,
        int? pageNumber
    )
    {
        var demons = await _context
            .Demons.Include(d => d.Persecutions)
                .ThenInclude(p => p.Soul)
            .Include(x => x.Category)
            .Skip(((pageNumber ?? 1) - 1) * (pageSize ?? 10))
            .Take(pageSize ?? 10)
            .OrderBy(d => d.DemonName)
            .ToListAsync();
        var totalItems = await _context.Demons.CountAsync();

        return (demons, totalItems);
    }
}
