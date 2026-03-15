using Entity = Inferno.src.Core.Domain.Entities;

namespace Inferno.src.Adapters.Outbound.Persistence.HellTask;

public interface IHellTaskRepository
{
    Task<Entity.HellTask> CreateAsync(Entity.HellTask input, CancellationToken cancellationToken);
    Task<List<Entity.HellTask>> CreateManyAsync(
        List<Entity.HellTask> input,
        CancellationToken cancellationToken
    );
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Entity.HellTask>> GetAllByDemonIdAsync(
        int? pageSize,
        int? pageNumber,
        Guid id,
        CancellationToken cancellationToken
    );
    Task<Entity.HellTask> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}
