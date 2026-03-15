using System.ComponentModel.DataAnnotations;

namespace Inferno.src.Adapters.Inbound.Controllers.JWTAuthServer;

public class LoginDto
{
    [EmailAddress]
    [Required(ErrorMessage = "Email is required.")]
    [MaxLength(100, ErrorMessage = "Email must be less than or equal to 100 characters.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required.")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
    [MaxLength(100, ErrorMessage = "Password must be less than or equal to 100 characters.")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "ClientId is required.")]
    public string ClientId { get; set; } = string.Empty;
}
