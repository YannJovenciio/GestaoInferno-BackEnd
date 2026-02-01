using Inferno.src.Core.Domain.Interfaces;

namespace Inferno.src.Core.Application.Analytics;

public class DemonRecomendationsUseCase : IDemonRecomendationsUseCase
{
    private readonly IDemonRepository _demonRepository;
    private readonly ILogger<DemonRecomendationsUseCase> _logger;

    public DemonRecomendationsUseCase(
        IDemonRepository demonRepository,
        ILogger<DemonRecomendationsUseCase> logger
    )
    {
        _demonRepository = demonRepository;
        _logger = logger;
    }

    public async Task<(List<DemonRecommendations> responses, string message)> GetRecomendations()
    {
        var demons = await _demonRepository.GetDemonswithPersecution();

        List<DemonRecommendations> recommendations = new List<DemonRecommendations>();

        foreach (var demon in demons)
        {
            var soulCount = demon.Persecutions.GroupBy(p => p.Soul).Count();
            var persecutionCount = demon.Persecutions.Count();

            var torturedSouls = demon
                .Persecutions.GroupBy(d => d.Soul)
                .OrderByDescending(d => d.Count())
                .FirstOrDefault();
            string mostTorturedSoul = torturedSouls == null ? "N/A" : torturedSouls.Key.Name;
            var recomendation = new DemonRecommendations(
                demon.IdDemon,
                demon.DemonName,
                string.IsNullOrEmpty(demon.Category?.CategoryName)
                    ? "Unknown"
                    : demon.Category.CategoryName,
                persecutionCount,
                mostTorturedSoul,
                soulCount
            );
            recommendations.Add(recomendation);
        }
        return (recommendations, $"Succesfull retrivied {recommendations.Count} recommendations");
    }
}
