namespace Inferno.src.Core.Domain.Entities.ManyToMany
{
    public class Realize
    {
        public Guid IdSin { get; set; }

        public Guid IdSoul { get; set; }

        //Navigation properties
        public Soul Soul { get; set; }
        public Sin Sin { get; set; }

        public Realize() { }
    }
}
