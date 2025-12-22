using System.ComponentModel.DataAnnotations.Schema;
using Inferno.src.Core.Domain.Entities.ManyToMany;

namespace Inferno.src.Core.Domain.Entities;

public class Demon
{
    public Guid IdDemon { get; set; } = Guid.NewGuid();
    public Guid CategoryId { get; set; }
    public string DemonName { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public virtual Category Category { get; set; }

    public virtual ICollection<Persecution> Persecutions { get; set; } = new List<Persecution>();
    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public Demon() { }

    public Demon(string demonName, Guid category)
    {
        DemonName = demonName;
        CategoryId = category;
    }
}
