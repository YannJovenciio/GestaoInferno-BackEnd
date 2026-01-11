using Inferno.src.Adapters.Inbound.Controllers.Category;
using Inferno.src.Core.Domain.Interfaces.Repository.Category;
using Inferno.src.Core.Domain.Interfaces.UseCases.Category;
using Entity = Inferno.src.Core.Domain.Entities;

namespace Inferno.src.Core.Application.UseCases.Category
{
    public class CategoryUseCase : ICategoryUseCase
    {
        private readonly ICategoryRepository _context;
        private readonly ILogger<CategoryUseCase> _logger;

        public CategoryUseCase(ICategoryRepository context, ILogger<CategoryUseCase> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<(CategoryResponse? response, string message)> CreateCategory(
            CategoryInput input
        )
        {
            var category = new Entity.Category() { CategoryName = input.CategoryName };
            await _context.CreateCategory(category);
            return (
                new CategoryResponse(category.IdCategory, category.CategoryName),
                "Category created successfully"
            );
        }

        public async Task<(List<CategoryResponse>? response, string message)> CreateManyCategory(
            List<CategoryInput> input
        )
        {
            _logger.LogInformation($"Receveid request to create {input.Concat} categories");
            var categories = input
                .Select(i => new Entity.Category() { CategoryName = i.CategoryName })
                .ToList();
            await _context.CreateManyCategory(categories);
            var response = categories
                .Select(c => new CategoryResponse(c.IdCategory, c.CategoryName))
                .ToList();
            _logger.LogInformation($"Created {response.Count} categories sucessfuly");

            return (response, $"{response.Count} categories created successfully");
        }

        public async Task<(CategoryResponse? response, string message)> GetCategoryById(Guid id)
        {
            _logger.LogInformation($"Receveid request to get category with id:{id}");
            var category = await _context.GetCategoryById(id);
            if (category == null)
                return (null, "Category not found");
            _logger.LogInformation($"Successfully created category with {category.CategoryName}");
            return (
                new CategoryResponse(category.IdCategory, category.CategoryName),
                "Successfully found category"
            );
        }

        public async Task<(List<CategoryResponse>? responses, string message)> ListAllCategory(
            int? pageSize,
            int? pageNumber
        )
        {
            _logger.LogInformation("Receveid request to list all categories");
            var categories = await _context.ListAllCategory(pageSize, pageNumber);
            if (categories == null || categories.Count == 0)
                return (null, $"{categories.Count} Categories found");
            var response = categories
                .Select(c => new CategoryResponse(c.IdCategory, c.CategoryName))
                .ToList();
            _logger.LogInformation($"sucessfuly retrieved {response.Count} categories");
            return (response, $"Successfully retrieved {response.Count} categories");
        }
    }
}
