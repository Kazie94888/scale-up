namespace ScaleUp.Core.Domain.Entities.Users;

public sealed record UserRole
{
    private UserRole()
    {
    }

    public UserRole(Guid roleId, string roleName)
    {
        RoleId = roleId;
        RoleName = roleName;
    }

    public Guid RoleId { get; set; }
    public string RoleName { get; set; }
}