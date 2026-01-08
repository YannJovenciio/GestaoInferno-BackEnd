using Inferno.src.Adapters.Outbound.Persistence;
using Inferno.src.Core.Domain.Entities;
using Inferno.src.Core.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

public class DemonRepository : IDemonRepository
{
    private readonly HellDbContext _context;

    public DemonRepository(HellDbContext context)
    {
        _context = context;
    }

    public async Task<Demon> GetByIdAsync(Guid id)
    {
        var demon = await _context.Demons.AsNoTracking().FirstOrDefaultAsync(d => d.IdDemon == id);
        return demon;
    }

    public async Task<Demon> CreateAsync(Demon input)
    {
        Console.WriteLine("Creating demon in repository com IDCATEGORY", input.CategoryId);
        await _context.Demons.AddAsync(input);
        await _context.SaveChangesAsync();
        return input;
    }

    public async Task<List<Demon>> CreateManyAsync(List<Demon> inputs)
    {
        await _context.AddRangeAsync(inputs);
        await _context.SaveChangesAsync();
        return inputs;
    }

    public async Task<List<Demon>> GetAllAsync()
    {
        var demons = await _context.Demons.AsNoTracking().ToListAsync();
        return demons;
    }

    public async Task<List<Demon>> GetAllWithFiltersAsync(Guid categoryId)
    {
        var demons = await _context
            .Demons.AsNoTracking()
            .Where(d => d.CategoryId == categoryId)
            .ToListAsync();
        return demons;
    }

    public async Task<List<Demon>> GetAllWithFiltersAsync(
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
}
