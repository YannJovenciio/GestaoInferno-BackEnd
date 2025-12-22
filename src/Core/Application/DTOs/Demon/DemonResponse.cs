using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inferno.src.Core.Domain.Entities;

namespace Inferno.src.Core.Application.DTOs
{
    public class DemonResponse
    {
        public string? DemonName { get; set; }
        public Category? Category { get; set; }
        public DateTime Birth { get; set; }

        public DemonResponse(string demonName, Category category)
        {
            DemonName = demonName;
            Category = category;
        }
    }
}
