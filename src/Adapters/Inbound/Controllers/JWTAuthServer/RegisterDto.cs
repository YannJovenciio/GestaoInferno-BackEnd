using System.ComponentModel.DataAnnotations;

namespace Inferno.src.Adapters.Inbound.Controllers.JWTAuthServer;

public class RegisterDto
{
    [Required(ErrorMessage = "CategoryId is required")]
    public Guid? CategoryId { get; set; }

    [Required(ErrorMessage = "First name is required.")]
    [MaxLength(50, ErrorMessage = "First name must be less than or equal to 50 characters.")]
    public string DemonName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    [MaxLength(100, ErrorMessage = "Email must be less than or equal to 100 characters.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required.")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
    [MaxLength(100, ErrorMessage = "Password must be less than or equal to 100 characters.")]
    public string Password { get; set; } = string.Empty;
}
