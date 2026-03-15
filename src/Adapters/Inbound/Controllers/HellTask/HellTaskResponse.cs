using Inferno.src.Core.Domain.Enums;

namespace Inferno.src.Adapters.Inbound.Controllers.HellTask;

public record HellTaskResponse(
    Guid HellTaskId,
    string? Title,
    string Description,
    DateTime CreatedAt,
    DateTime DeadLine,
    HellTaskStatus Status,
    int Progress,
    HellTaskPriority Priority,
    DateTime? UpdatedAt,
    DateTime? CompletedAt
);
