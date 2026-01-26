using System.ComponentModel.DataAnnotations;
using Inferno.src.Core.Domain.Enums;

namespace Inferno.src.Core.Application.DTOs.Request.Soul
{
    public class SoulInput
    {
        public Guid? CavernId { get; set; } = null;

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public Severity severity { get; set; } = Severity.critical;

        public SoulInput() { }

        public SoulInput(Guid? cavernId, string name, string description)
        {
            CavernId = cavernId;
            Name = name;
            Description = description;
        }

        public override string ToString()
        {
            return $"""
                SoulRequest:
                    CavernId: {CavernId}
                    Name: {Name}
                    Description: {Description}
                """;
        }
    }
}
