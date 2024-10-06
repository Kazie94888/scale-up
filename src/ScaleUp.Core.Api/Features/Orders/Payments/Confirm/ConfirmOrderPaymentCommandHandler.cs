using FluentResults;
using Microsoft.EntityFrameworkCore;
using ScaleUp.Core.Application.Orders;
using ScaleUp.Core.Persistence.Context;
using ScaleUp.Core.SharedKernel.Messaging;

namespace ScaleUp.Core.Api.Features.Orders.Payments.Confirm;

internal sealed class ConfirmOrderPaymentCommandHandler(
    MasterDataContext dataContext,
    IOrderManager orderManager
    ) : ICommandHandler<ConfirmOrderPaymentCommand, ConfirmOrderPaymentResponse>
{
    public async Task<Result<ConfirmOrderPaymentResponse>> Handle(ConfirmOrderPaymentCommand command, CancellationToken cancellationToken)
    {
        var order = await dataContext.Orders.FirstAsync(x => x.Id == command.OrderId,
            cancellationToken: cancellationToken);

        var confirmResult = await orderManager.ConfirmOrderPayment(order, command.UserInfo);
        if (confirmResult.IsFailed)
            return confirmResult;

        dataContext.Orders.Update(order);
        await dataContext.SaveChangesAsync(cancellationToken);

        return Result.Ok(new ConfirmOrderPaymentResponse
        {
            OrderId = order.Id
        });
    }
}
