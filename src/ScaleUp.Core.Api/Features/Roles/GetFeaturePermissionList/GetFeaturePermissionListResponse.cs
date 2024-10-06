
using ScaleUp.Core.Api.Features.Roles.Dtos;

namespace ScaleUp.Core.Api.Features.Roles.GetFeaturePermissionList;

internal sealed record GetFeaturePermissionListResponse
{
    public required List<FeaturePermissionGroupDto> Data { get; set; }
    
}