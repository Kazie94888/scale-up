namespace ScaleUp.Core.Api.Features.Roles.Dtos;

internal sealed record FeaturePermissionGroupDto
{
    public required string GroupName { get; set; }
    public required List<FeaturePermissionDto> Features { get; set; }
}