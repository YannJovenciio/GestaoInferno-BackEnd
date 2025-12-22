using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inferno.src.Core.Application.DTOs.Response;
using Entity = Inferno.src.Core.Domain.Entities.ManyToMany;

namespace Inferno.src.Core.Domain.Interfaces.Persecution
{
    public interface IPersecutionRepository
    {
        Task<(Entity.Persecution, string message)> CreatePersecution(Guid IdDemo, Guid IdSoul);
        Task<(List<Entity.Persecution> persecutions, string message)> GetAllPersecutions();
    }
}
