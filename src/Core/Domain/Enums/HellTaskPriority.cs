using System.ComponentModel;

namespace Inferno.src.Core.Domain.Enums;

public enum HellTaskPriority
{
    [Description("Minimal")]
    Minimal = 1,

    [Description("Medium")]
    Medium = 2,

    [Description("High")]
    High = 3,

    [Description("Critical")]
    Critical = 4,
}
