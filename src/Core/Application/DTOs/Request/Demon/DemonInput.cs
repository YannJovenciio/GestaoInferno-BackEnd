using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inferno.src.Core.Application.DTOs.Request.Demon
{
    public class DemonInput
    {
        public Guid CategoryId { get; set; }
        public string DemonName { get; set; }

        public DemonInput() { }

        public DemonInput(string demonName, Guid categoryId)
        {
            DemonName = demonName;
            CategoryId = categoryId;
        }
    }
}
