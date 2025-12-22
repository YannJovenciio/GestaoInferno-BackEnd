using System.ComponentModel.DataAnnotations.Schema;
using Inferno.src.Core.Domain.Entities.ManyToMany;

namespace Inferno.src.Core.Domain.Entities;

public class Soul
{
    public Guid IdSoul { get; set; } = Guid.NewGuid();
    public string? Name { get; set; }
    public string? Description { get; set; }
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
}
