using Inferno.src.Core.Application.DTOs;
using Inferno.src.Core.Application.DTOs.Request.Demon;
using Inferno.src.Core.Domain.Entities;

namespace Inferno.src.Core.Domain.Interfaces
{
    public interface IDemonRepository
    {
        Task<Demon> CreateAsync(Demon input);
        Task<List<Demon>> CreateManyAsync(List<Demon> inputs);
        Task<Demon> GetByIdAsync(Guid id);
    }
}
