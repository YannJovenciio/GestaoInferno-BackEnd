using Entity = Inferno.src.Core.Domain.Entities;

namespace Inferno.src.Adapters.Outbound.Persistence.Repositories.Demon;

public interface IDemonRepository
{
    Task<Entity.Demon> GetByIdAsync(Guid id);
    Task<List<Entity.Demon>> GetAllAsync(int? pageSize, int? pageNumber);
    Task<List<Entity.Demon>> GetAllAsync();

    Task<List<Entity.Demon>> GetAllWithFiltersAsync(
        Guid? categoryId,
        string? name,
        DateTime? createdAt
    );
    Task<(List<Entity.Demon> demons, int totalItems)> GetRecomendations(
        int? pageSize,
        int? pageNumber
    );
}
