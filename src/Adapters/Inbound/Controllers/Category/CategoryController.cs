using Inferno.src.Core.Domain.Interfaces.UseCases.Category;
using Microsoft.AspNetCore.Mvc;

namespace Inferno.src.Adapters.Inbound.Controllers.Category
{
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly ICategoryUseCase _categoryUseCase;

        public CategoryController(
            ILogger<CategoryController> logger,
            ICategoryUseCase categoryUseCase
        )
        {
            _logger = logger;
            _categoryUseCase = categoryUseCase;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(
            [FromQuery] int? pageSize,
            [FromQuery] int? pageNumber
        )
        {
            _logger.LogInformation("received request to get all Categories");
            var (response, message) = await _categoryUseCase.ListAllCategory(pageSize, pageNumber);
            _logger.LogInformation($"sucessfuly retrieved:{response.Count} categories");
            return new OkObjectResult(
                new
                {
                    data = response,
                    message,
                    response.Count,
                }
            );
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromQuery] Guid id)
        {
            _logger.LogInformation($"received request to get Category with id:{id}");
            if (id == Guid.Empty)
                return new BadRequestObjectResult("Invalid id provided");
            var (response, message) = await _categoryUseCase.GetCategoryById(id);
            _logger.LogInformation($"sucessfuly found category with id:{id}");
            return new OkObjectResult(new { data = response, message });
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CategoryInput category)
        {
            _logger.LogInformation($"received request to Create Category:{category}");
            if (category == null || string.IsNullOrWhiteSpace(category.CategoryName))
                return new BadRequestObjectResult("Invalid input provided");
            var (response, message) = await _categoryUseCase.CreateCategory(category);
            _logger.LogInformation($"sucessfuly created category");
            return new OkObjectResult(new { data = response, message });
        }

        [HttpPost("CreateMany")]
        public async Task<IActionResult> CreateMany([FromBody] List<CategoryInput> inputs)
        {
            _logger.LogInformation($"received request to create {inputs.Count} categories");
            if (inputs == null || inputs.Count == 0)
                return new BadRequestObjectResult("Invalid input provided");

            if (inputs.Any(x => string.IsNullOrWhiteSpace(x.CategoryName)))
                return new BadRequestObjectResult("All categories must have a valid CategoryName");

            var (response, message) = await _categoryUseCase.CreateManyCategory(inputs);
            _logger.LogInformation($"sucessfuly created {response.Count} categories");
            return new OkObjectResult(new { data = response, message });
        }
    }
}
