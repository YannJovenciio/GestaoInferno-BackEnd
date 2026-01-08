using System.ComponentModel.DataAnnotations;

namespace Inferno.src.Core.Application.DTOs.Request.Soul
{
    public class SoulRequest
    {
        public Guid? CavernId { get; set; } = null;

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;

        public SoulRequest() { }

        public SoulRequest(Guid? cavernId, string name, string description)
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
