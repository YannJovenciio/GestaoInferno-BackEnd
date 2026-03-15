using System.ComponentModel.DataAnnotations;
using Inferno.src.Core.Domain.Enums;

namespace Inferno.src.Adapters.Inbound.Controllers.HellTask;

public record HellTaskInput(
    [Required(ErrorMessage = "Title is required")] string? Title,
    [Required(ErrorMessage = "Description is required")] string Description,
    [Required(ErrorMessage = "DemonId is required")] Guid DemonId,
    [Required(ErrorMessage = "Priority is required")] HellTaskPriority Priority
);
