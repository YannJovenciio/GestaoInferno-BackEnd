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

    public async Task<AuthResponse> ExecuteAsync(LoginDto loginDto)
    {
        var client = await _context.GetClientByIdAsync(loginDto.ClientId);
        if (client == null)
            throw new UnauthorizedAccessException("Invalid client credentials.");

        var user = await _context.GetDemonByEmailAsync(loginDto.Email);
        if (user == null)
            throw new UnauthorizedAccessException("Invalid credentials.");

        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password);
        if (!isPasswordValid)
            throw new UnauthorizedAccessException("Invalid credentials.");

        var signingKey = await _context.GetActiveSigningKeyAsync();
        if (signingKey == null)
            throw new InvalidOperationException("No active signing key available.");

        var token = _tokenService.GenerateToken(user, client, signingKey);

        var demonResponse = new DemonResponse(
            user.IdDemon,
            user.DemonName,
            user.Category,
            user.CategoryId ?? Guid.Empty
        );

        return new AuthResponse(token, demonResponse, "Login successful");
    }

    public Task<bool> ExistsAsync(string demonEmail)
    {
        return _context.ExistsAsync(demonEmail);
    }

    public async Task<AuthResponse> RegisterAsync(RegisterDto input)
    {
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(input.Password);
        var demon = new Entity.Demon
        {
            IdDemon = Guid.NewGuid(),
            DemonName = input.DemonName,
            CategoryId = input.CategoryId,
            DemonEmail = input.Email,
            Password = hashedPassword,
        };
        await _context.RegisterAsync(demon);

        var demonResponse = new DemonResponse(
            demon.IdDemon,
            demon.DemonName,
            demon.Category,
            demon.CategoryId ?? Guid.Empty
        );

        return new AuthResponse(null, demonResponse!, "Demon created successfully. Please login.");
    }
}
