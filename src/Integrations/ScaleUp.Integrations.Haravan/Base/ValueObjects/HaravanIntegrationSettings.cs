
namespace ScaleUp.Integrations.Haravan.Base.ValueObjects;

public sealed record HaravanIntegrationSettings
{
    public string BaseAddress { get; init; }
    public string SecretKey { get; init; }
    public Guid TenantId { get; set; }
}