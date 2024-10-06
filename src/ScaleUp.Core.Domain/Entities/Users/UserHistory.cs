namespace ScaleUp.Core.Domain.Entities.Users;

public sealed record UserHistory
{
    private UserHistory()
    {
    }

    public UserHistory(string firstName, string lastName, string email, string? phone, string status, List<Guid> roleIds, List<Guid> warehouseIds,
        DateTime updatedAt, string updatedBy)
    {
        FirstName = firstName;
        LastName = lastName;
        Phone = phone;
        Email = email;
        RoleIds = roleIds;
        WarehouseIds = warehouseIds;
        UpdatedAt = updatedAt;
        UpdatedBy = updatedBy;
        Status = status;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string? Phone { get; set; }
    public List<Guid> RoleIds { get; set; }
    public List<Guid> WarehouseIds { get; set; }
    public string Status { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string UpdatedBy { get; set; }
}