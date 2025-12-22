using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inferno.src.Core.Application.DTOs.Request.Persecution
{
    public class PersecutionRequest
    {
        public Guid IdDemon { get; set; }
        public Guid IdSoul { get; set; }
    }
}
