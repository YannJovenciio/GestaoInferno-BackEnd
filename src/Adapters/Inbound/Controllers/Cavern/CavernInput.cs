using System.ComponentModel.DataAnnotations;

namespace Inferno.src.Adapters.Inbound.Controllers.Cavern;

public record CavernInput(
    [Required(ErrorMessage = "Location is required")] string Location,
    [Required(ErrorMessage = "Cavern name is required")] string CavernName,
    int Capacity
);
