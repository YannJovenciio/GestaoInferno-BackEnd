using Inferno.src.Core.Application.DTOs;
using Inferno.src.Core.Application.DTOs.Request.Demon;

namespace Inferno.src.Core.Domain.Interfaces.UseCases.Demon
{
    public interface IDemonUseCase
    {
        Task<(DemonResponse? response, string message)> CreateAsync(DemonInput input);
        Task<(List<DemonResponse>? responses, string message)> CreateManyAsync(
            List<DemonInput> inputs
        );
        Task<(DemonResponse? response, string message)> GetByIdAsync(Guid id);
        Task<(List<DemonResponse>? responses, string message)> GetAllAsync(
            int? pageSize,
            int? pageNumber
        );
        Task<(List<DemonResponse>? responses, string message)> GetAllWithFiltersAsync(
            Guid? categoryId,
            string? name,
            DateTime? createdAt
        );
    }
}
