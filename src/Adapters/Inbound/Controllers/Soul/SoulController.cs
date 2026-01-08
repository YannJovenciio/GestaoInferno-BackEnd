using Inferno.src.Core.Application.DTOs.Request.Soul;
using Inferno.src.Core.Domain.Interfaces.UseCases.Soul;
using Microsoft.AspNetCore.Mvc;

namespace Inferno.src.Adapters.Inbound.Controllers.Soul
{
    [Route("api/[controller]")]
    public class SoulController : Controller
    {
        private readonly ILogger<SoulController> _logger;
        private readonly ISoulUseCase _soulUseCase;

        public SoulController(ILogger<SoulController> logger, ISoulUseCase soulUseCase)
        {
            _logger = logger;
            _soulUseCase = soulUseCase;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("receveid request to get all Souls");
            var (response, message) = await _soulUseCase.GetAllSoulsAsync();
            _logger.LogInformation($"sucessfuly retrivied:{response.Count} souls");
            return new OkObjectResult(new { data = response, message });
        }

        [HttpGet("Get/{id?}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            _logger.LogInformation($"receveid request to get Soul with id:{id} ");
            if (id == Guid.Empty)
                return BadRequest("invalid id provided");
            var (response, message) = await _soulUseCase.GetSoulByIdAsync(id);
            _logger.LogInformation($"sucessfuly found soul with id:{id}");
            return new OkObjectResult(new { data = response, message });
        }

        [HttpPost("CreateMany")]
        public async Task<IActionResult> CreateMany([FromBody] List<SoulRequest> inputs)
        {
            _logger.LogInformation($"receveid request to create {inputs.Count} souls");
            if (inputs == null | inputs?.Count == 0)
                return BadRequest("invalid inputs provided");
            var (response, message) = await _soulUseCase.CreateManySoulsAsync(inputs);
            _logger.LogInformation($"sucessfuly created {response.Count} souls");
            return new OkObjectResult(new { data = response, message });
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] SoulRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest($"invalid input provided ");
            }
            var (response, message) = await _soulUseCase.CreateSoul(request);
            return new OkObjectResult(new { data = response, message });
        }

        [HttpGet("GetAllF")]
        public async Task<IActionResult> GetAllWithFilters(
            [FromQuery] Guid? cavernId,
            [FromQuery] string? description,
            [FromQuery] HellEnum? level
        )
        {
            var (response, message) = await _soulUseCase.GetAllSoulsWithFilters(
                cavernId,
                level,
                description
            );
            return new OkObjectResult(new { data = response, message });
        }
    }
}
