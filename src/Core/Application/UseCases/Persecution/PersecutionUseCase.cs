using Inferno.src.Core.Application.DTOs;
using Inferno.src.Core.Application.DTOs.Request.Persecution;
using Inferno.src.Core.Domain.Interfaces.Persecution;
using Inferno.src.Core.Domain.Interfaces.UseCases;

namespace Inferno.src.Core.Application.UseCases.Demon
{
    public class PersecutionUseCase : IPersecutionUseCase
    {
        private readonly ILogger<PersecutionUseCase> _logger;
        private readonly IPersecutionRepository _persecutionRepository;

        public PersecutionUseCase(
            IPersecutionRepository persecutionRepository,
            ILogger<PersecutionUseCase> logger
        )
        {
            _persecutionRepository = persecutionRepository;
            _logger = logger;
        }

        public async Task<(PersecutionResponse? response, string message)> CreatePersecution(
            PersecutionInput request
        )
        {
            _logger.LogInformation("Received request to create Persecution");
            if (request == null)
            {
                return (null, "Invalid input provided");
            }
            var idDemon = request.IdDemon;
            var idSoul = request.IdSoul;
            _logger.LogInformation($"Ids provided DemonId:${idDemon},IdSoul:${idSoul}");
            var response = await _persecutionRepository.CreatePersecution(idDemon, idSoul);
            return (
                new PersecutionResponse(response.Demon, response.Soul),
                "Persecution created sucessfuly"
            );
        }

        public async Task<(
            List<PersecutionResponse>? responses,
            string message
        )> GetAllPersecutions()
        {
            _logger.LogInformation("Received request to get all persecutions");
            var persecutions = await _persecutionRepository.GetAllPersecutions();
            var responses = persecutions
                .Select(p => new PersecutionResponse(p.Demon, p.Soul))
                .ToList();
            _logger.LogInformation($"sucessfuly found {persecutions.Count}persecutions");
            return (responses, $"sucessfuly found {responses.Count} persecutions");
        }

        public async Task<(
            List<PersecutionResponse>? persecutions,
            string message
        )> GetAllPersecutionsWithFilter(Guid? idDemon, Guid? idSoul)
        {
            _logger.LogInformation(
                $"Receveid request to get all persecutions with {idDemon ?? null},{idSoul ?? null} filter"
            );
            var persecutions = await _persecutionRepository.GetAllPersecutionWithFilter(
                idSoul: idSoul,
                idDemon: idDemon
            );

            var response = persecutions
                .Select(p => new PersecutionResponse(p.Demon, p.Soul))
                .ToList();
            _logger.LogInformation($"found {response.Count} persecutions for this filter");
            if (response.Count == 0)
                return (null, "No persecutions found for this filters");
            return (response, $"Sucessfuly found {response.Count} persecutions for this filter");
        }
    }
}
