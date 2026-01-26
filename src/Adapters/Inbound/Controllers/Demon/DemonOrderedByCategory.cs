using Entity = Inferno.src.Core.Domain.Entities;

namespace Inferno.src.Adapters.Inbound.Controllers.Demon;

public record DemonOrderedByCategory(
    Guid? CategoryId,
    int DemonCount,
    double Percentage,
    List<Entity.Category> Category
);
