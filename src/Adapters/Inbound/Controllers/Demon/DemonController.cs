using Inferno.src.Core.Application.DTOs;
using Inferno.src.Core.Application.DTOs.Request.Demon;
using Inferno.src.Core.Domain.Interfaces.UseCases.Demon;
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

    public async Task<IActionResult> CreateDemon([FromBody] DemonInput input)
    {
        _logger.LogInformation($"received to CreateDemon DemonInput:{input}");

        if (input == null)
        {
            return BadRequest(new { message = "input Invalid" });
        }

        var (response, message) = await _demonUseCase.CreateAsync(input);
        _logger.LogInformation($"sucessfuly created demon");

        return new OkObjectResult(new { data = response, message });
    }

    [HttpPost("CreateMany")]
    public async Task<IActionResult> CreateMany(List<DemonInput> inputs)
    {
        _logger.LogInformation($"receveid request to create {inputs.Count} demons");
        if (inputs == null)
        {
            return BadRequest(new { message = "input Invalid" });
        }

        var (responses, message) = await _demonUseCase.CreateManyAsync(inputs);
        _logger.LogInformation($"sucessfuly created {responses.Count} demons");
        return new OkObjectResult(new { data = responses, message });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DemonResponse>> GetDemonById(Guid id)
    {
        _logger.LogInformation($"received request to Get with id:{id}");
        if (id == Guid.Empty)
            return BadRequest(new { message = "ID inválid" });

        var (response, message) = await _demonUseCase.GetByIdAsync(id);
        _logger.LogInformation($"sucessfuly found demon with id:{id}");
        return new OkObjectResult(new { data = response, message });
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation($"receveid reqeust to get all demons");
        var (response, message) = await _demonUseCase.GetAllAsync();
        _logger.LogInformation($"successfully found {response.Count} demons");
        return new OkObjectResult(new { data = response, message });
    }

    [HttpGet("GetAllF/")]
    public async Task<IActionResult> GetAllWithFilters(
        [FromQuery] string? name,
        [FromQuery] Guid? categoryId,
        [FromQuery] DateTime? createdAt
    )
    {
        _logger.LogInformation(
            $"receveid request do GetAllWithFilters with queries:{name ?? null},{categoryId ?? null},{createdAt ?? null}"
        );
        var (responses, message) = await _demonUseCase.GetAllWithFiltersAsync(
            categoryId ?? null,
            name ?? null,
            createdAt ?? null
        );
        return new OkObjectResult(new { data = responses, message });
    }
}
