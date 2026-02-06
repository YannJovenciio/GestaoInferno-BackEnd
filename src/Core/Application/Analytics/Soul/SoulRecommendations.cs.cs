using Inferno.src.Adapters.Inbound.Controllers.Analytics.Soul;
using Inferno.src.Core.Domain.Interfaces.Repository.Souls;

namespace Inferno.src.Core.Application.Analytics.Soul;

public class SoulRecommendations : ISoulRecommendations
{
    private readonly ISoulRepository _souRepository;

    public SoulRecommendations(ISoulRepository soulRepository)
    {
        _souRepository = soulRepository;
    }

    public async Task<(List<SoulRecommendationsDto> responses, string message)> GetRecommendations()
    {
        var souls = await _souRepository.GetAllWithSins();
        List<SoulRecommendationsDto> recommendations = new List<SoulRecommendationsDto>();

        foreach (var soul in souls)
        {
            var soulId = soul.IdSoul;
            var soulName = soul.SoulName;
            var Level = soul.Level;
            var persecutionCount = soul.Persecutions.Count;

            var demons = soul
                .Persecutions.GroupBy(p => p.Demon)
                .OrderByDescending(g => g.Count())
                .FirstOrDefault();

            var MostActiveDemonName = demons == null ? "Uknown" : demons.Key.DemonName;

            var demonCount = soul.Persecutions.GroupBy(d => d.IdDemon).Count();
            var sinCount = soul.Realizes.Count();
            SoulRecommendationsDto recommendation = new(
                soulId,
                soulName,
                Level,
                persecutionCount,
                MostActiveDemonName,
                demonCount,
                sinCount
            );
            recommendations.Add(recommendation);
        }
        return (recommendations, $"Succesfull retrivied {recommendations.Count} recomendations");
    }
}
