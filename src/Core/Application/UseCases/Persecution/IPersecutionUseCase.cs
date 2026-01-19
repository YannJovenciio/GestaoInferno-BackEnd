using Inferno.src.Core.Application.DTOs;
using Inferno.src.Core.Application.DTOs.Request.Persecution;

namespace Inferno.src.Core.Domain.Interfaces.UseCases
{
    public interface IPersecutionUseCase
    {
        Task<(PersecutionResponse? response, string message)> CreatePersecution(
            PersecutionInput request
        );
        Task<(List<PersecutionResponse>? responses, string message)> GetAllPersecutions();
        Task<(
            List<PersecutionResponse>? persecutions,
            string message
        )> GetAllPersecutionsWithFilter(Guid? idDemon, Guid? idSoul);
    }
}
