using Inferno.src.Adapters.Inbound.Controllers.Model;
using Inferno.src.Core.Application.Analytics;
using Microsoft.AspNetCore.Mvc;

namespace Inferno.src.Adapters.Inbound.Controllers.Analytics;

[Route("api/[Controller]")]
public class DemonAnalyticsController : Controller
{
    private readonly ILogger<DemonAnalyticsController> _logger;
    private readonly IDemonRecomendationsUseCase _demonRecomendations;

    public DemonAnalyticsController(
        ILogger<DemonAnalyticsController> logger,
        IDemonRecomendationsUseCase demonRecomendations
    )
    {
        _logger = logger;
        _demonRecomendations = demonRecomendations;
    }

    [HttpGet]
    public async Task<IActionResult> GetDemonsAnalytics()
    {
        var (response, message) = await _demonRecomendations.GetRecomendations();
        return Ok(new APIResponse<List<DemonRecommendations>>(response, message));
    }
}
