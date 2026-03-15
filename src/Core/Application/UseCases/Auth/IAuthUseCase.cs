using Inferno.src.Adapters.Inbound.Controllers.Demon;
using Inferno.src.Adapters.Inbound.Controllers.JWTAuthServer;

namespace Inferno.src.Core.Application.UseCases.Auth;

public interface IAuthUseCase
{
    Task<AuthResponse> RegisterAsync(RegisterDto input);
    Task<AuthResponse> ExecuteAsync(LoginDto loginDto);
    Task<bool> ExistsAsync(string demonEmail);
}
