using Inferno.src.Adapters.Inbound.Controllers.GetSinsBySeverity;
using Inferno.src.Adapters.Inbound.Controllers.Model;
using Inferno.src.Core.Application.UseCases.GetSinsBySeverity;
using Inferno.src.Core.Application.UseCases.Sin;
using Inferno.src.Core.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Inferno.src.Adapters.Inbound.Controllers.Sin
{
    [ApiController]
    [Route("api/[controller]")]
    public class SinController : ControllerBase
    {
        private readonly ILogger<SinController> _logger;
        private readonly ISinUseCase _sinUseCase;
        private readonly IGetSinsBySeverity _getBySeverity;

        public SinController(
            ISinUseCase sinUseCase,
            ILogger<SinController> logger,
            IGetSinsBySeverity getBySeverity
        )
        {
            _sinUseCase = sinUseCase;
            _logger = logger;
            _getBySeverity = getBySeverity;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("HTTP GET: Retrieving all sins");
            var (response, message) = await _sinUseCase.GetAllSins();
            return Ok(new APIResponse<List<SinResponse>>(response, message));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                _logger.LogWarning("HTTP GET: Invalid id provided - Empty guid");
                return BadRequest(new APIResponse<object>(null, "Invalid id provided"));
            }

            _logger.LogInformation("HTTP GET: Retrieving sin by ID: {SinId}", id);
            var (response, message) = await _sinUseCase.GetById(id);
            return Ok(new APIResponse<SinResponse>(response, message));
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetAllWithFilters(
            [FromQuery] Guid? idSin,
            [FromQuery] Severity? severity
        )
        {
            _logger.LogInformation(
                "HTTP GET: Retrieving sins with filters - SinID: {SinId}, Severity: {Severity}",
                idSin ?? Guid.Empty,
                severity
            );
            var (response, message) = await _sinUseCase.GetAllWithFilters(idSin, severity);
            return Ok(new APIResponse<List<SinResponse>>(response, message));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SinInput input)
        {
            if (input == null)
            {
                _logger.LogWarning("HTTP POST: Invalid input provided - null");
                return BadRequest(new APIResponse<SinResponse>(null, "Invalid input provided"));
            }

            _logger.LogInformation("HTTP POST: Creating sin with name: {SinName}", input.SinName);
            var (response, message) = await _sinUseCase.CreateSin(input);
            return CreatedAtAction(
                nameof(GetById),
                new { id = response.IdSin },
                new APIResponse<SinResponse>(response, message)
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                _logger.LogWarning("HTTP DELETE: Invalid id provided - Empty guid");
                return BadRequest(new APIResponse<SinResponse>(null, "Invalid id provided"));
            }

            _logger.LogInformation("HTTP DELETE: Deleting sin by ID: {SinId}", id);
            var (response, message) = await _sinUseCase.Delete(id);
            return Ok(new APIResponse<SinResponse>(response, message));
        }

        [HttpGet("BySeverity")]
        public async Task<IActionResult> GetSinsBySeverity()
        {
            var (responses, message) = await _getBySeverity.GetSinBySeverity();
            return Ok(new APIResponse<List<GetSinBySeverityResponse>>(responses, message));
        }
    }
}
