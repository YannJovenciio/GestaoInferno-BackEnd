using Inferno.src.Adapters.Inbound.Controllers.Demon;

namespace Inferno.src.Core.Application.UseCases.Demon
{
    public interface IDemonUseCase
    {
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
        Task<(List<DemonOrderedByCategory> responses, string message)> GetDemonByCategory();
    }
}
