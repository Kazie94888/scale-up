using FluentResults;
using Microsoft.EntityFrameworkCore;
using ScaleUp.Core.Api.Features.Orders.Dtos;
using ScaleUp.Core.Persistence.Context;
using ScaleUp.Core.SharedKernel.Messaging;

namespace ScaleUp.Core.Api.Features.Orders.Get;

internal sealed class GetOrderDetailsQueryHandler(ReadOnlyMasterDataContext dataContext) : IQueryHandler<GetOrderDetailsQuery, OrderDetailsDto>
{
    public async Task<Result<OrderDetailsDto>> Handle(GetOrderDetailsQuery request, CancellationToken cancellationToken)
    {
        var order = await dataContext.Orders.Where(x => x.Id == request.OrderId).FirstOrDefaultAsync(cancellationToken: cancellationToken);
        if (order is null)
        {
            return Result.Fail($"Order {request.OrderId} not found");
        }

        var result = new OrderDetailsDto
        {
            Id = order.Id,
            Assignee = UtilHelpers.GetAssignee(order),
            AssigneeId = order.AssigneeId,
            Source = order.Source,
            Code = order.Code,
            CreatedAt = order.CreatedAt,
            CreatedBy = order.CreatedBy.Username,
            Note = order.Note,
            Customer = UtilHelpers.GetCustomer(order),
            Delivery = UtilHelpers.GetDelivery(order),
            FinancialStatus = order.FinancialStatus,
            DiscountAmount = order.DiscountAmount,
            FulfillmentId = 0,
            Lines = UtilHelpers.GetOrderLines(order),
            Payment = UtilHelpers.GetPayment(order),
            ShippingFee = order.ShippingFee,
            Status = order.Status,
            SubTotal = order.SubTotal,
            Total = order.Total,
            ExternalCode = order.ExternalCode,
            ExternalCreatedAt = order.ExternalCreatedAt,
            ExternalId = order.ExternalId,
            Channel = order.Channel,
            Warehouse = UtilHelpers.GetWarehouse(order),
            History = UtilHelpers.GetHistory(order),
            //TODO: Map GroupRelativeId, RelativeOrders
        };

        return Result.Ok(result);
    }
}