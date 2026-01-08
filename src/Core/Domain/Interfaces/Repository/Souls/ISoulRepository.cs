using Inferno.src.Core.Domain.Entities;

namespace Inferno.src.Core.Domain.Interfaces.Repository.Souls
{
    public interface ISoulRepository
    {
        Task<Soul> GetByIdAsync(Guid id);
        Task<List<Soul>> GetAllAsync();
        Task<List<Soul>> CreateManyAsync(List<Soul> souls);
        Task<Soul> CreateAsync(Soul soul);
        Task<List<Soul>> GetAllWithFilterAsync(
            Guid? cavernId,
            HellEnum? level,
            string? description
        );
    }
}
