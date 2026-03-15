namespace Inferno.src.Adapters.Inbound.Controllers.JWTAuthServer;

public class ProfileDto
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Firstname { get; set; } = string.Empty;
    public string? Lastname { get; set; }
    public List<string> Roles { get; set; } = [];
}
