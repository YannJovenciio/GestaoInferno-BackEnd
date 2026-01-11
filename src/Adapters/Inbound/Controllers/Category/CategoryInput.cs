using System.ComponentModel.DataAnnotations;

namespace Inferno.src.Adapters.Inbound.Controllers.Category
{
    public record CategoryInput(
        [Required(ErrorMessage = "CategoryName is required")] string CategoryName
    );
}
