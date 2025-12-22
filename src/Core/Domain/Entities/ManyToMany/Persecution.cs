using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Inferno.src.Core.Domain.Entities.ManyToMany
{
    public class Persecution
    {
        public Guid IdDemon { get; set; } = Guid.NewGuid();

        public Guid IdSoul { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public Demon Demon { get; set; }
        public Soul Soul { get; set; }

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
