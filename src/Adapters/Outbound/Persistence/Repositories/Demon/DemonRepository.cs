using Inferno.src.Adapters.Outbound.Persistence;
using Inferno.src.Core.Application.DTOs;
using Inferno.src.Core.Application.DTOs.Request.Demon;
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
        var demon = _context.Demons.FirstOrDefaultAsync(d => d.IdDemon == id);
        return await demon;
    }

    public async Task<Demon> CreateAsync(Demon input)
    {
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
}
