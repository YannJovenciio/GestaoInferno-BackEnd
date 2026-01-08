using Inferno.src.Core.Domain.Interfaces.Repository.Category;
using Microsoft.EntityFrameworkCore;

namespace Inferno.src.Adapters.Outbound.Persistence.Repositories.Category;

public class CategoryRepository : ICategoryRepository
{
    private readonly HellDbContext _context;

    public CategoryRepository(HellDbContext context)
    {
        _context = context;
    }

    public async Task<Core.Domain.Entities.Category> CreateCategory(
        Core.Domain.Entities.Category category
    )
    {
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task<List<Core.Domain.Entities.Category>> CreateManyCategory(
        List<Core.Domain.Entities.Category> categories
    )
    {
        await _context.Categories.AddRangeAsync(categories);
        await _context.SaveChangesAsync();
        return categories;
    }

    public async Task<Core.Domain.Entities.Category> GetCategoryById(Guid id)
    {
        var category = await _context
            .Categories.AsNoTracking()
            .FirstOrDefaultAsync(c => c.IdCategory == id);
        return category;
    }

    public async Task<List<Core.Domain.Entities.Category>> ListAllCategory()
    {
        var categories = _context.Categories.AsNoTracking().ToListAsync();
        return await categories;
    }
}
