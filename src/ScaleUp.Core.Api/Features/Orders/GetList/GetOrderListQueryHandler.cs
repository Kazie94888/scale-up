using FluentResults;
using Microsoft.EntityFrameworkCore;
using ScaleUp.Core.Api.Features.Orders.Dtos;
using ScaleUp.Core.Persistence.Context;
using ScaleUp.Core.SharedKernel.Messaging;

namespace ScaleUp.Core.Api.Features.Orders.GetList;

internal class GetOrderListQueryHandler(ReadOnlyMasterDataContext dataContext) : IQueryHandler<GetOrderListQuery, GetOrderListResponse>
{
    public async Task<Result<GetOrderListResponse>> Handle(GetOrderListQuery query, CancellationToken cancellationToken)
    {
        var orders = await dataContext.Orders.Skip(query.Page * query.PageSize).Take(query.PageSize).ToListAsync(cancellationToken);
        var total = await dataContext.Orders.CountAsync(cancellationToken);
        var data = orders.Select(x => new OrderDto
        {
            Id = x.Id,
            Assignee = x.Assignee is null ? null : new AssigneeDto
            {
                Id = x.Assignee.Id,
                Name = x.Assignee.FullName
            },
            AssigneeId = x.AssigneeId,
            Source = x.Source,
            Code = x.Code,
            CreatedAt = x.CreatedAt,
            Customer = UtilHelpers.GetCustomer(x),
            Status = x.Status,
            Total = x.Total,
            Channel = x.Channel,
            Warehouse = UtilHelpers.GetWarehouse(x)
        });

        var result = new GetOrderListResponse(data) { Total = total };
        return Result.Ok(result);
    }
}
