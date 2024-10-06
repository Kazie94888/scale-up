using FluentResults;
using Microsoft.EntityFrameworkCore;
using ScaleUp.Core.Domain.Entities.Warehouses;
using ScaleUp.Core.Persistence.Context;
using ScaleUp.Core.SharedKernel.Messaging;

namespace ScaleUp.Core.Api.Features.Warehouses.GetList;

internal sealed class GetWarehouseListQueryHandler(ReadOnlyMasterDataContext dataContext) : IQueryHandler<GetWarehouseListQuery, GetWarehouseListResponse>
{
    public async Task<Result<GetWarehouseListResponse>> Handle(GetWarehouseListQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Warehouse> warehouseQuery = dataContext.Warehouses;
        var data = await warehouseQuery.OrderByDescending(r => r.CreatedAt).Skip(request.Page * request.PageSize).Select(w => new GetWarehouseListResponseItem
        {
            Id = w.Id,
            Name = w.Name
        }).ToListAsync(cancellationToken);
        var total = await warehouseQuery.CountAsync(cancellationToken);
        
        return Result.Ok(new GetWarehouseListResponse
        {
            Total = total,
            Data = data
        });
    }
}