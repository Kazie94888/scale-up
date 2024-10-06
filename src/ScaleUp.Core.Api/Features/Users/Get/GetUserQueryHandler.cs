using FluentResults;
using Microsoft.EntityFrameworkCore;
using ScaleUp.Core.Api.Features.Users.Dtos;
using ScaleUp.Core.Persistence.Context;
using ScaleUp.Core.SharedKernel.Messaging;

namespace ScaleUp.Core.Api.Features.Users.Get;

internal sealed class GetUserQueryHandler(ReadOnlyMasterDataContext dataContext) : IQueryHandler<GetUserQuery, GetUserResponse>
{
    public async Task<Result<GetUserResponse>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await dataContext.Users.FirstAsync(x => x.Id == request.UserId, cancellationToken);
        var warehouses = await dataContext.Warehouses.Where(w => user.WarehouseIds.Contains(w.Id)).ToListAsync(cancellationToken);

        var response = new GetUserResponse
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Phone = user.Phone,
            CreatedAt = user.CreatedAt,
            Roles = user.Roles.Select(x => new UserRoleDto(x.RoleId, x.RoleName)),
            Warehouses = warehouses.Select(w => new UserWarehouseDto(w.Id, w.Name)),
            Status = user.Status
        };

        return Result.Ok(response);
    }
}