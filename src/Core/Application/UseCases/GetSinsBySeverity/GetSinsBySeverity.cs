using Inferno.src.Adapters.Inbound.Controllers.GetSinsBySeverity;
using Inferno.src.Core.Domain.Interfaces.Repository.Sin;

namespace Inferno.src.Core.Application.UseCases.GetSinsBySeverity;

public class GetSinsBySeverity : IGetSinsBySeverity
{
    private readonly ISinRepository _context;

    public GetSinsBySeverity(ISinRepository context)
    {
        _context = context;
    }

    public async Task<(List<GetSinBySeverityResponse> response, string message)> GetSinBySeverity()
    {
        var sins = await _context.GetAll();
        var totalSins = sins.Count;
        var groupedSins = sins.GroupBy(s => s.SinSeverity)
            .Select(g => new GetSinBySeverityResponse(
                g.Count(),
                (g.Count() / (double)totalSins) * 100,
                g.Key.ToString()
            ))
            .OrderByDescending(g => g.SinCount)
            .ToList();

        return (groupedSins, "Successfully retrieved sins grouped by severity");
    }
}
