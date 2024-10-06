using FluentResults;
using Microsoft.EntityFrameworkCore;
using ScaleUp.Core.Application.Orders;
using ScaleUp.Core.Persistence.Context;
using ScaleUp.Core.SharedKernel.Messaging;

namespace ScaleUp.Core.Api.Features.Orders.Deliver;

internal sealed class DeliverOrderCommandHandler(
    IOrderManager orderManager,
    MasterDataContext dataContext) : ICommandHandler<DeliverOrderCommand, DeliverOrderResponse>
{
    public async Task<Result<DeliverOrderResponse>> Handle(DeliverOrderCommand command,
        CancellationToken cancellationToken)
    {
        var order = await dataContext.Orders.FirstAsync(x => x.Id == command.OrderId, cancellationToken);
        
        var deliveredResult = await orderManager.DeliverOrder(order, command.Request.Note, command.UserInfo);
        if (deliveredResult.IsFailed)
            return deliveredResult;
        
        dataContext.Update(order);
        await dataContext.SaveChangesAsync(cancellationToken);

        return Result.Ok(new DeliverOrderResponse
        {
            OrderId = command.OrderId
        });
    }
}