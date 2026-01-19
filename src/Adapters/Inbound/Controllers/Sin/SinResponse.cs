using Inferno.src.Core.Domain.Enums;

namespace Inferno.src.Adapters.Inbound.Controllers.Sin
{
    public record SinResponse(Guid IdSin, string SinName, Severity SinSeverity);
}
