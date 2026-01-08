using Entity = Inferno.src.Core.Domain.Entities;

namespace Inferno.src.Core.Domain.Interfaces.Repository.Category
{
    public interface ICategoryRepository
    {
        Task<Entity.Category> GetCategoryById(Guid id);
        Task<List<Entity.Category>> ListAllCategory();
        Task<Entity.Category> CreateCategory(Entity.Category category);
        Task<List<Entity.Category>> CreateManyCategory(List<Entity.Category> categories);
    }
}
