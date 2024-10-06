using FluentResults;
using ScaleUp.Core.Api.Base.Configurations;
using ScaleUp.Core.Api.Features.Roles.Dtos;
using ScaleUp.Core.SharedKernel.Messaging;

namespace ScaleUp.Core.Api.Features.Roles.GetFeaturePermissionList;

internal sealed class GetFeaturePermissionListHandler(FeaturePermissionConfigurations featurePermissionConfigurations) : IQueryHandler<GetFeaturePermissionListQuery, GetFeaturePermissionListResponse>
{
    public Task<Result<GetFeaturePermissionListResponse>> Handle(GetFeaturePermissionListQuery request, CancellationToken cancellationToken)
    {
        var result = new GetFeaturePermissionListResponse
        {
            Data = featurePermissionConfigurations.Values.Select(x => new FeaturePermissionGroupDto
            {
                GroupName = x.GroupName,
                Features = x.Features.Select(f => new FeaturePermissionDto
                {
                    Code = f.Code,
                    Name = f.Name,
                    Note = f.Note
                }).ToList()
            }).ToList()
        };

        return Task.FromResult(Result.Ok(result));
    }
}