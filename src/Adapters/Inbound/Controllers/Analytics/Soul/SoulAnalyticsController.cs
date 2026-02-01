using Inferno.src.Adapters.Inbound.Controllers.Model;
using Inferno.src.Core.Application.Analytics.Soul;
using Microsoft.AspNetCore.Mvc;

namespace Inferno.src.Adapters.Inbound.Controllers.Analytics.Soul;

[Route("api/[Controller]")]
public class SoulAnalyticsController : Controller
{
    private readonly ISoulRecommendations _soulRecommendations;

    public SoulAnalyticsController(ISoulRecommendations soulRecommendations)
    {
        _soulRecommendations = soulRecommendations;
    }

    [HttpGet]
    public async Task<IActionResult> GetSoulWithSinRealization()
    {
        var (response, message) = await _soulRecommendations.GetRecommendations();
        return Ok(new APIResponse<List<SoulRecommendationsDto>>(response, message));
    }
}
