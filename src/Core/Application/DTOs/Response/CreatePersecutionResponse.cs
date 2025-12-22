using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inferno.src.Core.Domain.Entities;

namespace Inferno.src.Core.Application.DTOs.Response
{
    public class CreatePersecutionResponse
    {
        public Demon? Demon { get; set; }
        public Soul? Soul { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public CreatePersecutionResponse(Demon demon, Soul soul)
        {
            Demon = demon;
            Soul = soul;
        }
    }
}
