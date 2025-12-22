using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inferno.src.Core.Application.DTOs;
using Inferno.src.Core.Application.DTOs.Request.Demon;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace Inferno.src.Core.Domain.Interfaces.UseCases.Demon
{
    public interface IDemonUseCase
    {
        Task<(DemonResponse, string message)> CreateAsync(DemonInput input);
        Task<(List<DemonResponse>, string message)> CreateManyAsync(DemonInput[] inputs);
        Task<(DemonResponse, string message)> GetByIdAsync(Guid id);
    }
}
