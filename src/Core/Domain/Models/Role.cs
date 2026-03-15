using System.ComponentModel.DataAnnotations;

namespace Inferno.src.Core.Domain.Models;

public class Role
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public ICollection<DemonRole> DemonRoles { get; set; } = [];
}
