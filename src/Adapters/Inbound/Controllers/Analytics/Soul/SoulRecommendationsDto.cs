namespace Inferno.src.Adapters.Inbound.Controllers.Analytics.Soul;

public record SoulRecommendationsDto(
    Guid SoulId,
    string SoulName,
    HellEnum? Level,
    int PersecutionCount,
    string MostActiveDemonName,
    int DemonCount,
    int SinCount
);
