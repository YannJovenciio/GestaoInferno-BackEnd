using System.ComponentModel;

namespace Inferno.src.Core.Domain.Enums;

public enum HellTaskStatus
{
    [Description("Awaiting")]
    Awaiting = 1,

    [Description("Confirmed")]
    Confirmed = 2,

    [Description("Submit")]
    Submit = 3,

    [Description("Review")]
    Review = 4,
}
