using Inferno.src.Core.Domain.Entities.ManyToMany;

namespace Inferno.src.Core.Domain.Entities;

public class Soul
{
    public Guid IdSoul { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public HellEnum Level { get; set; } = HellEnum.Inferior;

    // Foreign Key
    public Guid? CavernId { get; set; }

    // Navigation Properties
    public Cavern? Cavern { get; set; }
    public ICollection<Sin> Sins { get; set; } = new List<Sin>();

    // Many-to-Many
    public virtual ICollection<Persecution> Persecutions { get; set; } = new List<Persecution>();
    public virtual ICollection<Realize> Realizes { get; set; } = new List<Realize>();

    public Soul() { }

    public Soul(string name, string description, Guid? cavernId = null)
    {
        Name = name;
        Description = description;
        CavernId = cavernId;
    }
}
