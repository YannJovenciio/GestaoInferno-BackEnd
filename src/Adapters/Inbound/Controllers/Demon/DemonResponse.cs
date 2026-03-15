using Entity = Inferno.src.Core.Domain.Entities;

namespace Inferno.src.Adapters.Inbound.Controllers.Demon;

public class DemonResponse
{
    public Guid IdDemon { get; set; }
    public string? DemonName { get; set; }
    public Guid CategoryId { get; set; }
    public Entity.Category? Category { get; set; }

    public DemonResponse(Guid idDemon, string demonName, Entity.Category? category, Guid categoryId)
    {
        IdDemon = idDemon;
        DemonName = demonName;
        Category = category;
        CategoryId = categoryId;
    }
}
