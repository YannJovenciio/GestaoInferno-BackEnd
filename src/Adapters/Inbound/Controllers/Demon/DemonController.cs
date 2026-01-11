using Inferno.src.Adapters.Inbound.Controllers.Model;
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

    [HttpPost]
    public async Task<IActionResult> CreateDemon([FromBody] DemonInput input)
    {
        _logger.LogInformation($"received to CreateDemon DemonInput:{input}");

        if (input == null)
        {
            return BadRequest(
                new ResponseModel<DemonResponse> { Status = false, Message = "input Invalid" }
            );
        }

        var (response, message) = await _demonUseCase.CreateAsync(input);
        _logger.LogInformation($"sucessfuly created demon");

        return CreatedAtAction(
            nameof(GetDemonById),
            new { id = response},
            new ResponseModel<DemonResponse>
            {
                Status = true,
                Message = message,
                Data = response,
            }
        );
    }

    [HttpPost("CreateMany")]
    public async Task<IActionResult> CreateMany([FromBody] List<DemonInput> inputs)
    {
        _logger.LogInformation($"receveid request to create {inputs.Count} demons");
        if (inputs == null || inputs.Count == 0)
        {
            return BadRequest(
                new ResponseModel<List<DemonResponse>> { Status = false, Message = "input Invalid" }
            );
        }

        var (responses, message) = await _demonUseCase.CreateManyAsync(inputs);
        _logger.LogInformation($"sucessfuly created {responses.Count} demons");
        return Ok(
            new ResponseModel<List<DemonResponse>>
            {
                Status = true,
                Message = message,
                Data = responses,
            }
        );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDemonById([FromRoute] Guid id)
    {
        _logger.LogInformation($"received request to Get with id:{id}");
        if (id == Guid.Empty)
        {
            return BadRequest(
                new ResponseModel<DemonResponse> { Status = false, Message = "ID inválid" }
            );
        }

        var (response, message) = await _demonUseCase.GetByIdAsync(id);
        if (response == null)
        {
            _logger.LogWarning($"demon with id:{id} not found");
            return NotFound(
                new ResponseModel<DemonResponse> { Status = false, Message = "Demon not found" }
            );
        }

        _logger.LogInformation($"sucessfuly found demon with id:{id}");
        return Ok(
            new ResponseModel<DemonResponse>
            {
                Status = true,
                Message = message,
                Data = response,
            }
        );
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int? pageSize, [FromQuery] int? pageNumber)
    {
        _logger.LogInformation($"receveid request to get all demons");
        var (response, message) = await _demonUseCase.GetAllAsync(pageSize, pageNumber);
        _logger.LogInformation($"successfully found {response.Count} demons");
        return Ok(
            new ResponseModel<List<DemonResponse>>
            {
                Status = true,
                Message = message,
                Data = response,
            }
        );
    }

    [HttpGet("filter")]
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
        return Ok(
            new ResponseModel<List<DemonResponse>>
            {
                Status = true,
                Message = message,
                Data = responses,
            }
        );
    }
}
