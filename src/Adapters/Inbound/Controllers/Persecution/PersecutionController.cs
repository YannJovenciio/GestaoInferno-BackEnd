using Inferno.src.Adapters.Inbound.Controllers.Category;
using Inferno.src.Adapters.Inbound.Controllers.Model;
using Inferno.src.Core.Application.DTOs;
using Inferno.src.Core.Application.DTOs.Request.Persecution;
using Inferno.src.Core.Domain.Interfaces.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace Inferno.src.Adapters.Inbound.Persecution
{
    [Route("api/[controller]")]
    public class PersecutionController : Controller
    {
        private readonly ILogger<PersecutionController> _logger;
        private readonly IPersecutionUseCase _persecutionUseCase;

        public PersecutionController(
            ILogger<PersecutionController> logger,
            IPersecutionUseCase persecution
        )
        {
            _persecutionUseCase = persecution;
            _logger = logger;
        }

        [HttpPost("CreatePersecution")]
        public async Task<IActionResult> CreatePersecution([FromBody] PersecutionInput request)
        {
            _logger.LogInformation(
                $"received request to create persecution with Demon id:{request.IdDemon} and Soul id:{request.IdSoul}"
            );

            if (!ModelState.IsValid)
                return BadRequest(new APIResponse<PersecutionResponse>("invalid input provided"));

            var (response, message) = await _persecutionUseCase.CreatePersecution(request);
            _logger.LogInformation(
                $"sucessfuly created persecution with Demon id:{request.IdDemon} and Soul id:{request.IdSoul}"
            );
            return CreatedAtAction(
                nameof(PersecutionResponse),
                new { id = response },
                new APIResponse<PersecutionResponse>(response, message)
            );
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("receveid request to get all persecutions");
            var (response, message) = await _persecutionUseCase.GetAllPersecutions();
            _logger.LogInformation($"sucessfuly found {response.Count} persecutions");
            return Ok(new { data = response, message });
        }

        [HttpGet("GetAllF")]
        public async Task<IActionResult> GetAllF(
            [FromQuery] Guid? idDemon,
            [FromQuery] Guid? idSoul
        )
        {
            var (response, message) = await _persecutionUseCase.GetAllPersecutionsWithFilter(
                idDemon,
                idSoul
            );
            return Ok(new APIResponse<List<PersecutionResponse>>(response, message));
        }
    }
}
