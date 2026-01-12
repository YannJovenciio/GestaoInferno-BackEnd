namespace Inferno.src.Core.Domain.Entities.ManyToMany
{
    public class Persecution
    {
        public Guid IdDemon { get; set; }

        public Guid IdSoul { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public required Demon Demon { get; set; }
        public required Soul Soul { get; set; }

        public Persecution() { }

        public Persecution(Demon demon, Soul soul)
        {
            Demon = demon;
            Soul = soul;
            IdSoul = soul.IdSoul;
            IdDemon = demon.IdDemon;
        }
    }
}
