using Inferno.src.Core.Application.UseCases.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inferno.src.Adapters.Inbound.Controllers.JWTAuthServer;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthUseCase _IAuthUseCase;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthUseCase authUseCase, ILogger<AuthController> logger)
    {
        _IAuthUseCase = authUseCase;
        _logger = logger;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var response = await _IAuthUseCase.ExecuteAsync(loginDto);
            return Ok(response);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpPost("Register")]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterDemon([FromBody] RegisterDto input)
    {
        _logger.LogInformation("received to RegisterDemon DemonInput:{Input}", input);

        if (input == null)
        {
            return BadRequest(new { message = "Input invalid" });
        }
        var exists = await _IAuthUseCase.ExistsAsync(input.Email);
        if (exists)
            return Conflict(new { message = "Demon with this email already exists" });

        var response = await _IAuthUseCase.RegisterAsync(input);
        _logger.LogInformation("successfully created demon");

        return CreatedAtAction(
            nameof(RegisterDemon),
            new { id = response.Demon?.IdDemon },
            response
        );
    }
}
