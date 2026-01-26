using Inferno.src.Core.Domain.Enums;

namespace Inferno.src.Adapters.Inbound.Controllers.Sin;

public record SinOrderedBySeverity(
    Severity Severity,
    int count,
    List<string> SinName,
    double Percentage
);
