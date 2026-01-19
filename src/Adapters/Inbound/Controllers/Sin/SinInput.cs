using Inferno.src.Core.Domain.Enums;

namespace Inferno.src.Adapters.Inbound.Controllers.Sin
{
    public class SinInput
    {
        public string SinName { get; set; }
        public Severity SinSeverity { get; set; }
    }
}
