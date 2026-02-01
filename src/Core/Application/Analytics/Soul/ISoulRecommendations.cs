using Inferno.src.Adapters.Inbound.Controllers.Analytics.Soul;

namespace Inferno.src.Core.Application.Analytics.Soul;

public interface ISoulRecommendations
{
    Task<(List<SoulRecommendationsDto> responses, string message)> GetRecommendations();
}
