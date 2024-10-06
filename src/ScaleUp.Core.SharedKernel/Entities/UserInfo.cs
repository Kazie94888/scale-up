using ScaleUp.Core.SharedKernel.Enums;

namespace ScaleUp.Core.SharedKernel.Entities;

public sealed record UserInfo
{
    public required Guid Id { get; set; }
    public required string Username { get; set; }
    public required UserInfoType Type { get; set; }

    public static UserInfo System => new UserInfo
    {
        Id = Guid.Empty,
        Username = "System",
        Type = UserInfoType.System,
    };

    public static UserInfo Integration => new UserInfo
    {
        Id = Guid.Empty,
        Username = "Integration",
        Type = UserInfoType.Integration,
    };
}
