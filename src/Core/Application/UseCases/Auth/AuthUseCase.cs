using Inferno.src.Adapters.Inbound.Controllers.Demon;
using Inferno.src.Adapters.Inbound.Controllers.JWTAuthServer;
using Inferno.src.Adapters.Outbound.Persistence.Repositories.Auth;
using Inferno.src.Core.Application.Services.AuthJWT;
using Entity = Inferno.src.Core.Domain.Entities;

namespace Inferno.src.Core.Application.UseCases.Auth;

public class AuthUseCase(IAuthRepository context, ITokenService tokenService) : IAuthUseCase
{
    private readonly IAuthRepository _context = context;
    private readonly ITokenService _tokenService = tokenService;

    public async Task<object> ExecuteAsync(LoginDto loginDto)
    {
        // Validate client
        var client = await _context.GetClientByIdAsync(loginDto.ClientId);
        if (client == null)
        {
            throw new UnauthorizedAccessException("Invalid client credentials.");
        }

        // Validate user
        var user = await _context.GetDemonByEmailAsync(loginDto.Email);
        if (user == null)
        {
            throw new UnauthorizedAccessException("Invalid credentials.");
        }

        // Verify password
        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password);
        if (!isPasswordValid)
        {
            throw new UnauthorizedAccessException("Invalid credentials.");
        }

        // Get active signing key
        var signingKey = await _context.GetActiveSigningKeyAsync();
        if (signingKey == null)
        {
            throw new InvalidOperationException("No active signing key available.");
        }

        // Generate token
        var token = _tokenService.GenerateToken(user, client, signingKey);

        return new { Token = token };
    }

    public Task<bool> ExistsAsync(string demonEmail)
    {
        throw new NotImplementedException();
    }

    public async Task<(DemonResponse? response, string message)> RegisterAsync(RegisterDto input)
    {
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(input.Password);
        var demon = new Entity.Demon
        {
            IdDemon = Guid.NewGuid(),
            DemonName = input.DemonName,
            CategoryId = input.CategoryId,
            Password = hashedPassword,
        };
        await _context.RegisterAsync(demon);
        return (
            new DemonResponse(
                demon.IdDemon,
                demon.DemonName,
                demon.Category,
                demon.CategoryId ?? Guid.Empty
            ),
            "Demon created sucessfuly"
        );
    }
}
