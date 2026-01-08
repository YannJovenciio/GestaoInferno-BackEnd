namespace Inferno.src.Core.Domain.Entities
{
    public class Cavern
    {
        public Guid IdCavern { get; set; } = Guid.NewGuid();
        public string? Location { get; set; }
        public int Capacity { get; set; }
        public virtual ICollection<Soul> Souls { get; set; } = new List<Soul>();

        public Cavern() { }
    }
}
