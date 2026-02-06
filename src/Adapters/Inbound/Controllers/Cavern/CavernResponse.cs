namespace Inferno.src.Adapters.Inbound.Controllers.Cavern;

public record CavernResponse(Guid CavernId, string CavernName, string Location, int Capacity);
