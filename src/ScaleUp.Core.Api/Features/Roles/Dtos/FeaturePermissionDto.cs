namespace ScaleUp.Core.Api.Features.Roles.Dtos;

internal sealed record FeaturePermissionDto
{
    public required string Code { get; set; }
    public required string Name { get; set; }
    public string? Note { get; set; }
}