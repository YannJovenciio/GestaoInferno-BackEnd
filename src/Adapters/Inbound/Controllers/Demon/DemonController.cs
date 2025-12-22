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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Core.Domain.Entities.Demon>>> GetDemons()
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DemonResponse>> GetDemonById(Guid id)
    {
        _logger.LogInformation($"Received request to GetDemonById idDemon:{id}");
        if (id == Guid.Empty)
            return BadRequest(new { message = "ID inválid" });

        var response = await _demonUseCase.GetByIdAsync(id);
        return new OkObjectResult($"Demon:{response}");
    }

    [HttpPost("Create")]
    public async Task<IActionResult> CreateDemon(DemonInput input)
    {
        _logger.LogInformation($"Received to CreateDemon DemonInput:{input}");

        if (input == null)
        {
            return BadRequest(new { message = "Input Invalid" });
        }

        var (response, message) = await _demonUseCase.CreateAsync(input);

        if (response == null)
        {
            return BadRequest(new { message });
        }

        return new OkObjectResult(new { data = response, message });
    }

    [HttpPost("CreateMany")]
    public async Task<IActionResult> CreateMany(DemonInput[] inputs)
    {
        if (inputs == null)
        {
            return BadRequest(new { message = "Input Invalid" });
        }

        var (responses, message) = await _demonUseCase.CreateManyAsync(inputs);
        return new OkObjectResult(new { data = responses, message });
    }
}
