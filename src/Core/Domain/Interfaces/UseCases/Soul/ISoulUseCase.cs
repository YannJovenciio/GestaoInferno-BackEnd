using Inferno.src.Core.Application.DTOs.Request.Soul;
using Inferno.src.Core.Application.DTOs.Response.Soul;

namespace Inferno.src.Core.Domain.Interfaces.UseCases.Soul
{
    public interface ISoulUseCase
    {
        Task<(SoulResponse? response, string message)> GetSoulByIdAsync(Guid id);
        Task<(List<SoulResponse>? response, string message)> GetAllSoulsAsync();
        Task<(List<SoulResponse>? response, string message)> CreateManySoulsAsync(
            List<SoulRequest> requests
        );
        Task<(SoulResponse? response, string message)> CreateSoul(SoulRequest request);
        Task<(List<SoulResponse>? responses, string message)> GetAllSoulsWithFilters(
            Guid? cavernId,
            HellEnum? level,
            string? description
        );
    }
}
