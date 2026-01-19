using Inferno.src.Adapters.Inbound.Controllers.Model;
using Inferno.src.Core.Application.DTOs.Request.Soul;
using Inferno.src.Core.Application.DTOs.Response.Soul;
using Inferno.src.Core.Domain.Interfaces.UseCases.Soul;
using Microsoft.AspNetCore.Mvc;

namespace Inferno.src.Adapters.Inbound.Controllers.Soul;

[ApiController]
[Route("api/[controller]")]
public class SoulController : ControllerBase
{
    private readonly ILogger<SoulController> _logger;
    private readonly ISoulUseCase _soulUseCase;

    public SoulController(ILogger<SoulController> logger, ISoulUseCase soulUseCase)
    {
        _logger = logger;
        _soulUseCase = soulUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SoulRequest request)
    {
        _logger.LogInformation("received request to create Soul");
        if (!ModelState.IsValid)
        {
            return BadRequest(new APIResponse<SoulResponse>("invalid input provided"));
        }

        var (response, message) = await _soulUseCase.CreateSoul(request);
        return CreatedAtAction(
            nameof(Create),
            new { id = response.IdSoul },
            new APIResponse<SoulResponse>(response, message)
        );
    }

    [HttpPost("bulk")]
    public async Task<IActionResult> CreateMany([FromBody] List<SoulRequest> inputs)
    {
        _logger.LogInformation($"received request to create {inputs?.Count ?? 0} souls");
        if (inputs == null || inputs.Count == 0)
        {
            return BadRequest(new APIResponse<List<SoulResponse>>("invalid inputs provided"));
        }

        var (response, message) = await _soulUseCase.CreateManySoulsAsync(inputs);
        _logger.LogInformation($"successfully created {response.Count} souls");
        return Ok(new APIResponse<List<SoulResponse>>(response, message));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("received request to get all Souls");
        var (response, message) = await _soulUseCase.GetAllSoulsAsync();
        _logger.LogInformation($"successfully retrieved {response.Count} souls");
        return Ok(new APIResponse<List<SoulResponse>>(response, message));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        _logger.LogInformation($"received request to get Soul with id:{id}");
        if (id == Guid.Empty)
        {
            return BadRequest(new APIResponse<SoulResponse>("invalid id provided"));
        }

        var (response, message) = await _soulUseCase.GetSoulByIdAsync(id);
        if (response == null)
        {
            _logger.LogWarning($"soul with id:{id} not found");
            return NotFound(new APIResponse<SoulResponse>("Soul not found"));
        }

        _logger.LogInformation($"successfully found soul with id:{id}");
        return Ok(new APIResponse<SoulResponse>(response, message));
    }

    [HttpGet("filter")]
    public async Task<IActionResult> GetAllWithFilters(
        [FromQuery] Guid? cavernId,
        [FromQuery] string? description,
        [FromQuery] HellEnum? level
    )
    {
        _logger.LogInformation(
            $"received request to filter souls with cavernId:{cavernId}, level:{level}, description:{description}"
        );
        var (response, message) = await _soulUseCase.GetAllSoulsWithFilters(
            cavernId,
            level,
            description
        );
        return Ok(new APIResponse<List<SoulResponse>>(response, message));
    }
}
