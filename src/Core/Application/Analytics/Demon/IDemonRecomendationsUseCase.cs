
namespace Inferno.src.Core.Application.Analytics;

public interface IDemonRecomendationsUseCase
{
    Task<(List<DemonRecommendations> responses, string message)> GetRecomendations();
}
