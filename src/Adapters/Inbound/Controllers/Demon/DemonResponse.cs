using Inferno.src.Core.Domain.Entities;

namespace Inferno.src.Core.Application.DTOs
{
    public class DemonResponse
    {
        public Guid IdDemon { get; set; }
        public string? DemonName { get; set; }
        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }
        public DateTime Birth { get; set; }

        public DemonResponse(Guid idDemon, string demonName, Guid categoryId)
        {
            IdDemon = idDemon;
            DemonName = demonName;
            CategoryId = categoryId;
        }
    }
}
