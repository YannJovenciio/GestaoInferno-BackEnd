using Inferno.src.Core.Domain.Entities;

namespace Inferno.src.Core.Domain.Interfaces
{
    public interface IDemonRepository
    {
        Task<Demon> CreateAsync(Demon input);
        Task<List<Demon>> CreateManyAsync(List<Demon> inputs);
        Task<Demon> GetByIdAsync(Guid id);
        Task<List<Demon>> GetAllAsync(int? pageSize, int? pageNumber);
        Task<List<Demon>> GetAllWithFiltersAsync(
            Guid? categoryId,
            string? name,
            DateTime? createdAt
        );
    }
}
