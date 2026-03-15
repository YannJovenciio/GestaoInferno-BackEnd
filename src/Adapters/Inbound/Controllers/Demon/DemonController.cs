using Inferno.src.Adapters.Inbound.Controllers.Model;
using Inferno.src.Core.Application.UseCases.Demon;
using Microsoft.AspNetCore.Mvc;

namespace Inferno.src.Adapters.Inbound.Controllers.Demon;

[ApiController]
[Route("api/[controller]")]
public class DemonController : ControllerBase
{
    private readonly ILogger<DemonController> _logger;
    private readonly IDemonUseCase _demonUseCase;

    public DemonController(ILogger<DemonController> logger, IDemonUseCase demonUseCase)
    {
        _logger = logger;
        _demonUseCase = demonUseCase;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDemonById([FromRoute] Guid id)
    {
        _logger.LogInformation("received request to Get with id:{Id}", id);
        if (id == Guid.Empty)
        {
            return BadRequest(
                new APIResponse<DemonResponse> { Status = false, Message = "ID inválid" }
            );
        }

        var (response, message) = await _demonUseCase.GetByIdAsync(id);
        if (response == null)
        {
            _logger.LogWarning("demon with id:{Id} not found", id);
            return NotFound(new APIResponse<DemonResponse>(response!, message));
        }

        _logger.LogInformation("sucessfuly found demon with id:{Id}", id);
        return Ok(new APIResponse<DemonResponse>(response, message));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int? pageSize, [FromQuery] int? pageNumber)
    {
        _logger.LogInformation("receveid request to get all demons");
        var (response, message) = await _demonUseCase.GetAllAsync(pageSize, pageNumber);
        _logger.LogInformation("successfully found {DemonCount} demons", response?.Count ?? 0);
        return Ok(new APIResponse<List<DemonResponse>>(response, message));
    }

    [HttpGet("filter")]
    public async Task<IActionResult> GetAllWithFilters(
        [FromQuery] string? name,
        [FromQuery] Guid? categoryId,
        [FromQuery] DateTime? createdAt
    )
    {
        _logger.LogInformation(
            "received request GetAllWithFilters with name:{Name} categoryId:{CategoryId} createdAt:{CreatedAt}",
            name,
            categoryId,
            createdAt
        );
        var (responses, message) = await _demonUseCase.GetAllWithFiltersAsync(categoryId, name, createdAt);
        return Ok(new APIResponse<List<DemonResponse>>(responses!, message));
    }

    [HttpGet("OrderedByCategory")]
    public async Task<IActionResult> GetOrderedByCategory()
    {
        var (response, message) = await _demonUseCase.GetDemonByCategory();
        return Ok(new APIResponse<List<DemonOrderedByCategory>>(response, message));
    }
}
