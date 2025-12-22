using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Threading.Tasks;
using Inferno.src.Core.Application.DTOs;
using Inferno.src.Core.Application.DTOs.Response;

namespace Inferno.src.Core.Domain.Interfaces.UseCases
{
    public interface IPersecutionUseCase
    {
        Task<(CreatePersecutionResponse, string message)> CreatePersecution(
            Guid IdDemo,
            Guid IdSoul
        );
        Task<(List<DemonResponse> responses, string message)> GetManyPersecutions();
    }
}
