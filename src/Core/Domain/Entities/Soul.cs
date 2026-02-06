using System.Text.Json.Serialization;
using Inferno.src.Core.Domain.Entities.ManyToMany;

namespace Inferno.src.Core.Domain.Entities;

public class Soul
{
    public Guid IdSoul { get; set; }
    public string SoulName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public HellEnum Level { get; set; } = HellEnum.Inferior;

    // Foreign Key
    public Guid? CavernId { get; set; }

    // Navigation Properties
    public Cavern? Cavern { get; set; }
    public ICollection<Sin> Sins { get; set; } = new List<Sin>();

    // Many-to-Many
    [JsonIgnore]
    public virtual ICollection<Persecution> Persecutions { get; set; } = new List<Persecution>();
    public virtual ICollection<Realize> Realizes { get; set; } = new List<Realize>();

    public Soul() { }

    public Soul(string soulName, string description, Guid? cavernId = null)
    {
        SoulName = soulName;
        Description = description;
        CavernId = cavernId;
    }
}
