using Inferno.src.Core.Domain.Entities.ManyToMany;
using Inferno.src.Core.Domain.Enums;

namespace Inferno.src.Core.Domain.Entities
{
    public class Sin
    {
        public Guid IdSin { get; set; } = Guid.NewGuid();
        public string SinName { get; set; }
        public required Severity SinSeverity { get; set; }

        // Foreign Key
        public Guid IdSoul { get; set; }

        // Navigation properties
        public Soul? Soul { get; set; }

        //Many-to-Many
        public virtual ICollection<Realize> Realizes { get; set; } = new List<Realize>();

        public Sin() { }
    }
}
