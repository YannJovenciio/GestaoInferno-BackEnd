using Inferno.src.Adapters.Inbound.Controllers.Demon;
using Inferno.src.Adapters.Inbound.Controllers.JWTAuthServer;

namespace Inferno.src.Core.Application.UseCases.Auth;

public interface IAuthUseCase
{
    Task<(DemonResponse? response, string message)> RegisterAsync(RegisterDto input);
    Task<object> ExecuteAsync(LoginDto loginDto);
    Task<bool> ExistsAsync(string demonEmail);
}
