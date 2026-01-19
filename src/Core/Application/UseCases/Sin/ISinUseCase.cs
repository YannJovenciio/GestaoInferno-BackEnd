using Inferno.src.Adapters.Inbound.Controllers.Sin;
using Inferno.src.Core.Domain.Enums;

namespace Inferno.src.Core.Application.UseCases.Sin;

public interface ISinUseCase
{
    Task<(List<SinResponse> responses, string message)> GetAllSins();
    Task<(List<SinResponse> responses, string message)> GetAllWithFilters(
        Guid? IdSIn,
        Severity? severity
    );
    Task<(SinResponse response, string message)> CreateSin(SinInput input);
    Task<(SinResponse? response, string message)> GetById(Guid idSin);
    Task<(SinResponse? response, string message)> Delete(Guid idSin);
    Task<(SinResponse response, string message)> Update(Guid idSin, SinResponse sin);
    Task<(List<SinResponse> responses, string message)> CreateMany(List<SinInput> input);
}
