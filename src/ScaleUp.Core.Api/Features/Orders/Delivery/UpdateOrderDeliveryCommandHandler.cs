using FluentResults;
using Mapster;
using Microsoft.EntityFrameworkCore;
using ScaleUp.Core.Application.Orders;
using ScaleUp.Core.Application.Orders.Dtos;
using ScaleUp.Core.Persistence.Context;
using ScaleUp.Core.SharedKernel.Messaging;

namespace ScaleUp.Core.Api.Features.Orders.Delivery;

internal sealed class UpdateOrderDeliveryCommandHandler(
    IOrderManager orderManager,
    MasterDataContext dataContext) : ICommandHandler<UpdateOrderDeliveryCommand, UpdateOrderDeliveryResponse>
{
    public async Task<Result<UpdateOrderDeliveryResponse>> Handle(UpdateOrderDeliveryCommand command, CancellationToken cancellationToken)
    {
        var order = await dataContext.Orders.FirstAsync(x => x.Id == command.OrderId, cancellationToken);

        var request = command.Request;

        var updatedResult = await orderManager.UpdateDelivery(order, request.Adapt<UpdateOrderDeliveryRequestDto>(), command.UserInfo);
        if (updatedResult.IsFailed)
            return updatedResult;

        dataContext.Orders.Update(order);
        await dataContext.SaveChangesAsync(cancellationToken);

        return Result.Ok(new UpdateOrderDeliveryResponse
        {
            OrderId = command.OrderId
        });
    }
}