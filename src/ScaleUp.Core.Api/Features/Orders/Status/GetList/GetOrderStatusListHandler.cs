using FluentResults;
using ScaleUp.Core.Api.Features.Orders.Dtos;
using ScaleUp.Core.Domain.Enums;
using ScaleUp.Core.SharedKernel.Messaging;

namespace ScaleUp.Core.Api.Features.Orders.Status.GetList;

internal sealed class GetOrderStatusListHandler : IQueryHandler<GetOrderStatusListQuery, GetOrderStatusListResponse>
{
    public Task<Result<GetOrderStatusListResponse>> Handle(GetOrderStatusListQuery request, CancellationToken cancellationToken)
    {
        var result = new GetOrderStatusListResponse
        {
            Data = OrderStatus.GetAll().Select(x => new OrderStatusDto
            {
                Status = x.Name,
                Label = x.Label,
                LabelType = x.LabelType,
                AllowedActions = x.AllowedActions.Select(a => a.Name).ToList(),
                AllowedStatuses = x.AllowedStatuses
            }).ToList()
        };

        return Task.FromResult(Result.Ok(result));
    }
}