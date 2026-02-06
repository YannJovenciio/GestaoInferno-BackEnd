using Microsoft.AspNetCore.Mvc;

namespace Inferno.src.Adapters.Inbound.Controllers.Cavern;

[Route("api/[controller]")]
public class CavernController : Controller
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CavernInput input)
    {
        return Ok();
    }
}
