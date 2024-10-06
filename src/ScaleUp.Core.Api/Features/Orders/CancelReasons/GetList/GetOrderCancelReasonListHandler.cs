using FluentResults;
using ScaleUp.Core.Api.Features.Orders.Dtos;
using ScaleUp.Core.SharedKernel.Configurations;
using ScaleUp.Core.SharedKernel.Messaging;

namespace ScaleUp.Core.Api.Features.Orders.CancelReasons.GetList;

internal sealed class GetOrderCancelReasonListHandler(OrderCancelReasonConfigurations cancelReasonConfigurations) : IQueryHandler<GetOrderCancelReasonListQuery, GetOrderCancelReasonListResponse>
{
    public Task<Result<GetOrderCancelReasonListResponse>> Handle(GetOrderCancelReasonListQuery request, CancellationToken cancellationToken)
    {
        var result = new GetOrderCancelReasonListResponse
        {
            Data = cancelReasonConfigurations.Values.Select(x => new OrderCancelReasonDto
            {
                Code = x.Code,
                Description = x.Description,
                Index = x.Index
            }).ToList()

        };

        return Task.FromResult(Result.Ok(result));
    }
}