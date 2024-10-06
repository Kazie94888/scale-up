using FluentResults;
using Microsoft.EntityFrameworkCore;
using ScaleUp.Core.Persistence.Context;
using ScaleUp.Core.SharedKernel.Messaging;

namespace ScaleUp.Core.Api.Features.Locations.GetList;

internal sealed class GetLocationListQueryHandler(ReadOnlyMasterDataContext context) : IQueryHandler<GetLocationListQuery, List<LocationDto>>
{
    public async Task<Result<List<LocationDto>>> Handle(GetLocationListQuery request, CancellationToken cancellationToken)
    {
        var locations = await context.Locations
            .Where(l => l.Type == request.Type && (!request.ParentId.HasValue || l.ParentId == request.ParentId))
            .Select(l => new LocationDto
            {
                Id = l.Id,
                Code = l.Code,
                Name = l.Name,
                Type = l.Type,
                ParentId = l.ParentId
            }).ToListAsync(cancellationToken);

        return Result.Ok(locations);
    }
}