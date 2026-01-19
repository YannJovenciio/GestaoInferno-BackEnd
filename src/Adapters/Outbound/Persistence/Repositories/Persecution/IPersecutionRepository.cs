using Entity = Inferno.src.Core.Domain.Entities.ManyToMany;

namespace Inferno.src.Core.Domain.Interfaces.Persecution
{
    public interface IPersecutionRepository
    {
        Task<Entity.Persecution> CreatePersecution(Guid IdDemo, Guid IdSoul);
        Task<List<Entity.Persecution>> GetAllPersecutions();
        Task<List<Entity.Persecution>> GetAllPersecutionWithFilter(Guid? idDemon, Guid? idSoul);
    }
}
