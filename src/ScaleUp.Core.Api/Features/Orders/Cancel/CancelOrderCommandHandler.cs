using FluentResults;
using Microsoft.EntityFrameworkCore;
using ScaleUp.Core.Application.Orders;
using ScaleUp.Core.Persistence.Context;
using ScaleUp.Core.SharedKernel.Messaging;

namespace ScaleUp.Core.Api.Features.Orders.Cancel;

internal sealed class CancelOrderCommandHandler(
    IOrderManager orderManager,
    MasterDataContext dataContext)
    : ICommandHandler<CancelOrderCommand, CancelOrderResponse>
{
    public async Task<Result<CancelOrderResponse>> Handle(CancelOrderCommand command,
        CancellationToken cancellationToken)
    {
        var order = await dataContext.Orders.FirstAsync(x => x.Id == command.OrderId,
            cancellationToken);

        var cancelResult = await orderManager.CancelOrder(order, command.Request.CancelReasonCode,
            command.Request.Note, command.UserInfo);

        if (cancelResult.IsFailed)
            return cancelResult;

        dataContext.Orders.Update(order);
        await dataContext.SaveChangesAsync(cancellationToken);

        return Result.Ok(new CancelOrderResponse { OrderId = order.Id });
    }
}