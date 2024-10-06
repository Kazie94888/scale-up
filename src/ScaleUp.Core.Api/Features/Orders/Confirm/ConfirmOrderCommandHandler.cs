using FluentResults;
using Microsoft.EntityFrameworkCore;
using ScaleUp.Core.Application.Orders;
using ScaleUp.Core.Persistence.Context;
using ScaleUp.Core.SharedKernel.Messaging;

namespace ScaleUp.Core.Api.Features.Orders.Confirm;

internal sealed class ConfirmOrderCommandHandler(
    IOrderManager orderManager,
    MasterDataContext dataContext) : ICommandHandler<ConfirmOrderCommand, ConfirmOrderResponse>
{
    public async Task<Result<ConfirmOrderResponse>> Handle(ConfirmOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await dataContext.Orders.FirstAsync(x => x.Id == command.OrderId,
            cancellationToken: cancellationToken);

        var confirmedResult = await orderManager.ConfirmOrder(order, command.Request.Note, command.UserInfo);
        if (confirmedResult.IsFailed)
            return confirmedResult;

        dataContext.Orders.Update(order);
        await dataContext.SaveChangesAsync(cancellationToken);

        return Result.Ok(new ConfirmOrderResponse
        {
            OrderId = order.Id
        });
    }
}