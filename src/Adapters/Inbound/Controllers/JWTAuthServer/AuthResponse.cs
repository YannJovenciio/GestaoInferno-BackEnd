using Inferno.src.Adapters.Inbound.Controllers.Demon;

namespace Inferno.src.Adapters.Inbound.Controllers.JWTAuthServer;

public class AuthResponse
{
    public string? Token { get; set; }
    public DemonResponse? Demon { get; set; }
    public string? Message { get; set; }

    public AuthResponse() { }

    public AuthResponse(string? token, DemonResponse? demon, string? message = null)
    {
        Token = token;
        Demon = demon;
        Message = message;
    }
}
