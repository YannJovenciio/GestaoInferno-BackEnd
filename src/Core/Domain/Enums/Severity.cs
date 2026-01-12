using System.ComponentModel;

namespace Inferno.src.Core.Domain.Enums
{
    public enum Severity
    {
        [Description("low")]
        low = 1,

        [Description("medium")]
        medium = 2,

        [Description("high")]
        high = 3,

        [Description("critical")]
        critical = 4,
    }
}
