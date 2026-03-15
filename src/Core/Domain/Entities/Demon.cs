using System.Text.Json.Serialization;
using Inferno.src.Core.Domain.Entities.ManyToMany;
using Inferno.src.Core.Domain.Models;

namespace Inferno.src.Core.Domain.Entities;

public class Demon
{
    public Guid IdDemon { get; set; }
    public Guid? CategoryId { get; set; } = null;
    public string? DemonName { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public int? ImageId { get; set; }
    public string DemonEmail { get; set; } = string.Empty;
    public virtual Image? Image { get; set; }
    public virtual Category Category { get; set; }
    public virtual ICollection<HellTask> HellTasks { get; set; }

    [JsonIgnore]
    public virtual ICollection<Persecution> Persecutions { get; set; } = [];

    public string Password { get; set; } = string.Empty;

    [JsonIgnore]
    public virtual ICollection<DemonRole> DemonRoles { get; set; } = [];

    public Demon() { }

    public Demon(string demonName, Guid? category, string demonEmail, string password)
    {
        DemonName = demonName;
        CategoryId = category;
        DemonEmail = demonEmail;
        Password = password;
    }

    public override string ToString()
    {
        return $"Demon{{IdDemon={IdDemon}, DemonName={DemonName}, CategoryId={CategoryId}, CreatedAt={CreatedAt}, UpdatedAt={UpdatedAt}}}";
    }
}
