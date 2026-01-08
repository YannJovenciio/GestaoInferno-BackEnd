using Inferno.src.Core.Domain.Entities;

namespace Inferno.src.Core.Application.DTOs
{
    public class PersecutionResponse
    {
        public Demon? Demon { get; set; }
        public Soul? Soul { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public PersecutionResponse(Demon demon, Soul soul)
        {
            Demon = demon;
            Soul = soul;
        }
    }
}
