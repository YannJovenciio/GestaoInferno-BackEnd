namespace Inferno.src.Core.Application.DTOs.Response.Soul;

public class SoulResponse
{
    public Guid IdSoul { get; set; }
    public Guid? CavernId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public HellEnum Level { get; set; }

    public SoulResponse(
        Guid idSoul,
        Guid? cavernId,
        string name,
        string description,
        HellEnum level
    )
    {
        IdSoul = idSoul;
        CavernId = cavernId;
        Name = name;
        Description = description;
        Level = level;
    }
}
