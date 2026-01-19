using Inferno.src.Core.Domain.Enums;
using Entity = Inferno.src.Core.Domain.Entities;

namespace Inferno.src.Core.Domain.Interfaces.Repository.Sin
{
    public interface ISinRepository
    {
        Task<List<Entity.Sin>> GetAll();
        Task<List<Entity.Sin>> GetAllWithFilters(Guid? IdSIn, Severity? severity);
        Task<Entity.Sin> Create(Entity.Sin sin);
        Task<Entity.Sin> GetById(Guid idSin);
        Task<Entity.Sin> Delete(Guid idSin);
        Task<Entity.Sin> Update(Guid idSin, Entity.Sin sin);
        Task<List<Entity.Sin>> CreateMany(List<Entity.Sin> sins);
    }
}
