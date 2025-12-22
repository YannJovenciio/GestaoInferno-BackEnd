using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Inferno.src.Core.Domain.Entities
{
    public class Category
    {
        public Guid IdCategoria { get; set; }
        public required string NomeCategoria { get; set; }
        public virtual ICollection<Demon> Demons { get; set; } = new List<Demon>();

        public Category() { }
    }
}
