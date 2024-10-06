namespace ScaleUp.Core.Domain.Entities.Tenants;

public sealed record HaravanIntegrationConfig
{
    public HaravanIntegrationConfig()
    {
        OrgId = string.Empty;
        ClientSecret = string.Empty;
        AuthenticationToken = string.Empty;
    }

    public string? OrgId { get; set; }
    public string? ClientSecret { get; set; }
    public string? AuthenticationToken { get; set; }
}
