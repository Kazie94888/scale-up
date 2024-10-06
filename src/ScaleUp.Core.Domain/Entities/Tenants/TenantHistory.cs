namespace ScaleUp.Core.Domain.Entities.Tenants;

public sealed record TenantHistory
{
    private TenantHistory()
    {
    }

    public TenantHistory(string name, string adminEmail, string? phone, string version, DateTime? activationEndDate,
        string activationState, DateTime updatedAt)
    {
        Name = name;
        AdminEmail = adminEmail;
        Phone = phone;
        Version = version;
        ActivationEndDate = activationEndDate;
        ActivationState = activationState;
        UpdatedAt = updatedAt;
    }
    
    public string Name { get; set; }
    public string AdminEmail { get; set; }
    public string? Phone { get; set; }
    public string Version { get; set; }
    public DateTime? ActivationEndDate { get; set; }
    public string ActivationState { get; set; }
    public DateTime UpdatedAt { get; set; }
}