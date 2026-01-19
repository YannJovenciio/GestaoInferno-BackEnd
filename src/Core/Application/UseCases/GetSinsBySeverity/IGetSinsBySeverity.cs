using Inferno.src.Adapters.Inbound.Controllers.GetSinsBySeverity;

namespace Inferno.src.Core.Application.UseCases.GetSinsBySeverity;

public interface IGetSinsBySeverity
{
    Task<(List<GetSinBySeverityResponse> response, string message)> GetSinBySeverity();
}
