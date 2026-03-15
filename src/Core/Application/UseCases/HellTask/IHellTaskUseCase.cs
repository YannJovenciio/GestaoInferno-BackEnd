using Inferno.src.Adapters.Inbound.Controllers.HellTask;

namespace Inferno.src.Core.Application.UseCases.HellTask;

public interface IHellTaskUseCase
{
    Task<(HellTaskResponse response, string message)> CreateAsync(
        HellTaskInput input,
        CancellationToken cancellationToken
    );

    Task<(List<HellTaskResponse> response, string message)> CreateManyAsync(
        List<HellTaskInput> input,
        CancellationToken cancellationToken
    );

    Task<(Guid id, string message)> DeleteAsync(Guid id, CancellationToken cancellationToken);

    Task<(List<HellTaskResponse> response, string message)> GetAllByDemonIdAsync(
        int? pageSize,
        int? pageNumber,
        Guid id,
        CancellationToken cancellationToken
    );
    Task<(HellTaskResponse response, string message)> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken
    );
}
