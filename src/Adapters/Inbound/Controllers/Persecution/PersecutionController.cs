using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Inferno.src.Core.Application.DTOs.Request.Persecution;
using Inferno.src.Core.Domain.Interfaces.UseCases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Inferno.src.Adapters.Inbound.Persecution
{
    [Route("[controller]")]
    public class PersecutionController : Controller
    {
        private readonly ILogger<PersecutionController> _logger;
        private readonly IPersecutionUseCase _persecution;

        public PersecutionController(
            ILogger<PersecutionController> logger,
            IPersecutionUseCase persecution
        )
        {
            _persecution = persecution;
            _logger = logger;
        }

        [HttpPost]
        [Route("CreatePersecutiom")]
        public async Task<IActionResult> CreatePersecution(Guid idDemon, Guid idSoul)
        {
            if (idDemon == null | idSoul == null)
            {
                return BadRequest("Invalid arguments");
            }
            _logger.LogInformation($"Received idDemon:{idDemon} and idSoul:{idSoul}");
            var (message, response) = await _persecution.CreatePersecution(idDemon, idSoul);
            return new OkObjectResult(new { data = response, message });
        }

        // [HttpGet]
        // [Route("GetAllPersecution")]
        // public async Task<IActionResult> GetAllPersecution() { }
    }
}
