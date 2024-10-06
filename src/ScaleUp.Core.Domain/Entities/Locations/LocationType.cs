using System.ComponentModel;

namespace ScaleUp.Core.Domain.Entities.Locations;

public enum LocationType
{
    [Description("country")]
    Country = 1,

    [Description("province")]
    Province = 2,

    [Description("distrit")]
    District = 3,

    [Description("ward")]
    Ward = 4
}