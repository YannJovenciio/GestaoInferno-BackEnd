using Inferno.src.Adapters.Inbound.Controllers.Category;

namespace Inferno.src.Core.Domain.Interfaces.UseCases.Category
{
    public interface ICategoryUseCase
    {
        Task<(CategoryResponse? response, string message)> GetCategoryById(Guid id);
        Task<(List<CategoryResponse>? responses, string message)> ListAllCategory();
        Task<(CategoryResponse? response, string message)> CreateCategory(CategoryInput input);
        Task<(List<CategoryResponse>? response, string message)> CreateManyCategory(
            List<CategoryInput> input
        );
    }
}
