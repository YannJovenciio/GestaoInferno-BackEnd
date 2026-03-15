using Inferno.src.Core.Domain.Entities;

namespace Inferno.src.Core.Domain.Models;

public class DemonRole
{
    public Guid DemonId { get; set; }
    public Demon Demon { get; set; } = null!;
    public int RoleId { get; set; }
    public Role Role { get; set; } = null!;
}
