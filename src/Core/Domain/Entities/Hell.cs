using System.ComponentModel.DataAnnotations.Schema;

namespace Inferno.src.Core.Domain.Entities
{
    public class Hell
    {
        public Guid IdHell { get; set; } = Guid.NewGuid();
        public string? Nome { get; set; }
        public string Descricao { get; set; }
        public HellEnum Nivel { get; set; } = HellEnum.Inferior;

        public Hell() { }
    }
}
