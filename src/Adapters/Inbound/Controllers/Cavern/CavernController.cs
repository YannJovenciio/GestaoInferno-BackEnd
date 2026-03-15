using Inferno.src.Adapters.Inbound.Controllers.Model;
using Inferno.src.Core.Application.UseCases.Cavern;
using Microsoft.AspNetCore.Mvc;

namespace Inferno.src.Adapters.Inbound.Controllers.Cavern;

[Route("api/[controller]")]
public class CavernController : Controller
{
    private readonly ICavernUseCase _cavernUseCase;
    private readonly ILogger<CavernController> _logger;

    public CavernController(ICavernUseCase cavernUseCase, ILogger<CavernController> logger)
    {
        _cavernUseCase = cavernUseCase;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCavern([FromBody] CavernInput input)
    {
        if (input == null)
            return BadRequest("Invalid input provided");
        var (response, message) = await _cavernUseCase.CreateCavern(input);
        return CreatedAtAction(
            nameof(CreateCavern),
            new { id = response.CavernId },
            new APIResponse<CavernResponse>(response, message)
        );
    }

    [HttpPost("CreateMany")]
    public async Task<IActionResult> CreateManyCavern([FromBody] List<CavernInput> inputs)
    {
        if (inputs == null)
            return BadRequest("Invalid input Provided");

        var (response, message) = await _cavernUseCase.CreateManyCavern(inputs);
        return CreatedAtAction(
            nameof(CreateManyCavern),
            new { id = response },
            new APIResponse<List<CavernResponse>>(response, message)
        );
    }
}
