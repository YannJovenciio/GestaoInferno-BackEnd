using Inferno.src.Adapters.Inbound.Controllers.Model;
using Inferno.src.Core.Application.UseCases.HellTask;
using Microsoft.AspNetCore.Mvc;

namespace Inferno.src.Adapters.Inbound.Controllers.HellTask;

[ApiController]
[Route("api/[controller]")]
public class HellTaskController : ControllerBase
{
    private readonly IHellTaskUseCase _hellTaskUseCase;
    private readonly ILogger<HellTaskController> _logger;
    private readonly string errorMessage = "Invalid input provided";

    public HellTaskController(ILogger<HellTaskController> logger, IHellTaskUseCase hellTaskUseCase)
    {
        _logger = logger;
        _hellTaskUseCase = hellTaskUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(
        [FromBody] HellTaskInput input,
        CancellationToken cancellationToken
    )
    {
        if (input == null)
            return BadRequest(new APIResponse<string>(errorMessage));

        var (response, message) = await _hellTaskUseCase.CreateAsync(input, cancellationToken);
        _logger.LogInformation(
            "Created HellTask {Id} for Demon {DemonId}",
            response.HellTaskId,
            input.DemonId
        );

        return CreatedAtAction(
            "GetById",
            new { id = response.HellTaskId },
            new APIResponse<HellTaskResponse>(response, message)
        );
    }

    [HttpPost("CreateMany")]
    public async Task<IActionResult> CreateManyAsync(
        [FromBody] List<HellTaskInput> input,
        CancellationToken cancellationToken
    )
    {
        if (input == null || input.Count == 0)
            return BadRequest(new APIResponse<string>(errorMessage));

        var (response, message) = await _hellTaskUseCase.CreateManyAsync(input, cancellationToken);
        _logger.LogInformation("Created {Count} HellTasks", response.Count);

        return CreatedAtAction(
            nameof(GetAllByDemonIdAsync),
            new { demonId = input[0].DemonId },
            new APIResponse<List<HellTaskResponse>>(response, message)
        );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        if (id == Guid.Empty)
            return BadRequest(new APIResponse<string>(errorMessage));

        var (response, message) = await _hellTaskUseCase.DeleteAsync(id, cancellationToken);
        _logger.LogInformation("Deleted HellTask {Id}", id);
        return Ok(new APIResponse<Guid>(response, message));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllByDemonIdAsync(
        [FromQuery(Name = "demonId")] Guid demonId,
        [FromQuery] int? pageSize,
        [FromQuery] int? pageNumber,
        CancellationToken cancellationToken
    )
    {
        if (demonId == Guid.Empty)
            return BadRequest(new APIResponse<string>(errorMessage));

        var (response, message) = await _hellTaskUseCase.GetAllByDemonIdAsync(
            pageSize,
            pageNumber,
            demonId,
            cancellationToken
        );
        return Ok(new APIResponse<List<HellTaskResponse>>(response, message));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        if (id == Guid.Empty)
            return BadRequest(new APIResponse<string>(errorMessage));

        var (response, message) = await _hellTaskUseCase.GetByIdAsync(id, cancellationToken);
        return Ok(new APIResponse<HellTaskResponse>(response, message));
    }
}
