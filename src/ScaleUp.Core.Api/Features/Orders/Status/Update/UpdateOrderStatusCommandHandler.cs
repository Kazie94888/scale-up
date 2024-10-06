using FluentResults;
using Microsoft.EntityFrameworkCore;
using ScaleUp.Core.Application.Orders;
using ScaleUp.Core.Domain.Enums;
using ScaleUp.Core.Persistence.Context;
using ScaleUp.Core.SharedKernel.Messaging;

namespace ScaleUp.Core.Api.Features.Orders.Status.Update;

internal sealed class UpdateOrderStatusCommandHandler(
    IOrderManager orderManager,
    MasterDataContext dataContext
    ) : ICommandHandler<UpdateOrderStatusCommand, UpdateOrderStatusResponse>
{
    public async Task<Result<UpdateOrderStatusResponse>> Handle(UpdateOrderStatusCommand command, CancellationToken cancellationToken)
    {
        var order = await dataContext.Orders.Include(order => order.CancelReason).FirstAsync(x => x.Id == command.OrderId,
            cancellationToken: cancellationToken);


        switch (command.Request.Status)
        {
            //TODO: Update order.CancelReason.Code,order.CancelReason.Description from input
            case var status when status == OrderStatus.Cancelled:
                var cancelResult = await orderManager.CancelOrder(order, order.CancelReason!.Code,
                                   order.CancelReason.Description!, command.UserInfo);
                if (cancelResult.IsFailed)
                    return cancelResult;
                break;

            case var status when status == OrderStatus.Confirmed:
                var confirmedResult = await orderManager.ConfirmOrder(order, command.Request.Note, command.UserInfo);
                if (confirmedResult.IsFailed)
                    return confirmedResult;
                break;

            case var status when status == OrderStatus.Picked:
                var pickedResult = orderManager.PickOrder(order, command.Request.Note, command.UserInfo);
                if (pickedResult.IsFailed)
                    return pickedResult;
                break;

            case var status when status == OrderStatus.Packed:
                var packedResult = orderManager.PackOrder(order, command.Request.Note, command.UserInfo);
                if (packedResult.IsFailed)
                    return packedResult;
                break;
            
            case var status when status == OrderStatus.Delivered:
                var deliveredResult = await orderManager.DeliverOrder(order, command.Request.Note, command.UserInfo);
                if (deliveredResult.IsFailed)
                    return deliveredResult;
                break;

            default:
                var updateStatusResult = orderManager.UpdateOrderStatus(order, command.Request.Status, command.Request.Note, command.UserInfo);
                if (!updateStatusResult.IsFailed)
                    return updateStatusResult;
                break;
        }

        dataContext.Orders.Update(order);
        await dataContext.SaveChangesAsync(cancellationToken);

        return Result.Ok(new UpdateOrderStatusResponse
        {
            OrderId = order.Id
        });
    }
}
