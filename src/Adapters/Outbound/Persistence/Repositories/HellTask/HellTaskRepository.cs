using Inferno.src.Adapters.Outbound.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Entity = Inferno.src.Core.Domain.Entities;

namespace Inferno.src.Adapters.Outbound.Persistence.HellTask;

public class HellTaskRepository(HellDbContext context, ILogger<HellTaskRepository> logger)
    : IHellTaskRepository
{
    private readonly HellDbContext _context = context;
    private readonly ILogger<HellTaskRepository> _logger = logger;

    public async Task<Entity.HellTask> CreateAsync(
        Entity.HellTask input,
        CancellationToken cancellationToken
    )
    {
        await _context.HellTasks.AddAsync(input, cancellationToken);
        await _context.SaveChangesAsync();
        return input;
    }

    public async Task<List<Entity.HellTask>> CreateManyAsync(
        List<Entity.HellTask> input,
        CancellationToken cancellationToken
    )
    {
        await _context.HellTasks.AddRangeAsync(input, cancellationToken);
        await _context.SaveChangesAsync();
        return input;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await _context
            .HellTasks.Where(h => h.HellTaskId == id)
            .ExecuteDeleteAsync(cancellationToken);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Entity.HellTask>> GetAllByDemonIdAsync(
        int? pageSize,
        int? pageNumber,
        Guid id,
        CancellationToken cancellationToken
    )
    {
        var size = pageSize ?? 10;
        var page = pageNumber ?? 1;

        _logger.LogDebug(
            "GetAllByDemonIdAsync called with id={Id} (ToString={IdStr}) page={Page} size={Size}",
            id,
            id.ToString(),
            page,
            size
        );

        return await _context
            .HellTasks.AsNoTracking()
            .Where(h => h.DemonId == id)
            .OrderBy(h => h.CreatedAt)
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync(cancellationToken);
    }

    public async Task<Entity.HellTask> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var task = await _context
            .HellTasks.AsNoTracking()
            .FirstOrDefaultAsync(x => x.HellTaskId == id, cancellationToken);

        return task!;
    }
}
